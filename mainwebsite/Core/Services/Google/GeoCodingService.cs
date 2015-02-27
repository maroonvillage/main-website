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
    public class GeoCodingService : ApiServiceBase, IGeoCodingService
    {
        private readonly IGeoCodingRepository _geoCodingRepository;

        public GeoCodingService(IGeoCodingRepository geoCodingRepository)
        {
            _geoCodingRepository = geoCodingRepository;
        }

        public JToken GeoCode(IGeoCodingRequestParameters requestParams)
        {
            try
            {
                return _geoCodingRepository.GeoCode(requestParams);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
