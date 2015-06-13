using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainWebsite.Models;

namespace MainWebsite.Controllers
{
    public class AdminController : MvControllerBase
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {

            //ViewBag.ReturnUrl = returnUrl;
            //var model = CreateModel<LoginModel>(pageMachineName: "Login", page: SitePageType.Login);

            //return View(model);
            return View();

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel model, string returnUrl)
        {
            //LoadModelData(model, page: SitePageType.Login);

            if (ModelState.IsValid)
            {
                //returnUrl = returnUrl ?? model.ReturnUrl;
               // var customerId = _customerService.AuthenticateCustomer(Dealer.DealerId, model.EmailAddress, model.Password);
                //if (customerId > 0)
                //{
                //    SetCookie(customerId, model.RememberMe);
                //    model.Customer = _customerService.GetCustomerById(customerId);
                //    return RedirectToLocal(returnUrl);
                //}
                //else
                //{
                //    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                //}
            }

            //return View(model);
            return View();
        }// end method

        [HttpGet]
        public ActionResult LogOff()
        {
            //FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
