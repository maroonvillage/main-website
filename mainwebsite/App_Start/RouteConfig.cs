using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MainWebsite.Helpers;

namespace MainWebsite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "maroonmaps-place",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "MaroonMaps", action = "Place", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "PlacesDefault",
               url: "{controller}/{name}",
               defaults: new { controller = "Places", action = "Index", name = UrlParameter.Optional }
           );


            

           


            
        }
    }
}