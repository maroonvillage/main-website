using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.Enumerators;

namespace MaroonVillage.Core.DomainModel
{

    public interface IApiConfig
    {
        int ApiConfigId { get; set; }
        string ApiName { get; set; }
        string ApiDescription { get; set; }
        string BaseUrl { get; set; }
        string ApiKey { get; set; }
        int RequestQuota { get; set; }
        string RequestPeriod { get; set; }
        string Secret { get; set; }
    }
    public class ApiConfig : IApiConfig
    {
        public int ApiConfigId { get; set; }
        public string ApiName { get; set; }
        public string ApiDescription { get; set; }
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public int RequestQuota { get; set; }
        public string RequestPeriod { get; set; }
        public string Secret { get; set; }

        public ApiConfig()
        {

        }

    }
}
