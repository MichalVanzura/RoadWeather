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

        protected bool Equals(Location other)
        {
            return Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Location) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Latitude.GetHashCode()*397) ^ Longitude.GetHashCode();
            }
        }
    }

    public class Trip
    {
        public IEnumerable<Location> Locations { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDateTime { get; set; }
        public int Duration { get; set; }
    }

    public class LocationDetail
    {
        public Location Location { get; set; }
        public DateTime Time { get; set; }

        protected bool Equals(LocationDetail other)
        {
            return Location.Equals(other.Location) && Time.Equals(other.Time);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((LocationDetail) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Location.GetHashCode()*397) ^ Time.GetHashCode();
            }
        }

    }
}