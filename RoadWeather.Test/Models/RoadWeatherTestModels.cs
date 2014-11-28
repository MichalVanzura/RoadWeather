using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoadWeather.Models;
using RoadWeather.Managers;

namespace RoadWeather.Test.Models
{
    [TestClass]
    public class RoadWeatherTestModels
    {


        [TestMethod]
        public void Trip_require_short_term_forecast()
        {
            //Arrange
            Trip newTrip = new Trip()
            {
                Duration = 4,
            };

            //Act
            bool isValid = WeatherUtils.AvailableForShortTermForecast(newTrip);
            
            //Assert
            Assert.IsTrue(isValid);

        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Closest_Forecast_For_Location_Estimated_Date_Throw_Exception()
        {
            //Arrange
            Location newLocation = new Location()
            {
                Latitude = 50,
                Longitude = 50

            };

            LocationDetail newLocationDetail = new LocationDetail()
            {
                Location = newLocation,
                Time = new DateTime(2014, 12, 8)
            };

            ForecastShortTerm newForecast = new ForecastShortTerm()
            {
                Entries = null
            };

            //Act
            var entry = WeatherUtils.SelectShortTermEntry(newLocationDetail, newForecast);

        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Daily_Weather_Forecast_For_Location_Estimated_Date_Throw_Exception()
        {
            //Arrange
            Location newLocation = new Location()
            {
                Latitude = 50,
                Longitude = 50
                
            };

            LocationDetail newLocationDetail = new LocationDetail()
            {
                Location = newLocation,
                Time = new DateTime(2014,12,8)
            };

            ForecastLongTerm newForecast = new ForecastLongTerm()
            {
                Entries = null
            };

            //Act
            var entry = WeatherUtils.SelectLongTermEntry(newLocationDetail, newForecast);

        }
    }
}
