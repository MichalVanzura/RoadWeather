using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Web;
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

        /* This one is probably totally bad, but I do not know how to test, that the result is equal
         * [TestMethod]
        public void Test_GetForecastLongTerm_IsEqual()
        {
            //Arrange
            Location location = new Location();
            WeatherProvider provider = new WeatherProvider();

            var webClient = new System.Net.WebClient();
            var json = webClient.DownloadStringTaskAsync("http://api.openweathermap.org/data/2.5/forecast/daily?lat={0}&lon={1}&APPID=3e498b17220c9b49140ea1bb8a94c010&units=metric&cnt=16");
           
            //Act
            Task<ForecastLongTerm> result = provider.GetForecastLongTerm(location);
            var myResult = JsonConvert.DeserializeObject<ForecastLongTerm>(json);

            //Assert
            Assert.Equals(result.ToString(),myResult.ToString());
        }*/

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
