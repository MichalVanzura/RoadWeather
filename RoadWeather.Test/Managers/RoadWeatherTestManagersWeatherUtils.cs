using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoadWeather.Managers;
using RoadWeather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadWeather.Test.Managers
{
    /// <summary>
    /// Test class covering static class WeatherUtils
    /// </summary>
    [TestClass]
    public class RoadWeatherTestManagersWeatherUtils
    {

        #region AvailableForShortTermForecast

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Test_AvailableForShortTermForecast_TripNull()
        {
            WeatherUtils.AvailableForShortTermForecast(null);
        }

        [TestMethod]
        public void Test_AvailableForShortTermForecast_False()
        {
            //Arrange
            var dtNow = new DateTime(2014, 10, 01, 15, 0, 0); 
            var dtStart = new DateTime(2014, 10, 7, 17, 0, 0);

            var trip = new Trip();
            trip.StartDateTime = dtStart;
            trip.Duration =  3600; // one hour

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return dtNow; };
                
                //Act
                bool available = WeatherUtils.AvailableForShortTermForecast(trip);
                
                Assert.IsFalse(available);
            }

        }

        [TestMethod]
        public void Test_AvailableForShortTermForecast_True()
        {
            //Arrange
            var dtNow = new DateTime(2014, 10, 1, 15, 0, 0);
            var dtStart = new DateTime(2014, 10, 4, 17, 0, 0);

            var trip = new Trip();
            trip.StartDateTime = dtStart;
            trip.Duration = 3600; // one hour

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return dtNow; };

                //Act
                bool available = WeatherUtils.AvailableForShortTermForecast(trip);

                Assert.IsTrue(available);
            }

        }

        [TestMethod]
        public void Test_AvailableForShortTermForecast_UnderBy_15Minutes()
        {
            //Arrange
            var dtNow = new DateTime(2014, 10, 1, 15, 0, 0);
            var dtStart = new DateTime(2014, 10, 5, 23, 30, 0);

            var trip = new Trip();
            trip.StartDateTime = dtStart;
            trip.Duration = 900; // 15 minutes

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return dtNow; };
                
                //Act
                bool available = WeatherUtils.AvailableForShortTermForecast(trip);

                Assert.IsTrue(available);
            }
        }

        [TestMethod]
        public void Test_AvailableForShortTermForecast_OverBy_30Minutes()
        {
            //Arrange
            var dtNow = new DateTime(2014, 10, 1, 15, 0, 0);
            var dtStart = new DateTime(2014, 10, 5, 23, 30, 0);

            var trip = new Trip();
            trip.StartDateTime = dtStart;
            trip.Duration = 3600; // 60 minutes

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return dtNow; };

                //Act
                bool available = WeatherUtils.AvailableForShortTermForecast(trip);

                Assert.IsFalse(available);
            }
        }


        #endregion



        #region SelectShortTermEntry

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_SelectShortTermEntry_Argument1Null()
        {
            var fc = new ForecastShortTerm();
            WeatherUtils.SelectShortTermEntry(null, fc);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_SelectShortTermEntry_Argument2Null()
        {
            var loc = new LocationDetail();
            WeatherUtils.SelectShortTermEntry(loc, null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Test_SelectShortTermEntry_NoEntries()
        {
            var loc = new LocationDetail();
            var fc = new ForecastShortTerm();
            fc.Entries = new List<ForecastShortTermEntry>();
            WeatherUtils.SelectShortTermEntry(loc, fc);
        }

        [TestMethod]
        public void Test_SelectShortTermEntry_FirstOne()
        {
            //Arrange
            var forecast = new ForecastShortTerm();

            var entry1 = new ForecastShortTermEntry();
            entry1.UnixTimestamp = 978361200; //2001/01/01 15:00:00
            var entry2 = new ForecastShortTermEntry();
            entry2.UnixTimestamp = 978368400; //2001/01/01 17:00:00

            forecast.Entries = new List<ForecastShortTermEntry>() { entry1, entry2 };

            var locDetail = new LocationDetail()
            {
                Time = new DateTime(2001, 01, 01, 15, 59, 59) //978364799
            };

            //Act
            var entry = WeatherUtils.SelectShortTermEntry(locDetail, forecast);

            //Assert - entry1 should be selected
            Assert.AreEqual(978361200, entry.UnixTimestamp);
        }

        [TestMethod]
        public void Test_SelectShortTermEntry_SecondOne()
        {
            //Arrange
            var forecast = new ForecastShortTerm();

            var entry1 = new ForecastShortTermEntry();
            entry1.UnixTimestamp = 978361200; //2001/01/01 15:00:00
            var entry2 = new ForecastShortTermEntry();
            entry2.UnixTimestamp = 978368400; //2001/01/01 17:00:00

            forecast.Entries = new List<ForecastShortTermEntry>() { entry1, entry2 };

            var locDetail = new LocationDetail();
            locDetail.Time = new DateTime(2001, 01, 01, 16, 0, 1); //978364801

            //Act
            var entry = WeatherUtils.SelectShortTermEntry(locDetail, forecast);

            //Assert - entry2 should be selected
            Assert.AreEqual(978368400, entry.UnixTimestamp);
        }

        #endregion


        #region SelectLongTermEntry

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_SelectLongTermEntry_Argument1Null()
        {
            var fc = new ForecastLongTerm();
            WeatherUtils.SelectLongTermEntry(null, fc);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_SelectLongTermEntry_Argument2Null()
        {
            var loc = new LocationDetail();
            WeatherUtils.SelectShortTermEntry(loc, null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Test_SelectLongTermEntry_NoEntries()
        {
            LocationDetail loc = new LocationDetail();
            var fc = new ForecastLongTerm();
            fc.Entries = new List<ForecastDailyEntry>();
            WeatherUtils.SelectLongTermEntry(loc, fc);
        }


        [TestMethod]
        public void Test_SelectLongTermEntry_FirstOne()
        {
            //Arrange
            var forecast = new ForecastLongTerm();

            var entry1 = new ForecastDailyEntry();
            entry1.UnixTimestamp = 1335913200; //2012/05/01 23:00
            var entry2 = new ForecastDailyEntry();
            entry2.UnixTimestamp = 1335927600; //2012/05/02 03:00

            forecast.Entries = new List<ForecastDailyEntry>() { entry1, entry2 };

            var locDetail = new LocationDetail();
            locDetail.Time = new DateTime(2012, 5, 2, 0, 0, 0); //1335916800

            //Act
            var entry = WeatherUtils.SelectLongTermEntry(locDetail, forecast);

            //Assert - entry2 should be selected
            Assert.AreEqual(1335927600, entry.UnixTimestamp);
        }

        [TestMethod]
        public void Test_SelectLongTermEntry_SecondOne()
        {
            //Arrange
            var forecast = new ForecastLongTerm();

            var entry1 = new ForecastDailyEntry();
            entry1.UnixTimestamp = 1335913200; //2012/05/01 23:00
            var entry2 = new ForecastDailyEntry();
            entry2.UnixTimestamp = 1335927600; //2012/05/02 03:00

            forecast.Entries = new List<ForecastDailyEntry>() { entry1, entry2 };

            var locDetail = new LocationDetail();
            locDetail.Time = new DateTime(2012, 5, 1, 0, 0, 0); 

            //Act
            var entry = WeatherUtils.SelectLongTermEntry(locDetail, forecast);

            //Assert - entry2 should be selected
            Assert.AreEqual(1335913200, entry.UnixTimestamp);
        }




        #endregion
    }
}
