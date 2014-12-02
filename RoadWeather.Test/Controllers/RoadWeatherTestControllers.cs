using System;
using RoadWeather.Controllers;
using RoadWeather.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RoadWeather.Test.Controllers
{
    [TestClass]
    public class RoadWeatherTestControllers
    {

        #region GetWeatherMarkers

        [TestMethod]
        public void Test_GetWeatherMarkers_Return()
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

            WeatherController temp = new WeatherController();

            //Act and Assert
            Assert.IsInstanceOfType(temp.GetWeatherMarkers(trip), typeof(Task<List<Marker>>));

        }

        #endregion
    }
}
