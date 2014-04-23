using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainWebsite.Extensions;
using MainWebsite.Models;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Interfaces.Services;
using MaroonVillage.Core.Interfaces.Services.Google;
using Newtonsoft.Json;

namespace MainWebsite.Controllers
{
    public class PlacesController : MvControllerBase
    {
        private readonly IMvPlacesService _mvPlacesService;
        private readonly IGooglePlacesService _googlePlacesService;


        public PlacesController(IMvPlacesService mvPlacesService, IGooglePlacesService googlePlacesService)
        {
            _mvPlacesService = mvPlacesService;
            _googlePlacesService = googlePlacesService;
        }

        //
        // GET: /MvPlaces/

        public ActionResult Index()
        {

            var model = CreateModel<MvPlacesModel>(action: x => 
                
                x.MvPlaces = _mvPlacesService.GetAllMvPlaces()
            );
           
            return View(model);
        }

        public ActionResult Detail(string name)
        {
            var model = CreateModel<MvPlacesModel>(action: x => 
                
                x.Place = _mvPlacesService.GetPlaceByName(name)
            );

            //var requestParams = new GooglePlacesRequestParameters
            //{
            //    Lattitude = lat,
            //    Longitude = lng,
            //    PageToken = pageToken,
            //    Radius = 1800,
            //    Sensor = false,
            //    Types = mvPlace.NearByPlaceTypes
            //};

            //var search = _googlePlacesService.GetNearBySearch(requestParams);

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

    }
}
