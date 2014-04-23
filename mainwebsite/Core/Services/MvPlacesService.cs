using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Enumerators;
using MaroonVillage.Core.Interfaces.Repositories;
using MaroonVillage.Core.Interfaces.Repositories.Google;
using MaroonVillage.Core.Interfaces.Services;
using MaroonVillage.Core.Interfaces.Services.Google;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.Services
{
    public class MvPlacesService : IMvPlacesService
    {
        private readonly ICacheService _cacheService;
        private readonly IMvPlacesRepository _mvPlacesRepository;
        private readonly IGeoCodingRepository _geoCodingRepository;

        public MvPlacesService(IMvPlacesRepository mvPlacesRepository, IGeoCodingRepository geoCodingRepository, ICacheService cacheService)
        {
            _mvPlacesRepository = mvPlacesRepository;
            _geoCodingRepository = geoCodingRepository;
            _cacheService = cacheService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MvPlace> GetAllMvPlaces()
        {
            try
            {
                var cacheKey = CoreHelperService.GetEnumDescription(CacheKeys.MvPlaces);
                var places = new List<MvPlace>().AsEnumerable<MvPlace>();
                if (_cacheService.Contains(cacheKey))
                {
                    places = (IEnumerable<MvPlace>)_cacheService[cacheKey];
                }
                else
                {
                    places = _mvPlacesRepository.GetAllMvPlaces();
                    if (places != null)
                        _cacheService.Add(cacheKey, places, CoreHelperService.GetEnumDefaultValue(CacheKeys.MvPlaces));
                }
                return places;
            }
            catch (Exception)//TODO: Add exception handling
            {
                //TODO: Add logging (log4net)    
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public MvPlace GetPlaceByName(string name)
        {
            //get mvplace then use the city name to retreive the Geocoding data
            var place = new MvPlace();
            try
            {
                var places = GetAllMvPlaces();
                place = places.Where(x => x.PlaceName.ToLower() == name.ToLower()).FirstOrDefault();
                //if (place != null)
                //{
                //    if (_cacheService.Contains(place.City.Trim()))
                //    {
                //        var geoCodeResponse = (JToken)_cacheService[place.City.Trim()];
                //        var results = JObject.Parse(geoCodeResponse.ToString());

                //        place.Lattitude = results["results"][0]["geometry"]["location"]["lat"].ToString();
                //        place.Longitude = results["results"][0]["geometry"]["location"]["lng"].ToString();

                //    }
                //    else
                //    {
                //        var requestParameters = new GeoCodingRequestParameters
                //        {
                //            //will only use city as the address to minimize the number of requests to Geocoding API
                //            Address = place.City,
                //            IsoCountryCode = Enumerators.IsoCountryCodesAlpha2.UnitedStates,
                //            Sensor = false,
                //            Language = "en"
                //        };
                //        //Call GeoCodingRepository because this method will handle caching of geocoding responses
                //        var geoCodeResponse = _geoCodingRepository.GetRequest(requestParameters);
                //        if (geoCodeResponse != null)
                //        {
                //            _cacheService.Add(place.City.Trim(), geoCodeResponse, 1440);
                //            //extract lat/long
                //            var results = JObject.Parse(geoCodeResponse.ToString());

                //            place.Lattitude = results["results"][0]["geometry"]["location"]["lat"].ToString();
                //            place.Longitude = results["results"][0]["geometry"]["location"]["lng"].ToString();
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {

            }
            return place ?? new MvPlace();
        }
    }
}
