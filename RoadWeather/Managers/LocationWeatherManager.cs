using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RoadWeather.Models;
using System.Threading.Tasks;
using log4net;
using RoadWeather.Managers.Interfaces;


namespace RoadWeather.Managers
{
    /// <summary>
    /// This class retrieves forecasts for locations provided.
    /// </summary>
    public class LocationWeatherManager : RoadWeather.Managers.Interfaces.ILocationWeatherManager
    {
        private static readonly ILog log = LogManager.GetLogger("LocationWeatherProvider");
        private IWeatherProvider weatherProvider;
        private IWeatherUtils weatherUtils;

        public LocationWeatherManager(IWeatherProvider weatherProvider, IWeatherUtils utils)
        {
            this.weatherProvider = weatherProvider;
            this.weatherUtils = utils;
        }

        /// <summary>
        /// Returns complete long term forecasts for list of locations specified.
        /// </summary>
        /// <param name="locations">List of locations</param>
        /// <returns>Dictionary of long term forecasts for locations</returns>
        public async Task<Dictionary<LocationDetail, ForecastDailyEntry>> GetEntriesForLocationsLongTerm(List<LocationDetail> locations)
        {
            if (locations == null)
            {
                throw new ArgumentNullException("Locations are null");
            }
            if (locations.Count == 0)
            {
                throw new ArgumentException("List of locations has length 0");
            }
            // Create tasks for getting forecast
            var dictOfTasks = locations.ToDictionary(loc => loc, loc => weatherProvider.GetForecastLongTerm(loc.Location));
            await Task.WhenAll(dictOfTasks.Values);

            // Return as dict of locations with forecasts
            var dictOfResults = new Dictionary<LocationDetail, ForecastDailyEntry>();
            foreach (var locAndTask in dictOfTasks)
            {
                var selectedEntry = weatherUtils.SelectLongTermEntry(locAndTask.Key, locAndTask.Value.Result);
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
            if (locations == null)
            {
                throw new ArgumentNullException("Locations are null");
            }
            if (locations.Count == 0)
            {
                throw new ArgumentException("List of locations has length 0");
            }
            // Create tasks for getting forecast
            var dictOfTasks = locations.ToDictionary(loc => loc, loc => weatherProvider.GetForecastShortTerm(loc.Location));
            await Task.WhenAll(dictOfTasks.Values);

            // Return as dict of locations with forecasts
            var dictOfResults = new Dictionary<LocationDetail, ForecastShortTermEntry>();
            foreach (var locAndTask in dictOfTasks)
            {
                var selectedEntry = weatherUtils.SelectShortTermEntry(locAndTask.Key, locAndTask.Value.Result);
                dictOfResults.Add(locAndTask.Key, selectedEntry);
            }
            return dictOfResults;
        }
    }
}