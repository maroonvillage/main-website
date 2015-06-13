using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Enumerators;
using Newtonsoft.Json.Linq;

namespace MainWebsite.Models
{
    public class MvPlacesModel : DefaultModel
    {
        public MvPlacesModel()
        {
            _weatherUnits = UnitTypes.None;
        }
        private string _placeAddress;

        public string GoogleApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings["GoogleApiKey"] != null ? Convert.ToString(ConfigurationManager.AppSettings["GoogleApiKey"]) : string.Empty;
            }
        }
        public string GoogleMapsV3Url
        {
            get
            {
                var baseUrl = ConfigurationManager.AppSettings["GoogleMapsV3Url"] != null ? Convert.ToString(ConfigurationManager.AppSettings["GoogleMapsV3Url"]) : string.Empty;
                return string.Format("{0}?key={1}&sensor=false", baseUrl, GoogleApiKey);
            }
        }
        public string GooglePlacesAPILibrary
        {
            get
            {
                var baseUrl = ConfigurationManager.AppSettings["GooglePlacesAPIJsLibraryUrl"] != null ? Convert.ToString(ConfigurationManager.AppSettings["GooglePlacesAPIJsLibraryUrl"]) : string.Empty;
                return string.Format("{0}&sensor=false", baseUrl);
            }
        }
        public IEnumerable<MvPlace> MvPlaces { get; set; }
        public IEnumerable<SimplePlace> SimplePlaces { get; set; }
        private IList<ServicePhoto> _servicePhotos;
        public IList<ServicePhoto> ServicePhotos { get { return _servicePhotos; } set { _servicePhotos = value; } }
        public IEnumerable<JToken> GeoCodingResponses { get; set; }
        public JToken GooglePlacesResponse { get; set; }
        private MvPlace _place;
        public MvPlace Place { get { return _place ?? new MvPlace(); } set { _place = value; } }
        public string GeoCodingJson
        {
            get
            {

                return _place != null ? _place.GeoCodingObject.ToString() : string.Empty;
            }
        }
        public string PlaceAddress
        {


            get
            {
                var addr = string.Format("{0}, {1}, {2} {3}", _place.Address1, _place.City, _place.State, _place.ZipCode);
                addr = addr.TrimStart(new char[] { ',' }).Trim();
                addr = addr.TrimEnd(new char[] { ',' }).Trim();
                return addr;
            }
        }
        private UnitTypes _weatherUnits;
        public UnitTypes WeatherUnits { get { return _weatherUnits; } set { _weatherUnits = value; } }
        public Coordinates Coords { get; set; }
        public string CountryName { get; set; }
        public int sunrise;
        public DateTime Sunrise
        {
            get
            {
                var dt = new DateTime(1970, 1, 1);

                return dt.AddSeconds(sunrise).ToLocalTime();
            }
        }
        public int sunset;
        public DateTime Sunset
        {
            get
            {
                var dt = new DateTime(1970, 1, 1);

                return dt.AddSeconds(sunset).ToLocalTime();
            }
        }
        private decimal _tempurature;
        public decimal Tempurature { get { return _tempurature; } set { _tempurature = value; } }
        private decimal _tempuratureFahrenheit;
        public decimal TempuratureFahrenheit
        {
            get
            {
                if (WeatherUnits == UnitTypes.Metric)
                {
                    return (_tempurature * 0.5555M) + 32;
                }
                else
                {
                    return _tempurature;
                }
            }

        }
        public decimal TempuratureCelcius
        {
            get
            {
                if (WeatherUnits == UnitTypes.Imperial)
                {
                    return (_tempurature - 32) * 0.5555M;
                }
                else
                {
                    return _tempurature;
                }
            }

        }
        public decimal MinTemp { get; set; }
        public decimal MaxTemp { get; set; }
        public decimal Pressure { get; set; }
        public decimal SeaLevel { get; set; }
        public decimal GroundLevel { get; set; }
        public int Humidity { get; set; }
        public decimal WindSpeed { get; set; }
        public decimal WindDegree { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string WeatherMain { get; set; }
    }

    public class Coordinates
    {
        public decimal Lattitude { get; set; }
        public decimal Longitude { get; set; }
        public Coordinates()
        {
            Lattitude = 0.0M;
            Longitude = 0.0M;
        }
    }
}