using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadWeather.Models
{
    public class ForecastEntryAdapter
    {
        private string description;
        private int temperature;
        private DateTime dateTime;
        private string icon;

        public string Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int Temperature
        {
            get { return temperature; }
            set { temperature = value; }
        }

        public DateTime DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        public ForecastEntryAdapter(ForecastShortTermEntry entry)
        {
            this.description = entry.WeatherDescription[0].Main;
            this.temperature = (int)Math.Round(entry.MainValues.Temp);
            this.dateTime = entry.ForecastTime;
            this.icon = entry.WeatherDescription[0].Icon;
        }

        public ForecastEntryAdapter(ForecastDailyEntry entry)
        {
            this.description = entry.WeatherDescription[0].Description;
            this.temperature = (int)Math.Round(entry.Temp.Day);
            this.dateTime = entry.ForecastTime;
            this.icon = entry.WeatherDescription[0].Icon;

        }
    }
}