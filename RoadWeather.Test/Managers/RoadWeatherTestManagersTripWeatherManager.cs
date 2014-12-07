using System;
using RoadWeather.Models;
using RoadWeather.Managers;
using RoadWeather.Managers.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Microsoft.QualityTools.Testing.Fakes;
using Moq;
using RoadWeather.Test.Fakes;

namespace RoadWeather.Test.Managers
{
    [TestClass]
    public class RoadWeatherTestManagersTripWeatherManager
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
        [ExpectedException(typeof(System.ArgumentNullException))]
        public async Task Test_GetForecastForTrip_TripNull()
        {
            //Arrange

            IClock iClock = new SystemClock();
            IWeatherProvider weatherProvider = new WeatherProvider();
            IWeatherUtils weatherUtils = new WeatherUtils(iClock);
            ILocationWeatherManager locationWeatherManager = new LocationWeatherManager(weatherProvider, weatherUtils);
            ITripIntervalManager tripIntervalManager = new TripIntervalManager();
            ITripWeatherManager tripWeatherManager = new TripWeatherManager(locationWeatherManager, weatherUtils, tripIntervalManager);

            //Act and Assert
            var result = await tripWeatherManager.GetForecastForTrip(null);
            
        }

        [TestMethod]
        public async Task Test_GetForecastForTrip_ForShortTerm_Return()
        {
            //Arrange
            var trip = CreateTrip();

            ILocationWeatherManager mockLocationWeatherManager = new MockLocationWeatherManager();
            
            var mockWeatherUtils = new Mock<IWeatherUtils>();
            mockWeatherUtils.Setup(t => t.AvailableForShortTermForecast(It.IsAny<Trip>())).Returns(true);

            var mockTripIntervalManager = new Mock<ITripIntervalManager>();

            var dt = new DateTime(2012, 12, 1, 12, 0, 0);
            var locDetailList = new List<LocationDetail>();
            for (var i = 0; i < 10; i++)
            {
                LocationDetail loc = new LocationDetail();
                loc.Location = new Location()
                {
                    Longitude = i,
                    Latitude = i
                };
                loc.Time = dt.AddSeconds(i * 60);
                locDetailList.Add(loc);
            }
            mockTripIntervalManager.Setup(t => t.GetLocationsInIntervalsWithTime(It.IsAny<Trip>()))
                .Returns(locDetailList);

            ITripWeatherManager tripWeatherManager = new TripWeatherManager(mockLocationWeatherManager, mockWeatherUtils.Object, mockTripIntervalManager.Object);
            
            var expectedEntry =
                new ForecastEntry(new MockWeatherUtils().SelectShortTermEntry(new LocationDetail(), new ForecastShortTerm()));
            
            //Act and Assert
            var result = await tripWeatherManager.GetForecastForTrip(trip);

            //Assert
            Assert.AreEqual(10, result.Count);
            
            Assert.AreEqual(new Location() { Latitude = 0, Longitude = 0 } ,result.Keys.First().Location);
            Assert.AreEqual(new Location() { Latitude = 9, Longitude = 9 }, result.Keys.Last().Location);
            
            Assert.AreEqual(dt, result.Keys.First().Time);
            Assert.AreEqual(dt.AddSeconds(9*60), result.Keys.Last().Time);


            Assert.AreEqual(expectedEntry, result.Values.First());
            Assert.AreEqual(expectedEntry, result.Values.Last());
        }


        [TestMethod]
        public async Task Test_GetForecastForTrip_ForLongTerm_Return()
        {
            //Arrange
            var trip = CreateTrip();

            ILocationWeatherManager mockLocationWeatherManager = new MockLocationWeatherManager();

            var mockWeatherUtils = new Mock<IWeatherUtils>();
            mockWeatherUtils.Setup(t => t.AvailableForShortTermForecast(It.IsAny<Trip>())).Returns(false);

            var mockTripIntervalManager = new Mock<ITripIntervalManager>();

            var dt = new DateTime(2012, 12, 1, 12, 0, 0);
            var locDetailList = new List<LocationDetail>();
            for (var i = 0; i < 10; i++)
            {
                LocationDetail loc = new LocationDetail();
                loc.Location = new Location()
                {
                    Longitude = i,
                    Latitude = i
                };
                loc.Time = dt.AddSeconds(i * 60);
                locDetailList.Add(loc);
            }
            mockTripIntervalManager.Setup(t => t.GetLocationsInIntervalsWithTime(It.IsAny<Trip>()))
                .Returns(locDetailList);

            ITripWeatherManager tripWeatherManager = new TripWeatherManager(mockLocationWeatherManager, mockWeatherUtils.Object, mockTripIntervalManager.Object);

            var expectedEntry =
                new ForecastEntry(new MockWeatherUtils().SelectLongTermEntry(new LocationDetail(), new ForecastLongTerm()));

            //Act and Assert
            var result = await tripWeatherManager.GetForecastForTrip(trip);

            //Assert
            Assert.AreEqual(10, result.Count);

            Assert.AreEqual(new Location() { Latitude = 0, Longitude = 0 }, result.Keys.First().Location);
            Assert.AreEqual(new Location() { Latitude = 9, Longitude = 9 }, result.Keys.Last().Location);

            Assert.AreEqual(dt, result.Keys.First().Time);
            Assert.AreEqual(dt.AddSeconds(9 * 60), result.Keys.Last().Time);


            Assert.AreEqual(expectedEntry, result.Values.First());
            Assert.AreEqual(expectedEntry, result.Values.Last());
        }



    }
}
