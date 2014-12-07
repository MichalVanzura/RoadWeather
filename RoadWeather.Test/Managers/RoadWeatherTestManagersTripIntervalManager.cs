using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoadWeather.Managers;
using RoadWeather.Managers.Interfaces;
using RoadWeather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadWeather.Test.Managers
{
    [TestClass]
    public class RoadWeatherTestManagersTripIntervalManager
    {

        private static Trip CreateTrip()
        {
            var trip = new Trip();
            var locations = new List<Location>();
            for (var i = 0; i < 100; i++)
            {
                var location = new Location { Latitude = i, Longitude = i };
                locations.Add(location);
            }
            trip.StartDateTime = new DateTime(2014, 12, 01, 12, 0, 0);
            trip.Locations = locations;
            trip.Duration = 100;
            return trip;
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_GetLocationsInIntervalsWithTime_ArgumentNull_Trip()
        {
            //Arrange
            ITripIntervalManager intervalManager = new TripIntervalManager();
            //Act
            intervalManager.GetLocationsInIntervalsWithTime(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_GetLocationsInIntervalsWithTime_ArgumentNull_TripLocations()
        {
            //Arrange
            ITripIntervalManager intervalManager = new TripIntervalManager();
            var trip = CreateTrip();
            trip.Locations = null;
            //Act
            intervalManager.GetLocationsInIntervalsWithTime(new Trip());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_GetLocationsInIntervalsWithTime_ArgumentNull_TripLocationsCount()
        {
            //Arrange
            ITripIntervalManager intervalManager = new TripIntervalManager();
            var trip = CreateTrip();
            trip.Locations = new List<Location>();
            //Act
            intervalManager.GetLocationsInIntervalsWithTime(trip);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_GetLocationsInIntervalsWithTime_ArgumentNull_TripDuration()
        {
            //Arrange
            ITripIntervalManager intervalManager = new TripIntervalManager();
            var trip = CreateTrip();
            trip.Duration = 0;
            //Act
            intervalManager.GetLocationsInIntervalsWithTime(trip);
        }

        [TestMethod]
        public void Test_GetLocationsInIntervalsWithTime_ListCount()
        {
            //Arrange
            var location = new Location();

            var list = new List<Location>();

            for (int i = 0; i < 200; i++)
            {
                list.Add(location);
            }

            var trip = new Trip();
            trip.Locations = list;
            trip.Duration = 72000;
            trip.StartDateTime = new DateTime(2014, 12, 01, 20, 00, 00);

            ITripIntervalManager temp = new TripIntervalManager();
            //Act
            List<LocationDetail> result = temp.GetLocationsInIntervalsWithTime(trip);

            //Assert
            Assert.AreEqual(result.Count, 10);
        }

        [TestMethod]
        public void Test_GetLocationsInIntervalsWithTime_LocationEqual()
        {
            //Arrange
            DateTime date = new DateTime(2014, 12, 01, 20, 00, 00); // start datetime

            List<Location> list = new List<Location>();

            for (int i = 0; i < 200; i++)
            {
                Location location = new Location();
                location.Latitude = i;
                location.Longitude = i;
                list.Add(location);
            }

            Trip trip = new Trip();
            trip.Locations = list;
            trip.Duration = 72000;
            trip.StartDateTime = new DateTime(2014, 12, 01, 20, 00, 00);

            ITripIntervalManager temp = new TripIntervalManager();
            //Act
            List<LocationDetail> result = temp.GetLocationsInIntervalsWithTime(trip);

            //Assert
            Assert.AreEqual(result[5].Location.Latitude, list[100].Latitude = 100);
        }

        [TestMethod]
        public void Test_GetLocationsInIntervalsWithTime_TimeEqual()
        {
            //Arrange
            DateTime date = new DateTime(2014, 12, 01, 20, 00, 00); // start datetime
            int stepDuration = 7200; // 120min

            Location location = new Location();

            List<Location> list = new List<Location>();

            for (int i = 0; i < 200; i++)
            {
                location.Latitude = i;
                location.Longitude = i;
                list.Add(location);
            }

            Trip trip = new Trip();
            trip.Locations = list;
            trip.Duration = 72000;
            trip.StartDateTime = new DateTime(2014, 12, 01, 20, 00, 00);

            ITripIntervalManager temp = new TripIntervalManager();
            //Act
            List<LocationDetail> result = temp.GetLocationsInIntervalsWithTime(trip);

            //Assert
            Assert.AreEqual(result[2].Time, date.AddSeconds(2 * stepDuration));
        }

        [TestMethod]
        public void Test_GetLocationsInIntervalsWithTime_Returns()
        {
            //Arrange
            ITripIntervalManager intervalManager = new TripIntervalManager();
            var trip = CreateTrip();
            //Act
            var result = intervalManager.GetLocationsInIntervalsWithTime(trip);
            var resultFirst = result.First();
            var resultLast = result.Last();
            //Assert
            var startTime = new DateTime(2014, 12, 01, 12, 0, 0);

            Assert.AreEqual(new Location() { Latitude = 0, Longitude = 0 }, resultFirst.Location);
            Assert.AreEqual(new Location() { Latitude = 90, Longitude = 90 }, resultLast.Location);
            Assert.AreEqual(startTime.AddSeconds(0), resultFirst.Time);
            Assert.AreEqual(startTime.AddSeconds(90), resultLast.Time);
            // TODO: improve algorithm for time and locations calculation

        }

    }
}
