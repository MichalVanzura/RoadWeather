using RoadWeather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Diagnostics.Contracts;
using RoadWeather.Managers.Interfaces;

namespace RoadWeather.Managers
{
    /// <summary>
    /// Provides helper methods for weather entities
    /// </summary>
    public class WeatherUtils : RoadWeather.Managers.Interfaces.IWeatherUtils
    {
        private static readonly ILog log = LogManager.GetLogger("WeatherUtils");
        private IClock clock;

        public WeatherUtils(IClock clock)
        {
            this.clock = clock;
        }

        /// <summary>
        /// Returns true if trip is available for short term forecast. 
        /// That is if trip duration estimate ends before last of the forecasts available.
        /// </summary>
        /// <param name="trip">Trip to be assessed</param>
        /// <returns>True if available for short term</returns>
        public bool AvailableForShortTermForecast(Trip trip)
        {
            if (trip == null)
            {
                throw new ArgumentNullException("Trip is null");
            }
            // 5 day forecast is returned for today and 4 following days
            // last entry is midnight of the 5th day
            bool isShortTerm = trip.StartDateTime.AddSeconds(trip.Duration) < clock.Now.Date.AddDays(5);
            log.Debug("Trip " + (isShortTerm ? "is " : "isn't ") + "available for short term forecast.");
            return isShortTerm;
        }

        /// <summary>
        /// Selects closest forecast for location's estimated time.
        /// </summary>
        /// <param name="location">Location</param>
        /// <param name="forecast">Forecasts for location</param>
        /// <returns>Forecast entry closest</returns>
        public ForecastShortTermEntry SelectShortTermEntry(LocationDetail location, ForecastShortTerm forecast)
        {
            if (location == null)
            {
                throw new ArgumentNullException("Location is null");
            }
            if (forecast == null)
            {
                throw new ArgumentNullException("Forecast is null");
            }

            var entry = forecast.Entries
                    .OrderBy(e => (e.ForecastTime - location.Time).Duration())
                    .FirstOrDefault();
            if (entry == null)
            {
                string msg = "No forecast was chosen as closest";
                log.Error(msg);
                throw new NullReferenceException(msg);
            }
            return entry;
        }

        /// <summary>
        /// Selects daily weather forecast for location's estimated date.
        /// </summary>
        /// <param name="location">Location</param>
        /// <param name="forecast">Forecasts for location</param>
        /// <returns>Forecast for the day</returns>
        public ForecastDailyEntry SelectLongTermEntry(LocationDetail location, ForecastLongTerm forecast)
        {
            if (location == null)
            {
                throw new ArgumentNullException("Location is null");
            }
            if (forecast == null)
            {
                throw new ArgumentNullException("Forecast is null");
            }

            var entry = forecast.Entries.Where(x => x.ForecastTime.Date == location.Time.Date).FirstOrDefault();
            if (entry == null)
            {
                string msg = "No forecast for the day selected";
                log.Error(msg);
                throw new NullReferenceException(msg);
            }
            return entry;
        }


    }
}