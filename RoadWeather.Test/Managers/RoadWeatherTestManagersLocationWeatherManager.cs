using System;
using System.Linq;
using RoadWeather.Managers;
using RoadWeather.Models;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoadWeather.Managers.Interfaces;
using RoadWeather.Test.Fakes;

namespace RoadWeather.Test.Managers
{
    [TestClass]
    public class RoadWeatherTestManagersLocationWeatherManager
    {

        #region GetEntriesForLocationsLongTerm

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Test_GetEntriesForLocationsLongTerm_ArgumentNullEx()
        {
            //Arrange
            IWeatherProvider mockWeatherProvider = new MockWeatherProvider();
            IWeatherUtils mockWeatherUtils = new MockWeatherUtils();

            ILocationWeatherManager locationWeatherManager = new LocationWeatherManager(
                mockWeatherProvider, mockWeatherUtils);

            //Act
            await locationWeatherManager.GetEntriesForLocationsLongTerm(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task Test_GetEntriesForLocationsLongTerm_ArgumentEx()
        {
            //Arrange
            IWeatherProvider mockWeatherProvider = new MockWeatherProvider();
            IWeatherUtils mockWeatherUtils = new MockWeatherUtils();

            ILocationWeatherManager locationWeatherManager = new LocationWeatherManager(
                mockWeatherProvider, mockWeatherUtils);

            //Act
            await locationWeatherManager.GetEntriesForLocationsLongTerm(new List<LocationDetail>());
        }


        [TestMethod]
        public async Task Test_GetEntriesForLocationsLongTerm_Return()
        {
            //Arrange
            IWeatherProvider mockWeatherProvider = new MockWeatherProvider();
            IWeatherUtils mockWeatherUtils = new MockWeatherUtils();

            ILocationWeatherManager locationWeatherManager = new LocationWeatherManager(
                mockWeatherProvider, mockWeatherUtils);


            var date = new DateTime(2014, 12, 01, 08, 00, 00);

            var location0 = new Location { Latitude = 0, Longitude = 0 };
            var locDetail0 = new LocationDetail { Location = location0, Time = date };

            var location1 = new Location { Latitude = 1, Longitude = 1 };
            var locDetail1 = new LocationDetail { Location = location1, Time = date };

            var locationDetailList = new List<LocationDetail> { locDetail0, locDetail1 };

            var expectedEntry0 = (await mockWeatherProvider.GetForecastLongTerm(location0)).Entries.FirstOrDefault();
            var expectedEntry1 = (await mockWeatherProvider.GetForecastLongTerm(location1)).Entries.FirstOrDefault();

            var expectedDict = new Dictionary<LocationDetail, ForecastDailyEntry>
            {
                {locDetail0, expectedEntry0},
                {locDetail1, expectedEntry1}
            };

            //Act
            var result = await locationWeatherManager.GetEntriesForLocationsLongTerm(locationDetailList);

            var key0 = result.First().Key;
            var value0 = result.First().Value;

            var key1 = result.Last().Key;
            var value1 = result.Last().Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Dictionary<LocationDetail, ForecastDailyEntry>));
            Assert.AreEqual(result.Count, 2);

//            Assert.AreEqual(key0, locDetail0);
//            Assert.AreEqual(key1, locDetail1);
//
//            Assert.AreEqual(value0, expectedEntry0);
//            Assert.AreEqual(value1, expectedEntry1);

            Assert.IsTrue(result.SequenceEqual(expectedDict));

        }

        #endregion




        #region GetEntriesForLocationsShortTerm

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Test_GetEntriesForLocationsShortTerm_ArgumentNullEx()
        {
            //Arrange
            IWeatherProvider mockWeatherProvider = new MockWeatherProvider();
            IWeatherUtils mockWeatherUtils = new MockWeatherUtils();

            ILocationWeatherManager locationWeatherManager = new LocationWeatherManager(
                mockWeatherProvider, mockWeatherUtils);

            //Act
            await locationWeatherManager.GetEntriesForLocationsShortTerm(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task Test_GetEntriesForLocationsShortTerm_ArgumentEx()
        {
            //Arrange
            IWeatherProvider mockWeatherProvider = new MockWeatherProvider();
            IWeatherUtils mockWeatherUtils = new MockWeatherUtils();

            ILocationWeatherManager locationWeatherManager = new LocationWeatherManager(
                mockWeatherProvider, mockWeatherUtils);

            //Act
            await locationWeatherManager.GetEntriesForLocationsShortTerm(new List<LocationDetail>());
        }


        [TestMethod]
        public async Task Test_GetEntriesForLocationsShortTerm_Return()
        {
            //Arrange
            IWeatherProvider mockWeatherProvider = new MockWeatherProvider();
            IWeatherUtils mockWeatherUtils = new MockWeatherUtils();

            ILocationWeatherManager locationWeatherManager = new LocationWeatherManager(
                mockWeatherProvider, mockWeatherUtils);


            var date = new DateTime(2014, 12, 01, 08, 00, 00);

            var location0 = new Location { Latitude = 0, Longitude = 0 };
            var locDetail0 = new LocationDetail { Location = location0, Time = date };

            var location1 = new Location { Latitude = 1, Longitude = 1 };
            var locDetail1 = new LocationDetail { Location = location1, Time = date };

            var locationDetailList = new List<LocationDetail> { locDetail0, locDetail1 };

            var expectedEntry0 = (await mockWeatherProvider.GetForecastShortTerm(location0)).Entries.FirstOrDefault(); ;
            var expectedEntry1 = (await mockWeatherProvider.GetForecastShortTerm(location1)).Entries.FirstOrDefault(); ;

            var expectedDict = new Dictionary<LocationDetail, ForecastShortTermEntry>
            {
                {locDetail0, expectedEntry0},
                {locDetail1, expectedEntry1}
            };

            //Act
            var result = await locationWeatherManager.GetEntriesForLocationsShortTerm(locationDetailList);

            var key0 = result.First().Key;
            var value0 = result.First().Value;

            var key1 = result.Last().Key;
            var value1 = result.Last().Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Dictionary<LocationDetail, ForecastShortTermEntry>));
            Assert.AreEqual(result.Count, 2);

//            Assert.AreEqual(key0, locDetail0);
//            Assert.AreEqual(key1, locDetail1);
//
//            Assert.AreEqual(value0, expectedEntry0);
//            Assert.AreEqual(value1, expectedEntry1);

            Assert.IsTrue(result.SequenceEqual(expectedDict));
        }

        #endregion

    }
}
