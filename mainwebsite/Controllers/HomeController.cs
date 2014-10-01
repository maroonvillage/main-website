using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MainWebsite.Controllers
{
    public class HomeController : MvControllerBase
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
