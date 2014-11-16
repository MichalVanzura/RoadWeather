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
using RoadWeather.Managers;
namespace RoadWeather.Controllers
{
    public class WeatherController : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger("WeatherController");

        [HttpPost]
        public async Task<List<Marker>> GetWeatherMarkers([FromBody]Trip trip)
        {
            TripWeatherManager weatherManager = new TripWeatherManager();
            var results = await weatherManager.GetForecastForTrip(trip);
            var markers = new List<Marker>();

            foreach (var kvp in results)
            {
                Marker marker = new Marker
                {
                    Location = new Location
                    {
                        Latitude = kvp.Key.Location.Latitude,
                        Longitude = kvp.Key.Location.Longitude,
                    },
                    Title = string.Format("<div style='width: 120px;'><b>{0}</b><br/>{1}°C<br/>{2}</div>",
                        kvp.Value.Description,
                        kvp.Value.Temperature,
                        kvp.Value.DateTime.ToShortTimeString()),
                    Image = new Image
                    {
                        Url = string.Format("http://openweathermap.org/img/w/{0}.png", kvp.Value.Icon)
                    }
                };

                markers.Add(marker);
            }
            return markers;
        }
    }
}
