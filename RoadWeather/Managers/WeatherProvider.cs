using Newtonsoft.Json;
using RoadWeather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using log4net;

namespace RoadWeather.Managers
{
    /// <summary>
    /// This class is able to provide selected mode of forecast for location provided.
    /// </summary>
    public class WeatherProvider
    {
        private static readonly ILog log = LogManager.GetLogger("WeatherProvider");
        private static readonly string WEATHER_API_URI_LONG =  @"http://api.openweathermap.org/data/2.5/forecast/daily?lat={0}&lon={1}&APPID=3e498b17220c9b49140ea1bb8a94c010&units=metric&cnt=16";
        private static readonly string WEATHER_API_URI_SHORT =  @"http://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&APPID=3e498b17220c9b49140ea1bb8a94c010&units=metric";

        /// <summary>
        /// Returns long term forecast for location specified
        /// </summary>
        /// <param name="location">Location</param>
        /// <returns>Long term forecast for location</returns>
        public async Task<ForecastLongTerm> GetForecastLongTerm(Location location)
        {
            Uri uri = new Uri
            (
                string.Format(WEATHER_API_URI_LONG, location.Latitude, location.Longitude)
            );
            using (var webClient = new System.Net.WebClient())
            {
                try
                {
                    var json = await webClient.DownloadStringTaskAsync(uri);
                    return JsonConvert.DeserializeObject<ForecastLongTerm>(json);
                }
                catch (Exception ex)
                {
                    string msg = "Problem retrieving long term forecasts";
                    log.Error(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        /// <summary>
        /// Returns short term forecast for location specified
        /// </summary>
        /// <param name="location">Location</param>
        /// <returns>Short term forecast for location</returns>
        public async Task<ForecastShortTerm> GetForecastShortTerm(Location location)
        {
            Uri uri = new Uri
            (
                string.Format(WEATHER_API_URI_SHORT, location.Latitude, location.Longitude)
            );
            using (var webClient = new System.Net.WebClient())
            {
                try
                {
                    var json = await webClient.DownloadStringTaskAsync(uri);
                    return JsonConvert.DeserializeObject<ForecastShortTerm>(json);
                }
                catch (Exception ex)
                {
                    string msg = "Problem retrieving short term forecasts";
                    log.Error(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

    }
}