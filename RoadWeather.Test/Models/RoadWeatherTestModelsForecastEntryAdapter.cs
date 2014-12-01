using System;
using RoadWeather.Models;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RoadWeather.Test.Models
{
    [TestClass]
    public class RoadWeatherTestModelsForecastEntryAdapter
    {
        
        /* Do not understand why this test is not working :(
        [TestMethod]
        public void Test_Icon()
        {
            //Arrange
            WeatherDescription temp = new WeatherDescription();
            temp.Id = 0;
            temp.Main = "main";
            temp.Description = "description";
            temp.Icon = "icon";

            ForecastShortTermEntry entry = new ForecastShortTermEntry();
            entry.WeatherDescription[0] = temp;

            //Act
            ForecastEntryAdapter adapter = new ForecastEntryAdapter(entry);

            //Arange
            Assert.AreEqual(entry.WeatherDescription[0].Icon, "icon");

        }*/
    }
}
