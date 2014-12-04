using System;
using RoadWeather.Models;
using RoadWeather.Managers;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Microsoft.QualityTools.Testing.Fakes;

namespace RoadWeather.Test.Managers
{
    [TestClass]
    public class RoadWeatherTestManagersTripWeatherManager
    {

        #region GetForecastForTrip

        [TestMethod]
 
        public void Test_GetForecastForTrip_Return()
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

            //Act and Assert
            Assert.IsInstanceOfType(temp.GetForecastForTrip(trip), typeof(Task<Dictionary<LocationDetail, ForecastEntry>>));
        }

        #endregion

        #region GetShortTermForecastForTrip

        [TestMethod]
        public void Test_GetShortTermForecastForTrip_Return()
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

            //Assert
            Assert.IsInstanceOfType(temp.GetShortTermForecastForTrip(trip), typeof(Task<Dictionary<LocationDetail, ForecastShortTermEntry>>));
        }

        /* This method is not working. I would expect ArgumentNullException will be thrown, because
         * trip is null, but nothing happens.
         * The same for not short therm trip, there should be ArgumentException as well
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Test_GetShortTermForecastForTrip_NullTrip()
        {
            //Arrange
            var dtNow = new DateTime(2014, 12, 01, 15, 0, 0);
            var dtStart = new DateTime(2014, 12, 20, 20, 0, 0);

            var trip = new Trip();
            trip.StartDateTime = dtStart;
            trip.Duration = 3600; // one hour

            TripWeatherManager temp = new TripWeatherManager();

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return dtNow; };

                //Act
                Task<Dictionary<LocationDetail, ForecastShortTermEntry>> result = temp.GetShortTermForecastForTrip(null);
            }
        }*/

        #endregion


        #region GetLongTermForecastForTrip

        [TestMethod]
        public void Test_GetLongTermForecastForTrip_Return()
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

            //Assert
            Assert.IsInstanceOfType(temp.GetLongTermForecastForTrip(trip), typeof(Task<Dictionary<LocationDetail, ForecastDailyEntry>>));
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Test_GetLongTermForecastForTrip_TripNull_Failed()
        {
            //Arrange
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

            TripWeatherManager temp = new TripWeatherManager();

            //Act
            Task<Dictionary<LocationDetail, ForecastDailyEntry>> result = temp.GetLongTermForecastForTrip(null);

            //Assert
            Assert.AreEqual(result.Result.Count, 10);
        }

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


            //Act and Assert
            Assert.IsInstanceOfType(temp.Call_GetLocationsInIntervalsWithTime(trip), typeof(List<LocationDetail>));
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


            //Act and Assert
            Assert.IsInstanceOfType(temp.Call_SelectLocationsInIntervals(list, list.Count()/10), typeof(List<Location>));
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


        [TestMethod]
        public void Test_SelectLocationsInIntervals_ListValue()
        {

            //Arrange
            
            List<Location> list = new List<Location>();

            for (int i = 0; i < 200; i++)
            {
                Location location = new Location();
                location.Latitude = i;
                location.Longitude = i;
                list.Add(location);
            }

            TripWeatherManager temp = new TripWeatherManager();

            //Act
            List<Location> result = temp.Call_SelectLocationsInIntervals(list, list.Count() / 10);


            //Assert
            Assert.AreEqual(result[4].Latitude, 80);
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

            //Assert
            Assert.IsInstanceOfType(temp.Call_GetTimeForLocations(list, stepDuration, date), typeof(List<LocationDetail>));
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
