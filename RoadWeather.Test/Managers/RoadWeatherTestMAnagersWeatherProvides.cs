using System;
using System.Threading.Tasks;
using RoadWeather.Managers;
using RoadWeather.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RoadWeather.Test.Managers
{
    [TestClass]
    public class RoadWeatherTestManagersWeatherProvides
    {

        #region GetForecastLongTerm

        [TestMethod]
        public void Test_GetForecastLongTerm_Return()
        {
            //Arrange
            Location location = new Location();
            WeatherProvider provider = new WeatherProvider();

            //Act
            Task<ForecastLongTerm> result = provider.GetForecastLongTerm(location);

            //Assert
            Assert.IsInstanceOfType(result, typeof(Task<ForecastLongTerm>));
        }
        #endregion

        #region GetForecastShortTerm
        [TestMethod]
        public void Test_GetForecastShortTerm_Return()
        {
            //Arrange
            Location location = new Location();
            WeatherProvider provider = new WeatherProvider();

            //Act
            Task<ForecastShortTerm> result = provider.GetForecastShortTerm(location);

            //Assert
            Assert.IsInstanceOfType(result, typeof(Task<ForecastShortTerm>));
        }
        #endregion
    }
}
