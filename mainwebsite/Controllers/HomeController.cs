using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainWebsite.Models;

namespace MainWebsite.Controllers
{
    public class HomeController : MvControllerBase
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var model = CreateModel<DefaultModel>(action: x =>

               x.PageTitle = "Home Page"
           );

            return View(model);
        }

    }
}
