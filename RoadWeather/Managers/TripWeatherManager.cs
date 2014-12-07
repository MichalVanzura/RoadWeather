using RoadWeather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Threading.Tasks;
using RoadWeather.Managers.Interfaces;

namespace RoadWeather.Managers
{
    /// <summary>
    /// This class provides weather services for triped specified by parameter.
    /// </summary>
    public class TripWeatherManager : RoadWeather.Managers.Interfaces.ITripWeatherManager
    {
        private static readonly ILog log = LogManager.GetLogger("WeatherManager");
        private ILocationWeatherManager locationWeatherManager;
        private IWeatherUtils weatherUtils;
        private ITripIntervalManager tripIntervalMgr;

        public TripWeatherManager(ILocationWeatherManager locationWeatherManager,
            IWeatherUtils weatherUtils, ITripIntervalManager tripIntervalMgr)
        {
            this.locationWeatherManager = locationWeatherManager;
            this.weatherUtils = weatherUtils;
            this.tripIntervalMgr = tripIntervalMgr;
        }

        /// <summary>
        /// Returns dictionary of location details and entry adapters for trip.
        /// ForecastEntryAdapters provide unified forecast entry for long and short term forecasts.
        /// </summary>
        /// <param name="trip">Trip</param>
        /// <returns>Weather for locations on the trip</returns>
        public async Task<Dictionary<LocationDetail, ForecastEntry>> GetForecastForTrip(Trip trip)
        {
            if (trip == null)
            {
                throw new ArgumentNullException("Trip is null");
            }

            try
            {
                var dictResult = new Dictionary<LocationDetail, ForecastEntry>();
                if (weatherUtils.AvailableForShortTermForecast(trip))
                {
                    log.Info("Getting ");
                    var locations = tripIntervalMgr.GetLocationsInIntervalsWithTime(trip);
                    var locationForecasts = await locationWeatherManager.GetEntriesForLocationsShortTerm(locations);

                    dictResult = locationForecasts.ToDictionary(kvp => kvp.Key, kvp => new ForecastEntry(kvp.Value));

                    return dictResult;
                }
                else
                {
                    //TODO: check end time doesn't exceed 16 days
                    //bool exceed = trip.StartDateTime.AddSeconds(trip.Duration) > DateTime.Now.Date.AddDays(16);
                    var locations = tripIntervalMgr.GetLocationsInIntervalsWithTime(trip);
                    var locationForecasts = await locationWeatherManager.GetEntriesForLocationsLongTerm(locations);

                    dictResult = locationForecasts.ToDictionary(kvp => kvp.Key, kvp => new ForecastEntry(kvp.Value));

                    return dictResult;
                }
            }
            catch (Exception ex)
            {
                string msg = "Problem in TripWeatherManager";
                log.Error(msg, ex);
                throw new Exception(msg, ex);
            }
          
        }
    }
}