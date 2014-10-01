using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Interfaces.Repositories.Socrata;
using MaroonVillage.Core.Interfaces.Services;
using MaroonVillage.Core.Interfaces.Services.Socrata;
using Newtonsoft.Json;

namespace MaroonVillage.Core.Services.Socrata
{
    public class OpenDataService : ApiServiceBase, IOpenDataService
    {
        /* THE SERVICE WILL CALL API REPOSITORY TO RETREIVE THE API INPUT REQUEST
         * AND BUILD THE APIREQUEST PARAMETERS OBJECT WHICH WILL AND BUILD THE REQUEST
         * WHICH WILL BE PASSED INTO THE REQUEST METHOD IN THE REPOSITORY
         * THE REQUEST TO THE CORRESPONDING API WILL BE MADE IN THE REPOSITORY
         */
        private readonly IApiService _apiService;
        private readonly IOpenDataRepository _openDataRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="openDataRepository"></param>
        public OpenDataService(IApiService apiService, IOpenDataRepository openDataRepository)
        {
            _apiService = apiService;
            _openDataRepository = openDataRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSetId"></param>
        /// <returns></returns>
        public List<LiquorLicense> GetNewLiquorLicenses(string apiName, string dataSetId)
        {
            var liquorLicenses = new List<LiquorLicense>();
            var apiConfig = _apiService.GetApiConfigByName(apiName);
            _openDataRepository.BaseUrl = apiConfig.BaseUrl;

            var list = _openDataRepository.GetSocrataDataset(dataSetId).ToList();


            foreach (object s in list)
            {
                var liquorLic = JsonConvert.DeserializeObject<LiquorLicense>(s.ToString());
                liquorLicenses.Add(liquorLic);
            }

            return liquorLicenses ?? new List<LiquorLicense>();
        }


    }
}
