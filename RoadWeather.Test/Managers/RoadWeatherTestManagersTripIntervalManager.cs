using System;
using RoadWeather.Managers;
using RoadWeather.Managers.Interfaces;
using RoadWeather.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RoadWeather.Test.Managers
{
    [TestClass]
    public class RoadWeatherTestManagersTripIntervalManager
    {

        #region GetLocationsInIntervalsWithTime

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Test_GetLocationsInIntervalsWithTime_TripNull()
        {
            ITripIntervalManager tripIntervalManager = new TripIntervalManager();

            tripIntervalManager.GetLocationsInIntervalsWithTime(null);

        }

        [TestMethod]
        public void Test_GetLocationsInIntervalsWithTime_ListCount()
        {
            //Arrange
            Location location = new Location();

            List<Location> list = new List<Location>();

            for (int i = 0; i < 200; i++)
            {
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
            Assert.AreEqual(result.Count, 10);
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


        #endregion

    }

}
