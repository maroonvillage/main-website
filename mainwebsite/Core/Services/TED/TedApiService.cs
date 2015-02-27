using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Interfaces.Repositories.TED;
using MaroonVillage.Core.Interfaces.Services;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.Services.TED
{
    public class TedApiService
    {
        private string _apiName;
        public string ApiName { get; set; }

        private ApiConfig _apiConfig { get; set; }

        private readonly IApiService _apiService;
        private readonly ITedApiRepository _tedApiRepository;

        public TedApiService()
        {

        }
        public TedApiService(IApiService apiService, ITedApiRepository tedApiRepository)
        {
            _apiService = apiService;
            _tedApiRepository = tedApiRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiName"></param>
        public TedApiService(IApiService apiService, string apiName)
        {
            _apiName = apiName;
            _apiService = apiService;
            _apiConfig = _apiService.GetApiConfigByName(apiName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tedApiRequestParameters"></param>
        /// <returns></returns>
        public IEnumerable<Country> GetCountries(ITedApiRequestParameters tedApiRequestParameters)
        {

            try
            {
                JToken tedCountries = _tedApiRepository.GetCountries(tedApiRequestParameters);

            }
            catch (Exception)
            {
                
                throw;
            }

            return null;
        }
    }
}
