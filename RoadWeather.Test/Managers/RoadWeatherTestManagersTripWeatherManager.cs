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

        [TestMethod]
        public void Test_GetForecastForTrip_Return()
        {

        }

        #endregion

        #region GetShortTermForecastForTrip
        #endregion


        #region GetLongTermForecastForTrip
        #endregion

        #region GetLocationsInIntervalsWithTime
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
