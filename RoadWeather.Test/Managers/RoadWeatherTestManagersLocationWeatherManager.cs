using System;
using RoadWeather.Managers;
using RoadWeather.Models;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace RoadWeather.Test.Managers
{
    [TestClass]
    public class RoadWeatherTestManagersLocationWeatherManager
    {
        /*
        #region GetEntriesForLocationsLongTerm

        [TestMethod]
        public void Test_GetEntriesForLocationsLongTerm_Return()
        {
            //Arrange
            DateTime date = new DateTime(2014, 12, 01, 08, 00, 00);
            List<LocationDetail> listDetail = new List<LocationDetail>();
            LocationDetail detail = new LocationDetail();

            for (int i = 0; i < 200; i++)
            {
                Location location = new Location();
                location.Longitude = i;
                location.Latitude = i;
                detail.Location = location;
                detail.Time = date;
                listDetail.Add(detail);
            }

            LocationWeatherProvider temp = new LocationWeatherProvider();

            //Act and Assert
            Assert.IsInstanceOfType(temp.GetEntriesForLocationsLongTerm(listDetail), typeof(Task<Dictionary<LocationDetail, ForecastDailyEntry>>));
        }

        #endregion

        #region GetEntriesForLocationsShortTerm

        [TestMethod]

        public void Test_GetEntriesForLocationsShortTerm_Return()
        {
            //Arrange
            DateTime date = new DateTime(2014, 12, 01, 08, 00, 00);
            List<LocationDetail> listDetail = new List<LocationDetail>();
            LocationDetail detail = new LocationDetail();

            for (int i = 0; i < 200; i++)
            {
                Location location = new Location();
                location.Longitude = i;
                location.Latitude = i;
                detail.Location = location;
                detail.Time = date;
                listDetail.Add(detail);
            }
            LocationWeatherProvider temp = new LocationWeatherProvider();

            //Act and Assert
            Assert.IsInstanceOfType(temp.GetEntriesForLocationsShortTerm(listDetail), typeof(Task<Dictionary<LocationDetail, ForecastShortTermEntry>>));
        }

        #endregion
         * */
    }
}
