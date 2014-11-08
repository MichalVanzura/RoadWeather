using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RoadWeather.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using log4net;
namespace RoadWeather.Controllers
{
    public class WeatherController : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger("WeatherController");

        List<Marker> markers = new List<Marker>();

        private bool AvailableFor5DayForecast(LocationsAtTime message)
        {
            // 5 day forecast is returned for today and 4 following days
            // last entry is midnight of the 5th day
            return message.StartDateTime.AddSeconds(message.Duration) < DateTime.Now.Date.AddDays(5);
        }
        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        private static long DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (long)(dateTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
        }


        private void SetMarkers3Hrs(Location location, Forecast3HoursEntry entry)
        {
            Marker marker = new Marker
            {
                Location = new Location
                {
                    Latitude = location.Latitude,
                    Longitude = location.Longitude,
                },
                Title = string.Format("<div style='width: 120px;'><b>{0}</b><br/>{1}°C<br/>{2}</div>",
                    entry.WeatherDescription[0].Main,
                    Math.Round(entry.MainValues.Temp),
                    entry.TimestampString.Substring(0, entry.TimestampString.Length - 3)),
                Image = new Image
                {
                    Url = string.Format("http://openweathermap.org/img/w/{0}.png", entry.WeatherDescription[0].Icon),
                }
            };

            markers.Add(marker);
        }

        private void SetMarkersDaily(Location location, ForecastDailyEntry entry,  DateTime expectedDateTime)
        {
            //suggestion: use expectedDateTime to determine use of morning / day / evening / night temperature
            Marker marker = new Marker
            {
                Location = new Location
                {
                    Latitude = location.Latitude,
                    Longitude = location.Longitude,
                },
                Title = string.Format("<div style='width: 120px;'><b>{0}</b><br/>{1}°C<br/>{2}</div>",
                    entry.WeatherDescription[0].Description,
                    Math.Round(entry.Temp.Day),
                    UnixTimeStampToDateTime(entry.UnixTimestamp)),
                Image = new Image
                {
                    Url = string.Format("http://openweathermap.org/img/w/{0}.png", entry.WeatherDescription[0].Icon)
                }
            };

            markers.Add(marker);
        }

        public async Task GetForecast3Hrs(Location location, DateTime expectedDateTime)
        {
            Uri uri = new Uri
            (
                string.Format("http://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&APPID=3e498b17220c9b49140ea1bb8a94c010&units=metric",
                    location.Latitude, location.Longitude)
            );
            using (var webClient = new System.Net.WebClient())
            {
                try
                {
                    var json = await webClient.DownloadStringTaskAsync(uri);
                    Forecast3Hours forecast = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Forecast3Hours>(json));
                    // Get time difference between expected time and available forecasts
                    Dictionary<Forecast3HoursEntry, long> dict = new Dictionary<Forecast3HoursEntry, long>();
                    foreach (var entry in forecast.Entries)
                    {
                        dict.Add(entry, Math.Abs(DateTimeToUnixTimestamp(expectedDateTime)  - entry.UnixTimestamp));
                    }
                    // Choose entry that is closest to the expected time
                    var closestEntry = dict.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
                    SetMarkers3Hrs(location, closestEntry);
                }
                catch (Exception ex)
                {
                    log.Error("Problem at method GetForecast3Hrs", ex);
                    throw new Exception("Problem at method GetForecast3Hrs", ex);
                }
            }
        }

        public async Task GetForecastDaily(Location location, DateTime expectedDateTime)
        {
            Uri uri = new Uri
            (
                string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?lat={0}&lon={1}&APPID=3e498b17220c9b49140ea1bb8a94c010&units=metric&cnt=16",
                    location.Latitude, location.Longitude)
            );
            using (var webClient = new System.Net.WebClient())
            {
                try
                {
                    var json = await webClient.DownloadStringTaskAsync(uri);
                    ForecastDaily forecast = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<ForecastDaily>(json));
                    // Get forecast for day of expected date time
                    var entryForDate = forecast.Entries.Where(x => UnixTimeStampToDateTime(x.UnixTimestamp).Date == expectedDateTime.Date).FirstOrDefault();
                    if (entryForDate == null)
                    {
                        log.Error("No forecast for the day selected");
                        throw new Exception("No forecast for the day selected");
                    }
                    SetMarkersDaily(location, entryForDate, expectedDateTime);
                }
                catch (Exception ex)
                {
                    log.Error("Problem at method GetForecastDaily", ex);
                    throw new Exception("Problem at method GetForecastDaily", ex);
                }
            }
        }

        [HttpPost]
        public async Task<JToken> GetWeatherMarkers([FromBody]LocationsAtTime message)
        {
            log.Debug("Method GetWeatherMarkers called.");
            var locations = message.Locations;
            int locationsStepLength = locations.Count() / 10;
            double stepSizeRatio = (double)locationsStepLength / (double)locations.Count();
            
            var selectedLocations = locations.Where((x, i) => i % locationsStepLength == 0);
            double stepDuration = stepSizeRatio * message.Duration;

            log.Debug(string.Format("Expected duration: {0} min", (int)(message.Duration / 60)));
            log.Debug(string.Format("Estimated step duration: {0} min", (int)(stepDuration / 60)));

            //TODO: check end time doesn't exceed 16 days
            bool availableFor5Day = AvailableFor5DayForecast(message);
            log.Debug("Trip " + (availableFor5Day ? "is " : "isn't ") + "available for 5 day forecast.");

            List<Task> tasks = new List<Task>();
            int counter = 0;

            foreach (Location loc in selectedLocations)
            {
                DateTime dTime = message.StartDateTime.AddSeconds(counter * stepDuration);
                counter++;
                if (availableFor5Day)
                {
                    tasks.Add(Task.Run(() => GetForecast3Hrs(loc, dTime)));
                }
                else
                {
                    tasks.Add(Task.Run(() => GetForecastDaily(loc, dTime)));
                }
            }
            await Task.WhenAll(tasks);
            return JToken.FromObject(markers);
        }
    }
}
