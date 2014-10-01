using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaroonVillage.Core.DomainModel
{
    public class Latlng
    {
        public bool needs_recoding { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
    }

    public class LiquorLicense
    {
        public string license_type { get; set; }
        public string zip_code { get; set; }
        public string license_number { get; set; }
        public string businesstype { get; set; }
        public string business_type { get; set; }
        public string street { get; set; }
        public string street_name { get; set; }
        public string state { get; set; }
        public string current_status { get; set; }
        public string city { get; set; }
        public string county { get; set; }
        public string county_number { get; set; }
        public string original_date { get; set; }
        public string street_number { get; set; }
        public string licensee { get; set; }
        public string licensee_name { get; set; }
        public string license_current_status { get; set; }
        public string status_effective_date { get; set; }
        public string district { get; set; }
        public string subdistrict { get; set; }
        public string sub_district { get; set; }
        public string dbaname { get; set; }
        public Latlng latlng { get; set; }
    }

}
