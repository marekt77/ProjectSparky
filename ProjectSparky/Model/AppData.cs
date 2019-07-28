using System;

namespace ProjectSparky.Model
{
    public class AppData
    {
        public string City { get; set; }
        public string PostalCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int owCityID { get; set; }
        public bool UseMetric { get; set; }
        public DateTime CurrentDate { get; set; }
        public BackgroundImage BackgoundImage { get; set; }
    }
}
