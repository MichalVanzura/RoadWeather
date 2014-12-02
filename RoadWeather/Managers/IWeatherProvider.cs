﻿using System;
namespace RoadWeather.Managers
{
    interface IWeatherProvider
    {
        System.Threading.Tasks.Task<RoadWeather.Models.ForecastLongTerm> GetForecastLongTerm(RoadWeather.Models.Location location);
        System.Threading.Tasks.Task<RoadWeather.Models.ForecastShortTerm> GetForecastShortTerm(RoadWeather.Models.Location location);
    }
}
