using System;
using RoadWeather.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RoadWeather.Test.Models
{
    [TestClass]
    public class RoadWeatherTestModelsMarker
    {
        [TestMethod]
        public void Test_Marker_FillIn()
        {
            //Arrange
            Location location = new Location();
            location.Latitude = 150;
            location.Longitude = 150;

            Text text = new Text();
            text.Description = "description";
            text.Temperature = 20;
            text.DateTime = new DateTime(2014, 12, 01);

            Image image = new Image();
            image.Url = "url";

            //Act
            Marker marker = new Marker();
            marker.Location = location;
            marker.Text = text;
            marker.Image = image;

            //Assert
            Assert.AreEqual(marker.Text.Temperature, 20);
            Assert.AreEqual(marker.Location.Latitude, 150);
            Assert.AreEqual(marker.Image.Url, "url");

        }
    }
}
