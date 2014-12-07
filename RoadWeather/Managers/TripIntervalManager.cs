using log4net;
using RoadWeather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadWeather.Managers
{
    public class TripIntervalManager : RoadWeather.Managers.Interfaces.ITripIntervalManager
    {

        private const int INTERVAL_COUNT = 10;
        private static readonly ILog log = LogManager.GetLogger("TripIntervalManager");


        /// <summary>
        /// Returns locations in intervals for the trip with estimated time.
        /// </summary>
        /// <param name="trip">Trip</param>
        /// <returns>List of locations with time</returns>
        public List<LocationDetail> GetLocationsInIntervalsWithTime(Trip trip)
        {
            if (trip == null) throw new ArgumentNullException("Trip is null");
            if (trip.Locations == null) throw new ArgumentNullException("Trip.Locations is null");
            if (!trip.Locations.Any()) throw new  ArgumentException("Locations count is 0");
            if (trip.Duration <= 0) throw  new ArgumentException("Duration cannot be less or eqal to 0");

            var locations = trip.Locations;
            int stepLength = locations.Count() / (INTERVAL_COUNT);

            double stepSizeRatio = (double)stepLength / (double)locations.Count();
            int stepDuration = (int)(stepSizeRatio * trip.Duration);
            log.Debug(string.Format("Expected duration: {0} min", (int)(trip.Duration / 60)));
            log.Debug(string.Format("Estimated step duration: {0} min", (int)(stepDuration / 60)));

            var selectedLocations = SelectLocationsInIntervals(trip.Locations.ToList(), stepLength);
            var locationsWithTime = GetTimeForLocations(selectedLocations, stepDuration, trip.StartDateTime);
            return locationsWithTime;
        }

        private List<LocationDetail> GetTimeForLocations(IList<Location> selectedLocations, double stepDuration, DateTime start)
        {
            var list = new List<LocationDetail>();

            int counter = 0;
            foreach (Location loc in selectedLocations)
            {
                DateTime dt = start.AddSeconds(Math.Round(counter * stepDuration));
                list.Add(new LocationDetail() { Location = loc, Time = dt });
                counter++;
            }
            return list;
        }

        private List<Location> SelectLocationsInIntervals(IList<Location> locations, int stepLength)
        {
            return locations.Where((x, i) => i % stepLength == 0).ToList();
        }
    }
}