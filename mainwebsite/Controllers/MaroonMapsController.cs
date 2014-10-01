using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainWebsite.Models;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Interfaces.Services.Google;
using Newtonsoft.Json.Linq;

namespace MainWebsite.Controllers
{
    public class MaroonMapsController : MvControllerBase
    {
        private readonly IGeoCodingService _geoCodingService;

        public MaroonMapsController(IGeoCodingService geoCodingService)
        {
            _geoCodingService = geoCodingService;
        }

        //
        // GET: /MaroonMaps/
        public ActionResult Index()
        {
            var model = CreateModel<MaroonMapsModel>(x => x.SearchToken = string.Empty);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string searchtoken)
        {
            if (ModelState.IsValid)
            {
                var model = CreateModel<MaroonMapsModel>(x => x.SearchToken = searchtoken);
                //Make call to Geo Coding Service ...
                var requestParams = new GeoCodingRequestParameters
                {
                    Address = searchtoken
                };

                var resp = _geoCodingService.GeoCode(requestParams);
                var srchAddresses = new List<AddressResult>();
                foreach (dynamic address in resp["results"])
                {
                    var addr = new AddressResult
                    {
                        FormattedAddress = address.formatted_address,
                        Lattitude = address.geometry.location.lat,
                        Longitude = address.geometry.location.lng
                    };

                    srchAddresses.Add(addr);
                }

                model.Locations = srchAddresses;

                return View(model);
            }
            return View();
        }

        public ActionResult Place(string title, string lat, string lon)
        {

            var model = CreateModel<MaroonMapsModel>(x => x.DefaultLocation = new AddressResult
            {
                FormattedAddress=title,
                Longitude = lon,
                Lattitude = lat
            });

            return View(model);

        }

    }
}
