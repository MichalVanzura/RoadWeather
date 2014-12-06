using RoadWeather.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadWeather.Test.Fakes
{
    public class StaticClock : IClock
    {
        private DateTime now;

        public StaticClock(DateTime dt)
        {
            this.now = dt;
        }

        DateTime IClock.Now
        {
            get { return now; }
        }
    }
}
