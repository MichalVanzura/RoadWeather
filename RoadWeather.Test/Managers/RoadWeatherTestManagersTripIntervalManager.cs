using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RoadWeather.Managers;
using RoadWeather.Managers.Interfaces;
using RoadWeather.Models;

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
            trip.StartDateTime = new DateTime(2014,12,01,12,0,0);
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
        /*
        [TestMethod]
        public void Test_GetLocationsInIntervalsWithTime_Returns2()
        {
            //Arrange
            ITripIntervalManager intervalManager = new TripIntervalManager();
            var trip = CreateTrip();
            var locations = new List<Location>();
            for (var i = 0; i < 101; i++)
            {
                var location = new Location { Latitude = i, Longitude = i };
                locations.Add(location);
            }
            trip.Locations = locations;
            trip.Duration = 1000;

            // Math.Round(locations.count / INTERVAL_COUNT / locations.count * trip.Duration) * 9 iterations)
            var expected = Math.Round((double)
                ((((double) ((int)(101 / 10)))/ ((double) 101)) * 1000) * 9);

            //Act
            var result = intervalManager.GetLocationsInIntervalsWithTime(trip);
            var resultFirst = result.First();
            var resultLast = result.Last();
            //Assert
            var startTime = new DateTime(2014, 12, 01, 12, 0, 0);
    
            //((101/10)101*1000*9) = 
            Assert.AreEqual(new Location() { Latitude = 0, Longitude = 0 }, resultFirst.Location );
            Assert.AreEqual( new Location() { Latitude = 100, Longitude = 100 }, resultLast.Location);
            Assert.AreEqual(startTime.AddSeconds(0), resultFirst.Time);
            Assert.AreEqual(startTime.AddSeconds(expected), resultLast.Time);

        }
        */




    }
}
