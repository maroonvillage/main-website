using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlickrNet;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Enumerators;
using MaroonVillage.Core.Interfaces.Repositories;
using MaroonVillage.Core.Interfaces.Repositories.Flickr;
using MaroonVillage.Core.Interfaces.Services;
using MaroonVillage.Core.Interfaces.Services.Flickr;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.Services.Flickr
{
    public class FlickrService : ApiServiceBase, IFlickrService
    {
        private readonly IApiRepository _apiRepository;
        private readonly ICacheService _cacheService;
        private readonly IFlickrRepository _flickrRepository;
        private readonly IMvPlacesRepository _mvPlacesRepository;

        public FlickrService(IApiRepository apiRepository, ICacheService cacheService, IFlickrRepository flickrRepository,
            IMvPlacesRepository mvPlacesRepository)
        {
            _apiRepository = apiRepository;
            _cacheService = cacheService;
            _flickrRepository = flickrRepository;
            _mvPlacesRepository = mvPlacesRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestParameters"></param>
        /// <returns></returns>
        public JToken GetPhotos(IFlickrPhotoSearchParameters requestParameters)
        {
            try
            {
                var flickrPhotosSearch = _flickrRepository.GetPhotos(requestParameters);
                //TODO: use types list to filter results from the API
                return flickrPhotosSearch;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return null;
        }

        public PhotoCollection SimpleSearch(ApiConfig apiConfig, string token)
        {
            //_flickrRepository.

            //return flickr.PhotosSearch(options);
            return _flickrRepository.SimpleSearch(apiConfig, token);

        }

        public Place GetPlacesByLatLon(double latt, double lon)
        {
            return _flickrRepository.GetPlacesByLatLon(latt, lon);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public JToken GetPlacesByQuery(string query)
        {
            var place = _flickrRepository.GetPlacesByQuery(query);

            PhotoCollection pc = new PhotoCollection();
            if (place != null)
                pc = _flickrRepository.GetPhotosByTag(place[0]);
            //pc = _flickrRepository.GetPlacesByLatLon(place[0].Latitude, place[1].Longitude);

            string output = JsonConvert.SerializeObject(pc);


            return JToken.Parse(output);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="photoId"></param>
        /// <returns></returns>
        public JToken GetServicePhotosJson(string serviceName)
        {
            var servicePhotos = _mvPlacesRepository.GetServicePhotos(serviceName);
            JToken photoInfoList = null;
            var cacheKey = string.Format(CoreHelperService.GetEnumDescription(CacheKeys.ServicePhotos), serviceName);

            if (_cacheService.Contains(cacheKey))
            {
                photoInfoList = (JToken)_cacheService[cacheKey];
                return photoInfoList;
            }
            else
            {
                servicePhotos = _mvPlacesRepository.GetServicePhotos(serviceName);
            }
            var apiConfig = new ApiConfig
            {
                ApiKey = "7c8f7d7e5280b333c218eb62544fdc19",
                Secret = "446eb7a3fe2150b2"

            };
             List<ServicePhoto> spList = new List<ServicePhoto>();
            foreach (ServicePhoto photo in servicePhotos)
            {
                var photoInfo = _flickrRepository.GetPhotoInfoById(apiConfig, photo.UserId, photo.PhotoId);
                if (photoInfo != null)
                {
                    spList.Add(new ServicePhoto
                    {
                        PhotoTitle = photo.PhotoTitle,
                        OwnerRealName = photoInfo.OwnerRealName,
                        UserId = photoInfo.OwnerUserId,
                        UserName = photoInfo.OwnerUserName,
                        PhotoId = photoInfo.PhotoId,
                        OriginalUrl = photoInfo.OriginalUrl,
                        Thumbnail = photoInfo.SmallUrl
                    });

                }
            }
            JToken jsonToken = null;

            if (photoInfoList == null)
            {
                jsonToken = JToken.FromObject(spList, new Newtonsoft.Json.JsonSerializer());
                _cacheService.Add(cacheKey, jsonToken, CoreHelperService.GetEnumDefaultValue(CacheKeys.ServicePhotos));
            }
            return jsonToken ?? JToken.Parse(string.Empty);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public IList<ServicePhoto> GetServicePhotos(string serviceName)
        {
            var servicePhotos = _mvPlacesRepository.GetServicePhotos(serviceName);
            IList<ServicePhoto> photoInfoList;
            var cacheKey = string.Format(CoreHelperService.GetEnumDescription(CacheKeys.ServicePhotos), serviceName);

            if (_cacheService.Contains(cacheKey))
            {
                photoInfoList = (IList<ServicePhoto>)_cacheService[cacheKey];
                return photoInfoList;
            }
            else
            {
                servicePhotos = _mvPlacesRepository.GetServicePhotos(serviceName);
            }
            var apiConfig = new ApiConfig
            {
                ApiKey = "7c8f7d7e5280b333c218eb62544fdc19",
                Secret = "446eb7a3fe2150b2"

            };
            List<ServicePhoto> spList = new List<ServicePhoto>();
            foreach (ServicePhoto photo in servicePhotos)
            {
                var photoInfo = _flickrRepository.GetPhotoInfoById(apiConfig, photo.UserId, photo.PhotoId);
                if (photoInfo != null)
                {
                    spList.Add(new ServicePhoto
                    {
                        PhotoTitle = photo.PhotoTitle,
                        OwnerRealName = photoInfo.OwnerRealName,
                        UserId = photoInfo.OwnerUserId,
                        UserName = photoInfo.OwnerUserName,
                        PhotoId = photoInfo.PhotoId,
                        OriginalUrl = photoInfo.OriginalUrl//.LargeSquareUrl
                    });

                }
            }
            return spList ?? new List<ServicePhoto>();
        }
    }
}
