using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoadWeather.Models
{
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class Trip
    {
        public IEnumerable<Location> Locations { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDateTime { get; set; }
        public int Duration { get; set; }
    }
}