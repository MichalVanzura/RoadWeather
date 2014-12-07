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

        protected bool Equals(ForecastEntry other)
        {
            return string.Equals(Icon, other.Icon) && string.Equals(Description, other.Description) && Temperature == other.Temperature && DateTime.Equals(other.DateTime);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ForecastEntry) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Icon != null ? Icon.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Temperature;
                hashCode = (hashCode*397) ^ DateTime.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return string.Format("Icon: {0}, Description: {1}, Temperature: {2}, DateTime: {3}", Icon, Description, Temperature, DateTime);
        }
    }
}