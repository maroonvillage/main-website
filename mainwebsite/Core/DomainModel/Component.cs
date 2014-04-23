using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  MaroonVillage.Core.Enumerators;
using MaroonVillage.Core.Services;

namespace MaroonVillage.Core.DomainModel
{
    public class Component
    {
        public string Route { get; set; }
        public string Locality { get; set; }
        public string AdministrativeArea { get; set; }
        public string PostalCode { get; set; }
        public IsoCountryCodesAlpha2 IsoCountryCode { get; set; }
        public bool IsSet
        {
            get
            {
                if (string.IsNullOrEmpty(Route) && string.IsNullOrEmpty(Locality) && string.IsNullOrEmpty(AdministrativeArea)
                    && string.IsNullOrEmpty(PostalCode) && IsoCountryCode == IsoCountryCodesAlpha2.Unknown)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// Returns the component filter string as required by Geocoding API
        /// </summary>
        /// <returns></returns>
        public string GetComponentFilterString()
        {
            var filter = new StringBuilder();
            if (!string.IsNullOrEmpty(Route)) filter.Append(string.Format("route:{0}{1}", Route, "|"));
            if (!string.IsNullOrEmpty(Locality)) filter.Append(string.Format("locality:{0}{1}", Locality, "|"));
            if (!string.IsNullOrEmpty(AdministrativeArea)) filter.Append(string.Format("administrative_area:{0}{1}", AdministrativeArea, "|"));
            if (!string.IsNullOrEmpty(PostalCode)) filter.Append(string.Format("postal_code:{0}{1}", PostalCode, "|"));
            if (IsoCountryCode != IsoCountryCodesAlpha2.Unknown) filter.Append(string.Format("country:{0}", CoreHelperService.GetEnumDescription(IsoCountryCode)));

            return filter.ToString();

        }


    }
}
