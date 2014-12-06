using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoadWeather.Managers;
using RoadWeather.Managers.Interfaces;
using RoadWeather.Models;
using RoadWeather.Test.Fakes;
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
            //Arrange
            DateTime dtNow = new DateTime(2014, 12, 6, 18, 40, 0);
            var weatherUtils = new WeatherUtils(new StaticClock(dtNow));

            //Act
            weatherUtils.AvailableForShortTermForecast(null);

        }
        [TestMethod]
        public void Test_AvailableForShortTermForecast_False()
        {
            //Arrange
            var dtNow = new DateTime(2014, 10, 1, 15, 0, 0);
            var dtStart = new DateTime(2014, 10, 7, 17, 0, 0);

            IClock staticClock = new StaticClock(dtNow);
            IWeatherUtils weatherUtils = new WeatherUtils(staticClock);

            var trip = new Trip();
            trip.StartDateTime = dtStart;
            trip.Duration = 3600; // one hour

            //Act
            bool available = weatherUtils.AvailableForShortTermForecast(trip);

            //Assert    
            Assert.IsFalse(available);



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

            IClock staticClock = new StaticClock(dtNow);
            IWeatherUtils weatherUtils = new WeatherUtils(staticClock);

            //Act
            bool available = weatherUtils.AvailableForShortTermForecast(trip);

            //Assert
            Assert.IsTrue(available);

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

            IClock staticClock = new StaticClock(dtNow);
            IWeatherUtils weatherUtils = new WeatherUtils(staticClock);

            //Act
            bool available = weatherUtils.AvailableForShortTermForecast(trip);

            //Assert
            Assert.IsTrue(available);
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

            IClock staticClock = new StaticClock(dtNow);
            IWeatherUtils weatherUtils = new WeatherUtils(staticClock);

            //Act
            bool available = weatherUtils.AvailableForShortTermForecast(trip);

            //Assert
            Assert.IsFalse(available);
        }


        #endregion


        #region SelectShortTermEntry

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_SelectShortTermEntry_Argument1Null()
        {
            IWeatherUtils weatherUtils = new WeatherUtils(new SystemClock());

            var fc = new ForecastShortTerm();
            weatherUtils.SelectShortTermEntry(null, fc);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_SelectShortTermEntry_Argument2Null()
        {
            IWeatherUtils weatherUtils = new WeatherUtils(new SystemClock());

            var loc = new LocationDetail();
            weatherUtils.SelectShortTermEntry(loc, null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Test_SelectShortTermEntry_NoEntries()
        {
            IWeatherUtils weatherUtils = new WeatherUtils(new SystemClock());

            var loc = new LocationDetail();
            var fc = new ForecastShortTerm();
            fc.Entries = new List<ForecastShortTermEntry>();

            weatherUtils.SelectShortTermEntry(loc, fc);
        }

        [TestMethod]
        public void Test_SelectShortTermEntry_FirstOne()
        {
            //Arrange
            IWeatherUtils weatherUtils = new WeatherUtils(new SystemClock());

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
            var entry = weatherUtils.SelectShortTermEntry(locDetail, forecast);

            //Assert - entry1 should be selected
            Assert.AreEqual(978361200, entry.UnixTimestamp);
        }
        
        [TestMethod]
        public void Test_SelectShortTermEntry_SecondOne()
        {
            //Arrange
            IWeatherUtils weatherUtils = new WeatherUtils(new SystemClock());

            var forecast = new ForecastShortTerm();

            var entry1 = new ForecastShortTermEntry();
            entry1.UnixTimestamp = 978361200; //2001/01/01 15:00:00
            var entry2 = new ForecastShortTermEntry();
            entry2.UnixTimestamp = 978368400; //2001/01/01 17:00:00

            forecast.Entries = new List<ForecastShortTermEntry>() { entry1, entry2 };

            var locDetail = new LocationDetail();
            locDetail.Time = new DateTime(2001, 01, 01, 16, 0, 1); //978364801

            //Act
            var entry = weatherUtils.SelectShortTermEntry(locDetail, forecast);

            //Assert - entry2 should be selected
            Assert.AreEqual(978368400, entry.UnixTimestamp);
        }

        #endregion


        #region SelectLongTermEntry

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_SelectLongTermEntry_Argument1Null()
        {
            IWeatherUtils weatherUtils = new WeatherUtils(new SystemClock());

            var fc = new ForecastLongTerm();
            weatherUtils.SelectLongTermEntry(null, fc);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_SelectLongTermEntry_Argument2Null()
        {
            IWeatherUtils weatherUtils = new WeatherUtils(new SystemClock());
            
            var loc = new LocationDetail();
            weatherUtils.SelectShortTermEntry(loc, null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Test_SelectLongTermEntry_NoEntries()
        {
            IWeatherUtils weatherUtils = new WeatherUtils(new SystemClock());

            LocationDetail loc = new LocationDetail();
            var fc = new ForecastLongTerm();
            fc.Entries = new List<ForecastDailyEntry>();
            weatherUtils.SelectLongTermEntry(loc, fc);
        }


        [TestMethod]
        public void Test_SelectLongTermEntry_FirstOne()
        {
            //Arrange
            IWeatherUtils weatherUtils = new WeatherUtils(new SystemClock());

            var forecast = new ForecastLongTerm();

            var entry1 = new ForecastDailyEntry();
            entry1.UnixTimestamp = 1335913200; //2012/05/01 23:00
            var entry2 = new ForecastDailyEntry();
            entry2.UnixTimestamp = 1335927600; //2012/05/02 03:00

            forecast.Entries = new List<ForecastDailyEntry>() { entry1, entry2 };

            var locDetail = new LocationDetail();
            locDetail.Time = new DateTime(2012, 5, 2, 0, 0, 0); //1335916800

            //Act
            var entry = weatherUtils.SelectLongTermEntry(locDetail, forecast);

            //Assert - entry2 should be selected
            Assert.AreEqual(1335927600, entry.UnixTimestamp);
        }

        [TestMethod]
        public void Test_SelectLongTermEntry_SecondOne()
        {
            //Arrange
            IWeatherUtils weatherUtils = new WeatherUtils(new SystemClock());
            
            var forecast = new ForecastLongTerm();

            var entry1 = new ForecastDailyEntry();
            entry1.UnixTimestamp = 1335913200; //2012/05/01 23:00
            var entry2 = new ForecastDailyEntry();
            entry2.UnixTimestamp = 1335927600; //2012/05/02 03:00

            forecast.Entries = new List<ForecastDailyEntry>() { entry1, entry2 };

            var locDetail = new LocationDetail();
            locDetail.Time = new DateTime(2012, 5, 1, 0, 0, 0); 

            //Act
            var entry = weatherUtils.SelectLongTermEntry(locDetail, forecast);

            //Assert - entry2 should be selected
            Assert.AreEqual(1335913200, entry.UnixTimestamp);
        }

        #endregion
    }
}
