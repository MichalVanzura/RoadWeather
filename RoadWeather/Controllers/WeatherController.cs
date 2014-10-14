using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RoadWeather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RoadWeather.Controllers
{
    public class WeatherController : ApiController
    {
        [HttpPost]
        public JToken GetWeatherMarkers([FromBody]IEnumerable<Location> locations)
        {
            List<Marker> markers = new List<Marker>();
            int numOfLocs = locations.Count() / 10;
            locations = locations.Where((x, i) => i % numOfLocs == 0);
            foreach (Location loc in locations)
            {
                string URL = string.Format("http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&APPID=3e498b17220c9b49140ea1bb8a94c010",
                    loc.Latitude, loc.Longitude);
                using (var webClient = new System.Net.WebClient())
                {
                    var json = webClient.DownloadString(URL);
                    Forecast forecast = JsonConvert.DeserializeObject<Forecast>(json);

                    Marker marker = new Marker
                    {
                        Location = new Location 
                        { 
                            Latitude = loc.Latitude, 
                            Longitude = loc.Longitude, 
                        },
                        Title = string.Format("Main: {0}<br/>Temp: {1}",
                            forecast.Weather[0].Main,
                            forecast.Main.Temp),
                        Image = new Image 
                        {
                            Url = string.Format("http://openweathermap.org/img/w/{0}.png", forecast.Weather[0].Icon),
                        }
                    };

                    markers.Add(marker);
                }
            }
            return JToken.FromObject(markers);
        }

    }
}
