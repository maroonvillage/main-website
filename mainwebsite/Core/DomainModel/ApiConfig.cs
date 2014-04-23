using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.Enumerators;

namespace MaroonVillage.Core.DomainModel
{
    public class ApiConfig
    {
        public int ApiConfigId { get; set; }
        public string ApiName { get; set; }
        public string ApiDescription { get; set; }
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public int RequestQuota { get; set; }
        public string RequestPeriod { get; set; }

        public ApiConfig()
        {

        }

    }
}
