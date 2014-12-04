using RoadWeather.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace RoadWeather.Managers
{
    public class SystemClock :IClock
    {
        public DateTime Now { get { return DateTime.Now; } }
    }


}