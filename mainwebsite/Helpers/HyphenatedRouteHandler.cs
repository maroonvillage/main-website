using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MainWebsite.Helpers
{
    public class HyphenatedRouteHandler : MvcRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            requestContext.RouteData.Values["controller"] = requestContext.RouteData.Values["controller"].ToString().Replace("-", string.Empty);
            requestContext.RouteData.Values["action"] = requestContext.RouteData.Values["action"].ToString().Replace("-", string.Empty);
            return base.GetHttpHandler(requestContext);
        }
    }
}