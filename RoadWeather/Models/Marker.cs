using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadWeather.Models
{
    public class Marker
    {
        public Location Location { get; set; }
        public string Title { get; set; }
        public Image Image { get; set; }
    }

    public struct Image
    {
        public string Url { get; set; }
    }
}