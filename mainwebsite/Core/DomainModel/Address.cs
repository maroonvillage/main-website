using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaroonVillage.Core.DomainModel
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ZipSuffix { get; set; }
    }
}
