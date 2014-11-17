﻿using RoadWeather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Threading.Tasks;

namespace RoadWeather.Managers
{
    /// <summary>
    /// This class provides weather services for triped specified by parameter.
    /// </summary>
    public class TripWeatherManager
    {
        private static readonly ILog log = LogManager.GetLogger("WeatherManager");
        private const int INTERVAL_COUNT = 10;
        private LocationWeatherProvider provider = new LocationWeatherProvider();

        /// <summary>
        /// Returns dictionary of location details and entry adapters for trip.
        /// ForecastEntryAdapters provide unified forecast entry for long and short term forecasts.
        /// </summary>
        /// <param name="trip">Trip</param>
        /// <returns>Weather for locations on the trip</returns>
        public async Task<Dictionary<LocationDetail, ForecastEntryAdapter>> GetForecastForTrip(Trip trip)
        {
            var dictResult = new Dictionary<LocationDetail, ForecastEntryAdapter>();        
            if (WeatherUtils.AvailableForShortTermForecast(trip))
            {
                var locationForecasts = await GetShortTermForecastForTrip(trip);
                foreach (var kvp in locationForecasts)
                {
                    dictResult.Add(kvp.Key, new ForecastEntryAdapter(kvp.Value));
                }
                return dictResult;
            }
            else
            {
                //TODO: check end time doesn't exceed 16 days
                var locationForecasts = await GetLongTermForecastForTrip(trip);
                foreach (var kvp in locationForecasts)
                {
                    dictResult.Add(kvp.Key, new ForecastEntryAdapter(kvp.Value));
                }
                return dictResult;
            }
        }

        /// <summary>
        /// Returns dictionary of location details and short term entry for trip.
        /// </summary>
        /// <param name="trip">Trip</param>
        /// <returns>Weather for locations on the trip</returns>
        public async Task<Dictionary<LocationDetail, ForecastShortTermEntry>> GetShortTermForecastForTrip(Trip trip)
        {
            if (!WeatherUtils.AvailableForShortTermForecast(trip))
            {
                string msg = "Provided trip does not qualify for short term forecast.";
                log.Error(msg);
                throw new ArgumentException(msg);
            }
            var locations = GetLocationsInIntervalsWithTime(trip);
            return await provider.GetEntriesForLocationsShortTerm(locations);
        }

        /// <summary>
        /// Returns dictionary of location details and long term entry for trip.
        /// </summary>
        /// <param name="trip">Trip</param>
        /// <returns>Weather for locations on the trip</returns>
        public async Task<Dictionary<LocationDetail, ForecastDailyEntry>> GetLongTermForecastForTrip(Trip trip)
        {
            var locations = GetLocationsInIntervalsWithTime(trip);
            return await provider.GetEntriesForLocationsLongTerm(locations);
        }

        /// <summary>
        /// Returns locations in intervals for the trip with estimated time.
        /// </summary>
        /// <param name="trip">Trip</param>
        /// <returns>List of locations with time</returns>
        private List<LocationDetail> GetLocationsInIntervalsWithTime(Trip trip)
        {
            var locations = trip.Locations;
            int stepLength = locations.Count() / INTERVAL_COUNT;

            double stepSizeRatio = (double)stepLength / (double)locations.Count();
            int stepDuration = (int)(stepSizeRatio * trip.Duration);
            log.Debug(string.Format("Expected duration: {0} min", (int)(trip.Duration / 60)));
            log.Debug(string.Format("Estimated step duration: {0} min", (int)(stepDuration / 60)));

            var selectedLocations = SelectLocationsInIntervals(trip.Locations.ToList(), stepLength);
            var locationsWithTime = GetTimeForLocations(selectedLocations, stepDuration, trip.StartDateTime);
            return locationsWithTime;
        }

        private List<Location> SelectLocationsInIntervals(IList<Location> locations, int stepLength)
        {
            return locations.Where((x, i) => i % stepLength == 0).ToList();
        }

        private List<LocationDetail> GetTimeForLocations(IList<Location> selectedLocations, int stepDuration, DateTime start)
        {
            var list = new List<LocationDetail>();

            int counter = 0;
            foreach (Location loc in selectedLocations)
            {
                DateTime dt = start.AddSeconds(counter * stepDuration);
                list.Add(new LocationDetail() { Location = loc, Time = dt });
                counter++;
            }
            return list;
        }
    }
}