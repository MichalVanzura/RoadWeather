using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadWeather.Models
{
    public class Forecast
    {
        public List<Weather> Weather { get; set; }
        public MainValues Main { get; set; }
        public Wind Wind { get; set; }
        public Clouds Clouds { get; set; }
    }

    public struct Weather
    {
        public long Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public struct Main
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
    /*
    public struct Wind
    {
        public double Speed { get; set; }
        public double Deg { get; set; }
    }

    public struct Clouds {
        public int All { get; set; }
    }*/
}