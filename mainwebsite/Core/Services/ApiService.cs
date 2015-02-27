using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Enumerators;
using MaroonVillage.Core.Interfaces.Repositories;
using MaroonVillage.Core.Interfaces.Services;

namespace MaroonVillage.Core.Services
{
    public class ApiService : ApiServiceBase, IApiService
    {
        private readonly IApiRepository _apiRepository;
        private readonly ICacheService _cacheService;

        public ApiService(IApiRepository apiRepository, ICacheService cacheService)
        {
            _apiRepository = apiRepository;
            _cacheService = cacheService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiName"></param>
        /// <returns></returns>
        public ApiConfig GetApiConfigByName(string apiName)
        {
            var cacheKey = string.Format(CoreHelperService.GetEnumDescription(CacheKeys.ApiConfigByName), apiName);
            var apiConfig = new ApiConfig();
            if (_cacheService.Contains(cacheKey))
            {
                apiConfig = (ApiConfig)_cacheService[cacheKey];
            }
            else
            {
                apiConfig = _apiRepository.GetApiConfigByName(apiName);
                if (apiConfig != null)
                    _cacheService.Add(cacheKey, apiConfig, CoreHelperService.GetEnumDefaultValue(CacheKeys.ApiConfigByName));
            }
            return apiConfig;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiConfigId"></param>
        /// <returns></returns>
        public IEnumerable<ApiRequestInput> GetApiRequestInputsByConfigId(int apiConfigId)
        {

           return _apiRepository.GetApiRequestInputsByConfigId(apiConfigId);
        }
    }
}
