using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaroonVillage.Core.DomainModel
{
    public interface ITedApiRequestParameters
    {
        string ApiKey { get; set; }
        string Fields { get; set; }
        string Filter { get; set; }
        string Language { get; set; }
        int Limit { get; set; }
        int Offset { get; set; }
        string Order { get; set; }
        string Sort { get; set; }
        bool SuppressErrorCodes { get; set; }
    }


    public class TedApiRequestParameters : ITedApiRequestParameters
    {
        public string ApiKey { get; set; }
        public string Fields { get; set; }
        public string Filter { get; set; }
        public string Language { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public string Order { get; set; }
        public string Sort { get; set; }
        public bool SuppressErrorCodes { get; set; }

    }
}
