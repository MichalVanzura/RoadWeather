using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RoadWeather.Test.Models
{
    [TestClass]
    public class RoadWeatherTestModels
    {
        [TestMethod]
        public void Trip_require_short_term_forecast()
        {
            //Arrange
            RoadWeather.Models.Trip newTrip = new RoadWeather.Models.Trip()
            {
                Duration = 4,
            };

            //Act
            bool isValid = RoadWeather.Managers.WeatherUtils.AvailableForShortTermForecast(newTrip);
            
            //Assert
            Assert.IsTrue(isValid);

        }
    }
}
