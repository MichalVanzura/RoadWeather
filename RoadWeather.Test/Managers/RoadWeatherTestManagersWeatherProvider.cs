using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Web;
using RoadWeather.Managers;
using RoadWeather.Models;
using RoadWeather.Managers.Interfaces;
using RoadWeather.Test.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RoadWeather.Test.Managers
{
    [TestClass]
    public class RoadWeatherTestManagersWeatherProvider
    {


        #region GetForecastLongTerm
        [TestMethod]
        public void Test_GetForecastLongTerm_Return()
        {
            //Arrange
            Location location = new Location();
            IWeatherProvider provider = new WeatherProvider();

            //Act and Assert
            Assert.IsInstanceOfType(provider.GetForecastLongTerm(location), typeof(Task<ForecastLongTerm>));
        }
        /*
        [TestMethod]
        public async Task Test_GetForecastLongTerm_IsEqual()
        {
            //Arrange
            Location location = new Location();
            location.Latitude = 49.2;
            location.Longitude = 16.6;

            IWeatherProvider provider = new WeatherProvider();
            MockWeatherProvider mockProvider = new MockWeatherProvider();

            //Act
            ForecastLongTerm result = await provider.GetForecastLongTerm(location);
            ForecastLongTerm expectedResult = await mockProvider.GetForecastLongTerm(location);

            //Assert
            Assert.AreEqual(result.Entries.Count, expectedResult.Entries.Count);
        }*/

        #endregion

        #region GetForecastShortTerm

        [TestMethod]

        public void Test_GetForecastShortTerm_Return()
        {
            //Arrange
            Location location = new Location();
            IWeatherProvider provider = new WeatherProvider();

            //Act and Assert
            Assert.IsInstanceOfType(provider.GetForecastShortTerm(location), typeof(Task<ForecastShortTerm>));
        }

        /// TODO: we would need to create fake service that would return static result json that could be tested...
        
/*
        [TestMethod]
        public async Task Test_GetForecastShortTerm_IsEqual()
        {
            //Arrange
            Location location = new Location();
            location.Latitude = 49.2;
            location.Longitude = 16.6;

            IWeatherProvider provider = new WeatherProvider();
            MockWeatherProvider mockProvider = new MockWeatherProvider();

            //Act
            ForecastShortTerm result = await provider.GetForecastShortTerm(location);
            ForecastShortTerm expectedResult = await mockProvider.GetForecastShortTerm(location);

            //Assert
            Assert.AreEqual(result.Entries.Count, expectedResult.Entries.Count);
        }*/
        #endregion
    }
}
