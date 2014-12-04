using System;
namespace RoadWeather.Managers.Interfaces
{
    public interface IWeatherUtils
    {
        bool AvailableForShortTermForecast(RoadWeather.Models.Trip trip);
        RoadWeather.Models.ForecastDailyEntry SelectLongTermEntry(RoadWeather.Models.LocationDetail location, RoadWeather.Models.ForecastLongTerm forecast);
        RoadWeather.Models.ForecastShortTermEntry SelectShortTermEntry(RoadWeather.Models.LocationDetail location, RoadWeather.Models.ForecastShortTerm forecast);
    }
}
