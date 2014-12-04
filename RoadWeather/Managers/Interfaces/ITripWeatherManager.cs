using System;
namespace RoadWeather.Managers.Interfaces
{
    public interface ITripWeatherManager
    {
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<RoadWeather.Models.LocationDetail, RoadWeather.Models.ForecastEntry>> GetForecastForTrip(RoadWeather.Models.Trip trip);
    }
}
