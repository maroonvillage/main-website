using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using FlickrNet;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Interfaces.Repositories.Flickr;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.Repositories.Flickr
{
    public class FlickrRepository : IFlickrRepository
    {

        FlickrNet.Flickr _flickr;

        public const string MY_NSID = "8448548@N05";



        public string BaseUrl
        {
            get
            {
                return "https://api.flickr.com/services/rest/";
            }
        }

        /*
         * <photos page="2" pages="89" perpage="10" total="881">
	<photo id="2636" owner="47058503995@N01" 
		secret="a123456" server="2" title="test_04"
		ispublic="1" isfriend="0" isfamily="0" />
	<photo id="2635" owner="47058503995@N01"
		secret="b123456" server="2" title="test_03"
		ispublic="0" isfriend="1" isfamily="1" />
	<photo id="2633" owner="47058503995@N01"
		secret="c123456" server="2" title="test_01"
		ispublic="1" isfriend="0" isfamily="0" />
	<photo id="2610" owner="12037949754@N01"
		secret="d123456" server="2" title="00_tall"
		ispublic="1" isfriend="0" isfamily="0" />
</photos>
         */

        private string secret;
        public FlickrRepository()
        {
            secret = ConfigurationManager.AppSettings["FlickrSecret"];
            _flickr = new FlickrNet.Flickr();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestParameters"></param>
        /// <returns></returns>
        public JToken GetPhotos(IFlickrPhotoSearchParameters requestParameters)
        {
            var requestUrl = new StringBuilder(@"?method=flickr.photos.search");

            if (!string.IsNullOrEmpty(requestParameters.Key))
                requestUrl.Append(string.Format("&api_key={0}", requestParameters.Key));
            else
                throw new Exception("Required request parameter 'api_key' not set.");

            if (!string.IsNullOrEmpty(requestParameters.Tags))
            {
                requestUrl.Append(string.Format("&tags={0}", requestParameters.Tags));
            }

            if (!string.IsNullOrEmpty(requestParameters.Text))
            {
                requestUrl.Append(string.Format("&text={0}", requestParameters.Text));
            }

            // request parameter is complete ...
            HttpClient client = new HttpClient { BaseAddress = new Uri(BaseUrl) };
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("applicaiton/json"));

            var svcResponse = client.GetAsync(requestUrl.ToString()).Result;
            if (svcResponse.IsSuccessStatusCode)
            {
                //MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
                // Use the JSON formatter to create the content of the request body.
                return svcResponse.Content.ReadAsAsync<JToken>().Result;
            }
            return null;


            throw new NotImplementedException("Method: GetPhotos has not been implemented yet.");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="latt"></param>
        /// <param name="lon"></param>
        /// <returns></returns>
        public Place GetPlacesByLatLon(double latt, double lon)
        {
            try
            {
                return _flickr.PlacesFindByLatLon(latt, lon);

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public PlaceCollection GetPlacesByQuery(string query)
        {
            try
            {
                // Cache c = new Cache();

                _flickr.ApiKey = "7c8f7d7e5280b333c218eb62544fdc19";
                _flickr.ApiSecret = "446eb7a3fe2150b2";

                var places = _flickr.PlacesFind(query);

                return places ?? new PlaceCollection();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        public PhotoCollection GetPhotosByTag(Place place)
        {
            if (place == null) throw new ArgumentNullException("Place cannot be null!");

            _flickr.ApiKey = "7c8f7d7e5280b333c218eb62544fdc19";
            _flickr.ApiSecret = "446eb7a3fe2150b2";

            PhotoSearchOptions options = new PhotoSearchOptions();

            options.PlaceId = place.PlaceId;
            options.WoeId = place.WoeId;
            options.Tags = "cityscape,city,skyline,aerial,cc,creativecommons,creative,commons";
            options.HasGeo = true;
            options.Radius = 5.0F;
            options.Latitude = place.Latitude;
            options.Longitude = place.Longitude;
            
            
            //default 100 per page
            options.PerPage = 50;
            var photos = _flickr.PhotosSearch(options);

            //var tags = _flickr.PlacesTagsForPlace(place.PlaceId, place.WoeId);

            //if (tags == null) throw new ArgumentNullException("Tags collection cannot be null!");

            //var clusters = _flickr.TagsGetClusters(tags[0].TagName);

            //if (clusters == null) throw new ArgumentNullException("Clusters collection cannot be null!");

            //var photos = _flickr.TagsGetClusterPhotos(clusters[0]);

            return photos ?? new PhotoCollection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public PhotoCollection SimpleSearch(ApiConfig apiConfig, string token)
        {
            FlickrNet.Flickr flickr = new FlickrNet.Flickr(apiConfig.ApiKey, secret);

            PhotoSearchOptions options = new PhotoSearchOptions();
            options.UserId = MY_NSID;// "1234567@N01"; // Your NSID
            options.PerPage = 100; // 100 is the default anyway

            return flickr.PhotosSearch(options);


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiConfig"></param>
        /// <param name="photoId"></param>
        /// <returns></returns>
        public FlickrNet.PhotoInfo GetPhotoInfoById(ApiConfig apiConfig,  string userName, string photoId)
        {
            PhotoInfo photoInfo = null;
            try
            {
                var pageNo = 1;
                PhotoCollection photos = new PhotoCollection();
                _flickr.ApiKey = apiConfig.ApiKey;
                _flickr.ApiSecret = apiConfig.Secret;

                photoInfo = _flickr.PhotosGetInfo(photoId);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

            return photoInfo ?? new PhotoInfo();
        }

        public PhotoCollection GetPhotosByUserId(ApiConfig apiConfig, string userName, int page = 1)
        {
            try
            {
                _flickr.ApiKey = "7c8f7d7e5280b333c218eb62544fdc19";
                _flickr.ApiSecret = "446eb7a3fe2150b2";

               //var foundUser = _flickr.PeopleFindByUserName(userName);
               // var photoInfor = _flickr.PhotosGetInfo(


                var psResult = _flickr.PhotosSearch(new PhotoSearchOptions
                {
                    UserId = userName,
                    Extras = PhotoSearchExtras.AllUrls | PhotoSearchExtras.Description | PhotoSearchExtras.Tags,
                    SortOrder = PhotoSearchSortOrder.Relevance,
                    Page = page

                });

                return psResult;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

            //throw NotImplementedException("NOT YET");
        }

    }
}
