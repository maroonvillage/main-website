using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaroonVillage.Core.DomainModel
{
    public interface ICivicsInformationRequestParameters
    {
        string Address { get; set; }
        string OcId { get; set; }

    }
    public class CivicsInformationRequestParameters : ICivicsInformationRequestParameters
    {
        public string Address { get; set; }
        public string OcId { get; set; }
    }
}
