using System;
using RoadWeather.Models;
using RoadWeather.Managers;
using RoadWeather.Managers.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Microsoft.QualityTools.Testing.Fakes;

namespace RoadWeather.Test.Managers
{
    [TestClass]
    public class RoadWeatherTestManagersTripWeatherManager
    {

        #region GetForecastForTrip

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Test_GetForecastForTrip_TripNull()
        {
            //Arrange

            IClock iClock = new SystemClock();
            IWeatherProvider weatherProvider = new WeatherProvider();
            IWeatherUtils weatherUtils = new WeatherUtils(iClock);
            ILocationWeatherManager locationWeatherManager = new LocationWeatherManager(weatherProvider, weatherUtils);
            ITripIntervalManager tripIntervalManager = new TripIntervalManager();
            ITripWeatherManager temp = new TripWeatherManager(locationWeatherManager, weatherUtils, tripIntervalManager);

            //Act and Assert
            Task<Dictionary<LocationDetail, ForecastEntry>> result = temp.GetForecastForTrip(null);
            
        }

        [TestMethod]
        public void Test_GetForecastForTrip_DictionaryCount()
        {
            //Arrange
            
            List<Location> list = new List<Location>();

            for (int i = 0; i < 200; i++)
            {
                Location location = new Location();
                location.Latitude = i;
                location.Longitude = i;
                list.Add(location);
            }

            Trip trip = new Trip();
            trip.Locations = list;
            trip.Duration = 72000;
            trip.StartDateTime = new DateTime(2014, 12, 01, 20, 00, 00);

            IClock iClock = new SystemClock();
            IWeatherProvider weatherProvider = new WeatherProvider();
            IWeatherUtils weatherUtils = new WeatherUtils(iClock);
            ILocationWeatherManager locationWeatherManager = new LocationWeatherManager(weatherProvider, weatherUtils);
            ITripIntervalManager tripIntervalManager = new TripIntervalManager();
            ITripWeatherManager temp = new TripWeatherManager(locationWeatherManager, weatherUtils, tripIntervalManager);

            //Act and Assert
            Task<Dictionary<LocationDetail, ForecastEntry>> result = temp.GetForecastForTrip(trip);
            
        }

        #endregion
        
    }
}
