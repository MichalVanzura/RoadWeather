using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadWeather.Models
{
    public class ForecastShortTerm
    {
        [JsonProperty(PropertyName = "list")]
        public List<ForecastShortTermEntry> Entries { get; set; }
    }

    public class ForecastLongTerm
    {
        [JsonProperty(PropertyName = "list")]
        public List<ForecastDailyEntry> Entries { get; set; }
    }

    public class ForecastShortTermEntry
    {
        #region Equals
        protected bool Equals(ForecastShortTermEntry other)
        {
            return UnixTimestamp == other.UnixTimestamp && MainValues.Equals(other.MainValues) && string.Equals(TimestampString, other.TimestampString) && WeatherDescription.SequenceEqual(other.WeatherDescription) && Clouds.Equals(other.Clouds) && Wind.Equals(other.Wind);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ForecastShortTermEntry) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = UnixTimestamp;
                hashCode = (hashCode*397) ^ MainValues.GetHashCode();
                hashCode = (hashCode*397) ^ TimestampString.GetHashCode();
                hashCode = (hashCode*397) ^ WeatherDescription.GetHashCode();
                hashCode = (hashCode*397) ^ Clouds.GetHashCode();
                hashCode = (hashCode*397) ^ Wind.GetHashCode();
                return hashCode;
            }
        }
        #endregion

        [JsonProperty(PropertyName = "dt")]
        public int UnixTimestamp { get; set; }
        [JsonProperty(PropertyName="dt_txt")]
        public string TimestampString { get; set; }
        [JsonProperty(PropertyName="main")]
        public MainValues MainValues { get; set; }
        [JsonProperty(PropertyName= "weather")]
        public List<WeatherDescription> WeatherDescription { get; set; }
        public Clouds Clouds { get; set; }
        public Wind Wind { get; set; }
        
        [JsonIgnore]
        public DateTime ForecastTime
        {
            get { return UnixTimeConverter.UnixTimeStampToDateTime(UnixTimestamp); }
        }

    
    }

    public class ForecastDailyEntry
    {
        #region Equals
        protected bool Equals(ForecastDailyEntry o)
        {
            return UnixTimestamp == o.UnixTimestamp && Temp.Equals(o.Temp) && Pressure.Equals(o.Pressure) && Humidity == o.Humidity && WeatherDescription.SequenceEqual(o.WeatherDescription) && Speed.Equals(o.Speed) && Deg == o.Deg && Clouds == o.Clouds && Rain.Equals(o.Rain);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ForecastDailyEntry) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = UnixTimestamp;
                hashCode = (hashCode*397) ^ Temp.GetHashCode();
                hashCode = (hashCode*397) ^ Pressure.GetHashCode();
                hashCode = (hashCode*397) ^ Humidity;
                hashCode = (hashCode*397) ^ WeatherDescription.GetHashCode();
                hashCode = (hashCode*397) ^ Speed.GetHashCode();
                hashCode = (hashCode*397) ^ Deg;
                hashCode = (hashCode*397) ^ Clouds;
                hashCode = (hashCode*397) ^ Rain.GetHashCode();
                return hashCode;
            }
        }
        #endregion

        public override string ToString()
        {
            return string.Format("UnixTimestamp: {0}, Temp: {1}, Pressure: {2}, Humidity: {3}, WeatherDescription: {4}, Speed: {5}, Deg: {6}, Clouds: {7}, Rain: {8}, ForecastTime: {9}", UnixTimestamp, Temp, Pressure, Humidity, WeatherDescription, Speed, Deg, Clouds, Rain, ForecastTime);
        }

        [JsonProperty(PropertyName = "dt")]
        public int UnixTimestamp { get; set; }
        public Temp Temp { get; set; }
        public double Pressure { get; set; }
        public int Humidity { get; set; }
        [JsonProperty(PropertyName = "weather")]
        public List<WeatherDescription> WeatherDescription { get; set; }
        public double Speed { get; set; }
        public int Deg { get; set; }
        public int Clouds { get; set; }
        public double? Rain { get; set; }

        [JsonIgnore]
        public DateTime ForecastTime
        {
            get { return UnixTimeConverter.UnixTimeStampToDateTime(UnixTimestamp); }
        }


    }

    public class MainValues
    {
        #region Equals
        protected bool Equals(MainValues other)
        {
            return Temp.Equals(other.Temp) && TempMin.Equals(other.TempMin) && TempMax.Equals(other.TempMax) && Pressure.Equals(other.Pressure) && SeaLevel.Equals(other.SeaLevel) && GroundLevel.Equals(other.GroundLevel) && Humidity.Equals(other.Humidity);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MainValues) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Temp.GetHashCode();
                hashCode = (hashCode*397) ^ TempMin.GetHashCode();
                hashCode = (hashCode*397) ^ TempMax.GetHashCode();
                hashCode = (hashCode*397) ^ Pressure.GetHashCode();
                hashCode = (hashCode*397) ^ SeaLevel.GetHashCode();
                hashCode = (hashCode*397) ^ GroundLevel.GetHashCode();
                hashCode = (hashCode*397) ^ Humidity.GetHashCode();
                return hashCode;
            }
        }
        #endregion

        public double Temp { get; set; }
        [JsonProperty(PropertyName = "Temp_Min")]
        public double TempMin { get; set; }
        [JsonProperty(PropertyName = "Temp_Max")]
        public double TempMax { get; set; }
        public double Pressure { get; set; }
        [JsonProperty(PropertyName = "Sea_Level")]
        public double SeaLevel { get; set; }
        [JsonProperty(PropertyName = "Grnd_Level")]
        public double GroundLevel { get; set; }
        public double Humidity { get; set; }
    }

    public class Temp
    {
        #region Equals
        protected bool Equals(Temp other)
        {
            return Day.Equals(other.Day) && Min.Equals(other.Min) && Max.Equals(other.Max) && Night.Equals(other.Night) && Eve.Equals(other.Eve) && Morn.Equals(other.Morn);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Temp) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Day.GetHashCode();
                hashCode = (hashCode*397) ^ Min.GetHashCode();
                hashCode = (hashCode*397) ^ Max.GetHashCode();
                hashCode = (hashCode*397) ^ Night.GetHashCode();
                hashCode = (hashCode*397) ^ Eve.GetHashCode();
                hashCode = (hashCode*397) ^ Morn.GetHashCode();
                return hashCode;
            }
        }
        #endregion

        public double Day { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public double Night { get; set; }
        public double Eve { get; set; }
        public double Morn { get; set; }
    }

    public class WeatherDescription
    {
        #region Equals
        protected bool Equals(WeatherDescription other)
        {
            return Id == other.Id && string.Equals(Main, other.Main) && string.Equals(Icon, other.Icon) && string.Equals(Description, other.Description);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((WeatherDescription) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode*397) ^ Main.GetHashCode();
                hashCode = (hashCode*397) ^ Icon.GetHashCode();
                hashCode = (hashCode*397) ^ Description.GetHashCode();
                return hashCode;
            }
        }
        #endregion

        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class Clouds
    {
        #region Equals
        protected bool Equals(Clouds other)
        {
            return All == other.All;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Clouds) obj);
        }

        public override int GetHashCode()
        {
            return All;
        }
        #endregion 

        public int All { get; set; }
    }

    public class Wind
    {
        #region Equals
        protected bool Equals(Wind other)
        {
            return Speed.Equals(other.Speed) && Deg.Equals(other.Deg);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Wind) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Speed.GetHashCode()*397) ^ Deg.GetHashCode();
            }
        }
        #endregion

        public double Speed { get; set; }
        public double Deg { get; set; }
    }

    internal class UnixTimeConverter
    {
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0 , System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp)/*.ToLocalTime()*/;
            return dtDateTime;
        }

        public static long DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (long)(dateTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
        }
    }
}