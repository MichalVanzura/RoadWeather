﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RoadWeather.Models;
using System.Threading.Tasks;
using log4net;

namespace RoadWeather.Managers
{
    /// <summary>
    /// This class retrieves forecasts for locations provided.
    /// </summary>
    public class LocationWeatherProvider
    {
        private WeatherProvider provider = new WeatherProvider();
        private static readonly ILog log = LogManager.GetLogger("LocationWeatherProvider");

        /// <summary>
        /// Returns complete long term forecasts for list of locations specified.
        /// </summary>
        /// <param name="locations">List of locations</param>
        /// <returns>Dictionary of long term forecasts for locations</returns>
        public async Task<Dictionary<LocationDetail, ForecastDailyEntry>> GetEntriesForLocationsLongTerm(List<LocationDetail> locations)
        {
            // Create tasks for getting forecast
            var dictOfTasks = new Dictionary<LocationDetail, Task<ForecastLongTerm>>();
            foreach (LocationDetail loc in locations)
            {
                dictOfTasks.Add(loc, provider.GetForecastLongTerm(loc.Location));
            }
            await Task.WhenAll(dictOfTasks.Values);

            // Return as dict of locations with forecasts
            var dictOfResults = new Dictionary<LocationDetail, ForecastDailyEntry>();
            foreach (var locAndTask in dictOfTasks)
            {
                var selectedEntry = WeatherUtils.SelectLongTermEntry(locAndTask.Key, locAndTask.Value.Result);
                dictOfResults.Add(locAndTask.Key, selectedEntry);
            }
            return dictOfResults;
        }

        /// <summary>
        /// Returns complete short term forecasts for list of locations specified.
        /// </summary>
        /// <param name="locations">List of locations</param>
        /// <returns>Dictionary of short term forecasts for locations</returns>
        public async Task<Dictionary<LocationDetail, ForecastShortTermEntry>> GetEntriesForLocationsShortTerm(List<LocationDetail> locations)
        {
            // Create tasks for getting forecast
            var dictOfTasks = new Dictionary<LocationDetail, Task<ForecastShortTerm>>();
            foreach (LocationDetail loc in locations)
            {
                dictOfTasks.Add(loc, provider.GetForecastShortTerm(loc.Location));
            }
            await Task.WhenAll(dictOfTasks.Values);

            // Return as dict of locations with forecasts
            var dictOfResults = new Dictionary<LocationDetail, ForecastShortTermEntry>();
            foreach (var locAndTask in dictOfTasks)
            {
                var selectedEntry = WeatherUtils.SelectShortTermEntry(locAndTask.Key, locAndTask.Value.Result);
                dictOfResults.Add(locAndTask.Key, selectedEntry);
            }
            return dictOfResults;
        }

        
    }
}