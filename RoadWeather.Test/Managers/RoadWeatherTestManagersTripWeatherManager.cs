using System;
using RoadWeather.Models;
using RoadWeather.Managers;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RoadWeather.Test.Managers
{
    [TestClass]
    public class RoadWeatherTestManagersTripWeatherManager
    {

        #region GetForecastForTrip
        #endregion

        #region GetShortTermForecastForTrip
        #endregion


        #region GetLongTermForecastForTrip
        #endregion

        #region GetLocationsInIntervalsWithTime

        [TestMethod]
        public void Test_GetLocationsInIntervalsWithTime_Return()
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

            TripWeatherManager temp = new TripWeatherManager();
            //Act
            List<LocationDetail> result = temp.Call_GetLocationsInIntervalsWithTime(trip);

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<LocationDetail>));
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

            TripWeatherManager temp = new TripWeatherManager();
            //Act
            List<LocationDetail> result = temp.Call_GetLocationsInIntervalsWithTime(trip);

            //Assert
            Assert.AreEqual(result.Count, 10);
        }

        [TestMethod]
        public void Test_GetLocationsInIntervalsWithTime_ListValue()
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

            TripWeatherManager temp = new TripWeatherManager();
            //Act
            List<LocationDetail> result = temp.Call_GetLocationsInIntervalsWithTime(trip);

            //Assert
            Assert.AreEqual(result[2].Time, date.AddSeconds(2*stepDuration));
        }

        #endregion

        #region SelectLocationsInIntervals


        [TestMethod]
        public void Test_SelectLocationsInIntervals_Return()
        {

            //Arrange
            Location location = new Location();
            List<Location> list = new List<Location>();

            for (int i = 0; i < 200; i++)
            {
                list.Add(location);
            }

            TripWeatherManager temp = new TripWeatherManager();

            //Act
            List<Location> result = temp.Call_SelectLocationsInIntervals(list, list.Count()/10);


            //Assert
            Assert.IsInstanceOfType(result, typeof(List<Location>));
        }


        [TestMethod]
        public void Test_SelectLocationsInIntervals_ListCount()
        {

            //Arrange
            Location location = new Location();
            List<Location> list = new List<Location>();

            for (int i = 0; i < 200; i++)
            {
                list.Add(location);
            }

            TripWeatherManager temp = new TripWeatherManager();

            //Act
            List<Location> result = temp.Call_SelectLocationsInIntervals(list, list.Count() / 10);


            //Assert
            Assert.AreEqual(result.Count(), 10);
        }

        /* I was expected, that there will be 200 locations in the list and each will have unique 
         * longitude and latitude in range <0..199>, but this is not working.
         * In the end there are 200 locations and every of them has latitude and longitude 199
        [TestMethod]
        public void Test_SelectLocationsInIntervals_ListValue()
        {

            //Arrange
            Location location = new Location();
            List<Location> list = new List<Location>();

            for (int i = 0; i < 200; i++)
            {
                location.Latitude = i;
                location.Longitude = i;
                list.Add(location);
            }

            TripWeatherManager temp = new TripWeatherManager();

            //Act
            List<Location> result = temp.Call_SelectLocationsInIntervals(list, list.Count() / 10);


            //Assert
            Assert.AreEqual(list[20].Latitude, 19);
        }*/


        #endregion

        #region GetTimeForLocations
        [TestMethod]
        public void Test_GetTimeForLocations_ReturnType()
        {
            //Arrange
            DateTime date = new DateTime(2014, 12, 01);
            int stepDuration = 10800; // 180min
            Location location = new Location();

            List<Location> list = new List<Location>();

            for (int i = 0; i < 20; i++)
            {
                list.Add(location);
            }
            
            TripWeatherManager temp = new TripWeatherManager();

            //Act
            List<LocationDetail> result = temp.Call_GetTimeForLocations(list, stepDuration, date);

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<LocationDetail>));
        }

        [TestMethod]
        public void Test_GetTimeForLocations_ListCount()
        {
            //Arrange
            DateTime date = new DateTime(2014, 12, 01, 08, 00, 00); // 1.12.2014 8:00
            int stepDuration = 10800; // 180min
            Location location = new Location();

            List<Location> list = new List<Location>();

            for (int i = 0; i < 20; i++)
            {
                list.Add(location);
            }

            TripWeatherManager temp = new TripWeatherManager();

            //Act
            List<LocationDetail> result = temp.Call_GetTimeForLocations(list, stepDuration, date);

            //Assert
            Assert.AreEqual(result.Count, 20);
        }

        [TestMethod]
        public void Test_GetTimeForLocations_ListValue()
        {
            //Arrange
            DateTime date = new DateTime(2014, 12, 01, 14, 00, 00); // 1.12.2014 8:00
            int stepDuration = 10800; // 180min
            Location location = new Location();

            List<Location> list = new List<Location>();

            for (int i = 0; i < 20; i++)
            {
                list.Add(location);
            }

            TripWeatherManager temp = new TripWeatherManager();

            //Act
            List<LocationDetail> result = temp.Call_GetTimeForLocations(list, stepDuration, date);

            //Assert
            Assert.AreEqual(result[10].Time, date.AddSeconds(10800*10));
        }
        #endregion


    }
}
