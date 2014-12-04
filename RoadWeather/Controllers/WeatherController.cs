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
using RoadWeather.Managers.Interfaces;
using RoadWeather.App_Start;
using Microsoft.Practices.Unity;


namespace RoadWeather.Controllers
{
    public class WeatherController : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger("WeatherController");
        private ITripWeatherManager _tripWeatherManager;
        
        public ITripWeatherManager TripWeatherManager
        {
            get { return _tripWeatherManager; }
            set { _tripWeatherManager = value; }
        }
        public WeatherController()
        {
            var container = UnityConfig.GetConfiguredContainer();
            this._tripWeatherManager = container.Resolve<TripWeatherManager>();
        }

        [HttpPost]
        public async Task<List<Marker>> GetWeatherMarkers([FromBody]Trip trip)
        {

            var results = await _tripWeatherManager.GetForecastForTrip(trip);
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
                    Text = new Text
                    {
                        Description = kvp.Value.Description,
                        Temperature = kvp.Value.Temperature,
                        DateTime = kvp.Value.DateTime
                    },
                    //string.Format("<div style='width: 120px;'><b>{0}</b><br/>{1}°C<br/>{2}</div>",
                    //    kvp.Value.Description,
                    //    kvp.Value.Temperature,
                    //    kvp.Value.DateTime.ToShortTimeString()),
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
