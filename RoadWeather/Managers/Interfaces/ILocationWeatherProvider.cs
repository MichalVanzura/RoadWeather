using System;
namespace RoadWeather.Managers.Interfaces
{
    public interface ILocationWeatherProvider
    {
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<RoadWeather.Models.LocationDetail, RoadWeather.Models.ForecastDailyEntry>> GetEntriesForLocationsLongTerm(System.Collections.Generic.List<RoadWeather.Models.LocationDetail> locations);
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<RoadWeather.Models.LocationDetail, RoadWeather.Models.ForecastShortTermEntry>> GetEntriesForLocationsShortTerm(System.Collections.Generic.List<RoadWeather.Models.LocationDetail> locations);
    }
}
