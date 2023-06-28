﻿
namespace Addresses.DataContext.Helpers
{
    public class MapPoint
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public MapPoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
