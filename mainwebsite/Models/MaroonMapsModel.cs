using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MainWebsite.Models
{
    public class MaroonMapsModel : DefaultModel
    {
       
        [Required]
        [Display(Name = "Search")]
        public string SearchToken { get; set; }

        private List<AddressResult> _locations;
        public List<AddressResult> Locations { 
            get
            {
                return _locations ?? new List<AddressResult>();
            }
            set
            {
                _locations = value;
            } 
        }

        private AddressResult _defaultLocation;

        public AddressResult DefaultLocation {

            get
            {
                return _defaultLocation ?? new AddressResult();
            }

            set
            {
                _defaultLocation = value;
            }
        
        }
      
    }


    public class AddressResult
    {
        private string _formattedAddress;
        public string FormattedAddress { get; set; }
        private string _lattitude;
        public string Lattitude
        {
            get
            {

                return !string.IsNullOrEmpty(_lattitude) ? _lattitude : string.Empty;
            }

            set { _lattitude = value; }
        }
        private string _longitude;
        public string Longitude { get; set; }
    }
}