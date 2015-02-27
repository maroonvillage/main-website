using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Interfaces.Repositories;
using MaroonVillage.Core.Interfaces.Repositories.Google;
using MaroonVillage.Core.Interfaces.Services.Google;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.Services.Google
{
    public class CivicsInformationService : ICivicsInformationService
    {
        private readonly ICivicsInformationRepository _civicsInformationRepository;
        private readonly IApiRepository _apiRepository;

        public CivicsInformationService(IApiRepository apiRepository, ICivicsInformationRepository civicsInformationRepository)
        {
            _civicsInformationRepository = civicsInformationRepository;
            _apiRepository = apiRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiConfig"></param>
        /// <param name="requestParameters"></param>
        /// <returns></returns>
        public JToken GetRepresentativesByAddress(ICivicsInformationRequestParameters requestParameters)
        {
            try
            {
                var apiConfig = _apiRepository.GetApiConfigByName("GoogleCivicsInformationAPI_Representatives");
                var civicsInfo = _civicsInformationRepository.GetRepresentativesByAddress(apiConfig, requestParameters);
                return civicsInfo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiConfig"></param>
        /// <param name="requestParameters"></param>
        /// <returns></returns>
        public JToken GetRepresentativesByDivision(ICivicsInformationRequestParameters requestParameters)
        {
            try
            {
                var apiConfig = _apiRepository.GetApiConfigByName("GoogleCivicsInformationAPI_Representatives");
                var civicsInfo = _civicsInformationRepository.GetRepresentativesByDivision(apiConfig, requestParameters);
                return civicsInfo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return null;
        }
    }
}
