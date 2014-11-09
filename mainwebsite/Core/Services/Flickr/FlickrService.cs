using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Interfaces.Repositories.Flickr;
using MaroonVillage.Core.Interfaces.Services.Flickr;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.Services.Flickr
{
    public class FlickrService : ApiServiceBase, IFlickrService
    {
        private readonly IFlickrRepository _flickrRepository;

        public FlickrService(IFlickrRepository flickrRepository)
        {
            _flickrRepository = flickrRepository;
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
    }
}
