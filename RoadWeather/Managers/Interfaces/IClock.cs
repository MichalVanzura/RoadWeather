using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadWeather.Managers.Interfaces
{
    public interface IClock
    {
        DateTime Now { get; } 
    }
}
