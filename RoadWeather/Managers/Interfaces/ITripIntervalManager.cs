using System;
namespace RoadWeather.Managers.Interfaces
{
    public interface ITripIntervalManager
    {
        System.Collections.Generic.List<RoadWeather.Models.LocationDetail> GetLocationsInIntervalsWithTime(RoadWeather.Models.Trip trip);
    }
}
