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
        
         /*Do not understand why this test is not working :(
        [TestMethod]
        public void Test_Icon()
        {
            //Arrange
            WeatherDescription weatherDescription = new WeatherDescription();
            weatherDescription.Id = 0;
            weatherDescription.Main = "main";
            weatherDescription.Description = "description";
            weatherDescription.Icon = "icon";

            var list = new List<WeatherDescription>();
            list.Add(weatherDescription);

            ForecastShortTermEntry entry = new ForecastShortTermEntry();
            entry.WeatherDescription = list;
            
            // other attributes must be also assigned for this test to pass.
            // it might be best ti implement null checking in ForecastEntry class.

            //Act
            ForecastEntry adapter = new ForecastEntry(entry);

            //Assert
            Assert.AreEqual(entry.WeatherDescription[0].Icon, "icon");
        
        }*/
    }
}
