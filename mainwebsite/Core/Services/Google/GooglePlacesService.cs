using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Interfaces.Repositories.GoogleApi;
using MaroonVillage.Core.Interfaces.Services.Google;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.Services.Google
{
    public class GooglePlacesService : ApiServiceBase, IGooglePlacesService
    {
        private readonly IGooglePlacesRepository _googlePlacesRepository;

        public GooglePlacesService(IGooglePlacesRepository googlePlacesRepository)
        {
            _googlePlacesRepository = googlePlacesRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        public JToken GetNearBySearch(IGooglePlacesRequestParameters requestParams)
        {
            try
            {
                var placesNearBySearch = _googlePlacesRepository.GetNearBySearch(requestParams);
                //TODO: use types list to filter results from the API
                return placesNearBySearch;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
