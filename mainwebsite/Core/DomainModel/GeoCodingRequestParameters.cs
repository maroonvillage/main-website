using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.Enumerators;

namespace MaroonVillage.Core.DomainModel
{

    public interface IGeoCodingRequestParameters
    {
        string Address { get; set; }
        string Route { get; set; }
        string Locality { get; set; }
        string AdministrativeArea { get; set; }
        string PostalCode { get; set; }
        IsoCountryCodesAlpha2 IsoCountryCode { get; set; }
        bool Sensor { get; set; }
        string Key { get; set; }
        string Language { get; set; }
        string Region { get; set; }
        Component RequestComponents { get; }


    }
    public class GeoCodingRequestParameters : IGeoCodingRequestParameters
    {
        public string Address { get; set; }
        public string Route { get; set; }
        public string Locality { get; set; }
        public string AdministrativeArea { get; set; }
        public string PostalCode { get; set; }
        public IsoCountryCodesAlpha2 IsoCountryCode { get; set; }
        public bool Sensor { get; set; }
        public string Key { get; set; }
        public string Language { get; set; }
        public string Region { get; set; }

        public Component RequestComponents
        {
            get
            {
                return new Component
                {
                    Route = Route,
                    Locality = Locality,
                    AdministrativeArea = AdministrativeArea,
                    PostalCode = PostalCode,
                    IsoCountryCode = IsoCountryCode
                };
            }
        }
    }
}
