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

namespace RoadWeather.Controllers
{
    public class WeatherController : ApiController
    {
        List<Marker> markers = new List<Marker>();

        public async Task GetForecast(Location location)
        {
            Uri uri = new Uri 
            (
                string.Format("http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&APPID=3e498b17220c9b49140ea1bb8a94c010&units=metric",
                    location.Latitude, location.Longitude)
            );
            using (var webClient = new System.Net.WebClient())
            {
                var json = await webClient.DownloadStringTaskAsync(uri);
                Forecast forecast = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Forecast>(json));

                Marker marker = new Marker
                {
                    Location = new Location
                    {
                        Latitude = location.Latitude,
                        Longitude = location.Longitude,
                    },
                    Title = string.Format("<b>{0}</b><br/>{1}°C",
                        forecast.Weather[0].Main,
                        Math.Round(forecast.Main.Temp)),
                    Image = new Image
                    {
                        Url = string.Format("http://openweathermap.org/img/w/{0}.png", forecast.Weather[0].Icon),
                    }
                };

                markers.Add(marker);
            }
        }

        [HttpPost]
        public async Task<JToken> GetWeatherMarkers([FromBody]IEnumerable<Location> locations)
        {
            int numOfLocs = locations.Count() / 10;
            locations = locations.Where((x, i) => i % numOfLocs == 0);
            List<Task> tasks = new List<Task>();
            foreach (Location loc in locations)
            {
                tasks.Add(Task.Run(() => GetForecast(loc)));
            }
            await Task.WhenAll(tasks);
            return JToken.FromObject(markers);
        }
    }
}
