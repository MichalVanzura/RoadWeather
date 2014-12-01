using System;
using RoadWeather.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RoadWeather.Test.Models
{
    [TestClass]
    public class RoadWeatherTestModelsLocation
    {
        [TestMethod]
        public void Test_Location_FillIn()
        {
            //Arrange
            Location location = new Location();
            location.Longitude = 100;
            location.Latitude = 100;

            DateTime date = new DateTime(2014,12,01);

            //Act
            LocationDetail locationDetail = new LocationDetail();
            locationDetail.Location = location;
            locationDetail.Time = date;

            Trip trip = new Trip();
            trip.StartDateTime = date;
            trip.Duration = 10;

            //Assert
            Assert.AreEqual(locationDetail.Location.Longitude, 100);
            Assert.AreEqual(trip.StartDateTime, date);


        }
    }
}
