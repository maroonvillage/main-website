using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainWebsite.Models;

namespace MainWebsite.Controllers
{
    public class MvControllerBase : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext requestContext)
        {
            base.OnActionExecuting(requestContext);

        }

        /// <summary>
        /// Creates the model and loads the default model data
        /// </summary>
        /// <typeparam name="T">The type of model to create.</typeparam>
        /// <param name="action">Optionally modify the returned model object.</param>
        /// <returns></returns>
        protected T CreateModel<T>(Action<T> action = null)
            where T : DefaultModel, new()
        {
            return LoadModelData(new T(), action);
        }

        /// <summary>
        /// Loads the default data into the given model
        /// </summary>
        /// <typeparam name="T">The type of model to create.</typeparam>
        /// <param name="model">The model to load the data into.</param>
        /// <param name="action">Optionally modify the returned model object.</param>
        /// <returns></returns>
        protected virtual T LoadModelData<T>(T model, Action<T> action = null)
            where T : DefaultModel
        {
            if (HttpContext.User != null && HttpContext.User.Identity.IsAuthenticated)
            {
                model.User = HttpContext.User;
            }

            if (action != null)
            {
                action(model);
            }

            return model;
        }

    }
}
