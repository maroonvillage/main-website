using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MaroonVillage.Core.DomainModel;
using Newtonsoft.Json.Linq;

namespace MainWebsite.Models
{
    public class MvPlacesModel : DefaultModel
    {
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

        public string PlaceAddress {
            get { return string.Format("{0}, {1}, {2} {3}", _place.Address1, _place.City, _place.State, _place.ZipCode); }
        }
    }
}