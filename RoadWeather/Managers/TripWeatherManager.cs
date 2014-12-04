using RoadWeather.Models;
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
        public async Task<Dictionary<LocationDetail, ForecastEntry>> GetForecastForTrip(Trip trip)
        {
            if (trip == null)
            {
                throw new ArgumentNullException("Trip is null");
            }

            var dictResult = new Dictionary<LocationDetail, ForecastEntry>();        
            if (WeatherUtils.AvailableForShortTermForecast(trip))
            {
                var locations = GetLocationsInIntervalsWithTime(trip);
                var locationForecasts = await provider.GetEntriesForLocationsShortTerm(locations);

                dictResult = locationForecasts.ToDictionary(kvp => kvp.Key, kvp => new ForecastEntry(kvp.Value));

                return dictResult;
            }
            else
            {
                //TODO: check end time doesn't exceed 16 days
                //bool exceed = trip.StartDateTime.AddSeconds(trip.Duration) > DateTime.Now.Date.AddDays(16);
                var locations = GetLocationsInIntervalsWithTime(trip);
                var locationForecasts = await provider.GetEntriesForLocationsLongTerm(locations);

                dictResult = locationForecasts.ToDictionary(kvp => kvp.Key, kvp => new ForecastEntry(kvp.Value));

                return dictResult;
            }
        }

     
        /// <summary>
        /// Returns locations in intervals for the trip with estimated time.
        /// </summary>
        /// <param name="trip">Trip</param>
        /// <returns>List of locations with time</returns>
        private List<LocationDetail> GetLocationsInIntervalsWithTime(Trip trip)
        {
            if (trip == null)
            {
                throw new ArgumentNullException("Trip is null");
            }

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

        //Use this wrapper method in unit test.
        //This makes the actual call to the private method "GetLocationsInIntervalsWithTime"
        public List<LocationDetail> Call_GetLocationsInIntervalsWithTime(Trip trip)
        {
            return GetLocationsInIntervalsWithTime(trip);
        }

        private List<Location> SelectLocationsInIntervals(IList<Location> locations, int stepLength)
        {
            return locations.Where((x, i) => i % stepLength == 0).ToList();
        }

        //Use this wrapper method in unit test.
        //This makes the actual call to the private method "SelectLocationsInIntervals"
        public List<Location> Call_SelectLocationsInIntervals(IList<Location> locations, int stepLength)
        {
            return SelectLocationsInIntervals(locations, stepLength);
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

        //Use this wrapper method in unit test.
        //This makes the actual call to the private method "GetTimeForLocations"
        public List<LocationDetail> Call_GetTimeForLocations(IList<Location> selectedLocations, int stepDuration, DateTime start)
        {
            return GetTimeForLocations(selectedLocations,stepDuration,start);
        }
    }
}