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
        public double Day { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public double Night { get; set; }
        public double Eve { get; set; }
        public double Morn { get; set; }
    }

    public class WeatherDescription
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class Clouds
    {
        public int All { get; set; }
    }

    public class Wind
    {
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