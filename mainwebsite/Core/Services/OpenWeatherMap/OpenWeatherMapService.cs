using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Interfaces.Repositories;
using MaroonVillage.Core.Interfaces.Repositories.OpenWeatherMap;
using MaroonVillage.Core.Interfaces.Services.OpenWeatherMap;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.Services.OpenWeatherMap
{
    public class OpenWeatherMapService : ApiServiceBase, IOpenWeatherMapService
    {

        private readonly IOpenWeatherMapRepository _openMapWeatherRepository;
        private readonly IApiRepository _apiRepository;

        public OpenWeatherMapService(IApiRepository apiRepository, IOpenWeatherMapRepository openMapWeatherRepository)
        {
            _openMapWeatherRepository = openMapWeatherRepository;
            _apiRepository = apiRepository;

            //TODO: Build a tree to contain all of the apis that this service will potentially call
            //  
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        public JToken GetCityWeather(IOpenWeatherMapRequestParameters requestParams)
        {
            try
            {
                var apiConfig = _apiRepository.GetApiConfigByName("OpenWeatherMapAPI");
                var openWeatherMap = _openMapWeatherRepository.GetCityWeather(apiConfig, requestParams);
                
                return openWeatherMap;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return null;
        }// end  method: GetCityWeatherByQuery

        public JToken GetCityWeatherById(int id)
        {
            return null;
        }
    }
}
