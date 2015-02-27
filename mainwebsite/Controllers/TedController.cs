using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainWebsite.Models;
using MaroonVillage.Core.Interfaces.Services.TED;

namespace MainWebsite.Controllers
{
    public class TedController : MvControllerBase
    {

        private readonly ITedApiService _tedApiService;

        public TedController(ITedApiService tedApiService)
        {
            _tedApiService = tedApiService;
        }
        //
        // GET: /Ted/

        public ActionResult Index()
        {
            var model = CreateModel<DefaultModel>(action: x =>

               x.PageTitle = "Ted Talks Home"
           );



            //_tedApiService.GetCountries();
            return View(model);
        }

    }
}
