using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadWeather.Models
{
    public class ForecastEntry
    {
        public string Icon {get; set; }

        public string Description {get; set;}

        public int Temperature {get; set;}

        public DateTime DateTime {get; set; }

        public ForecastEntry(ForecastShortTermEntry entry)
        {
            this.Description = entry.WeatherDescription[0].Main;
            this.Temperature = (int)Math.Round(entry.MainValues.Temp);
            this.DateTime = entry.ForecastTime;
            this.Icon = entry.WeatherDescription[0].Icon;
        }

        public ForecastEntry(ForecastDailyEntry entry)
        {
            this.Description = entry.WeatherDescription[0].Description;
            this.Temperature = (int)Math.Round(entry.Temp.Day);
            this.DateTime = entry.ForecastTime;
            this.Icon = entry.WeatherDescription[0].Icon;

        }
    }
}