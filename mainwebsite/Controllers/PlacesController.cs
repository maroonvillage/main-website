using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MainWebsite.Extensions;
using MainWebsite.Models;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Enumerators;
using MaroonVillage.Core.Interfaces.Services;
using MaroonVillage.Core.Interfaces.Services.CsvDataSets;
using MaroonVillage.Core.Interfaces.Services.Flickr;
using MaroonVillage.Core.Interfaces.Services.Google;
using MaroonVillage.Core.Interfaces.Services.OpenWeatherMap;
using MaroonVillage.Core.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MainWebsite.Controllers
{
    public class PlacesController : MvControllerBase
    {

        private readonly IMvPlacesService _mvPlacesService;
        private readonly IMvPlacesMongoDbService _mvPlacesService2;
        private readonly IGooglePlacesService _googlePlacesService;
        private readonly ICsvDataSetService _csvDataSetService;
        private readonly IOpenWeatherMapService _openMapWeatherService;
        private readonly ICivicsInformationService _civicsInformationService;
        private readonly IFlickrService _flickrService;

        public PlacesController(IMvPlacesService mvPlacesService, IMvPlacesMongoDbService mvPlacesService2, IGooglePlacesService googlePlacesService,
            ICsvDataSetService csvDataSetService, IOpenWeatherMapService openMapWeatherService,
            ICivicsInformationService civicsInformationService, IFlickrService flickrService)
        {
            _mvPlacesService = mvPlacesService;
            _mvPlacesService2 = mvPlacesService2; 
            _googlePlacesService = googlePlacesService;
            _csvDataSetService = csvDataSetService;
            _openMapWeatherService = openMapWeatherService;
            _civicsInformationService = civicsInformationService;
            _flickrService = flickrService;
        }


        //
        // GET: /MvPlaces/
        public ActionResult Index()
        {
            var model = CreateModel<MvPlacesModel>(action: x =>

                x.SimplePlaces = _mvPlacesService2.GetAllPlaces()
            );
            return View(model);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult Detail(string name)
        {
            var model = CreateModel<MvPlacesModel>(action: x =>

                x.Place = _mvPlacesService.GetPlaceByName(name)
            );

            var servicePhotos = _flickrService.GetServicePhotosJson("Flickr");

            //TODO: Refactor this code to another action method to be called via jQuery/Ajax
            var requestParams = new OpenWeatherMapRequestParameters
            {
                Id = 0,
                Query = string.Format("{0},{1}", name, CoreHelperService.GetEnumDescription(IsoCountryCodesAlpha3.UnitedStates)),
                Units = MaroonVillage.Core.Enumerators.UnitTypes.Metric
            };

            var weather = _openMapWeatherService.GetCityWeather(requestParams);

            model.CountryName = weather["sys"]["country"].ToString();
            model.WeatherUnits = MaroonVillage.Core.Enumerators.UnitTypes.Metric;
            model.Tempurature = Convert.ToDecimal(weather["main"]["temp"]);
            model.Humidity = Convert.ToInt32(weather["main"]["humidity"]);
            model.WindDegree = Convert.ToDecimal(weather["wind"]["degree"]);
            model.WindSpeed = Convert.ToDecimal(weather["wind"]["speed"]);
            model.WeatherMain = weather["weather"][0]["main"].ToString();
            model.sunrise = Convert.ToInt32(weather["sys"]["sunrise"]);
            model.sunset = Convert.ToInt32(weather["sys"]["sunset"]);
            model.Pressure = Convert.ToDecimal(weather["main"]["pressure"]);



            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        public JsonResult GetPlaces(string name, string lat, string lng, string pageToken = null)
        {
            //build request parameters
            var mvPlace = _mvPlacesService.GetPlaceByName(name);

            var requestParams = new GooglePlacesRequestParameters
            {
                Lattitude = lat,
                Longitude = lng,
                PageToken = pageToken,
                Radius = 1800,
                Sensor = false,
                Types = mvPlace.NearByPlaceTypes
            };

            var search = _googlePlacesService.GetNearBySearch(requestParams);

            var result = new JsonNetResult
            {
                Data = search.ToObject<dynamic>(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Settings = { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            };


            return result ?? new JsonResult();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <param name="pageToken"></param>
        /// <returns></returns>
        public JsonResult GetCivilInfo(string address, string ocid)
        {
            JToken civicsInfo = null;

            var requestParams = new CivicsInformationRequestParameters
            {
                Address = !string.IsNullOrEmpty(address) ? address.Trim() : string.Empty,
                OcId = !string.IsNullOrEmpty(ocid) ? ocid.Trim() : string.Empty,
            };

            if (!string.IsNullOrEmpty(requestParams.Address))
            {
                civicsInfo = _civicsInformationService.GetRepresentativesByAddress(requestParams);
            }
            else if (!string.IsNullOrEmpty(requestParams.OcId))
            {
                civicsInfo = _civicsInformationService.GetRepresentativesByDivision(requestParams);
            }



            var result = new JsonNetResult
                       {
                           Data = civicsInfo.ToObject<dynamic>(),
                           JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                           Settings = { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
                       };


            return result ?? new JsonResult();

        }//end method: GetCivilInfo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public JsonResult GetFlickrPhotos(string query)
        {
            var places = _flickrService.GetPlacesByQuery(query);

            var result = new JsonNetResult
            {
                Data = places.ToObject<dynamic>(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Settings = { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            };

            return result ?? new JsonResult();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="placeName"></param>
        /// <returns></returns>
        public JsonResult GetWeatherInfo(string placeName)
        {
            return new JsonResult();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonNetResult GetAlprDataByCity(string city)
        {
            IEnumerable<AlprCapture> captures = _csvDataSetService.GetAlprDataByCity(city);

            return new JsonNetResult(Json(new
            {
                results = captures.Take(25000)
            }, JsonRequestBehavior.AllowGet));

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetServicePhotos()
        {
            var photoList = _flickrService.GetServicePhotosJson("Flickr");

            //photoList.to
            var result = new JsonNetResult
            {
                Data = photoList,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Settings = { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            };

            return result ?? new JsonResult();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public JsonResult GetCarouselPics(string query)
        {
            return new JsonResult();
        }



    }

    class PlaceTag
    {
        public PlaceTag()
        {

        }

        public int Counts { get; set; }
        public string Tag { get; set; }
    }
}
