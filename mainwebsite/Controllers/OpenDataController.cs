using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainWebsite.Models;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Interfaces.Services;
using MaroonVillage.Core.Interfaces.Services.Socrata;

namespace MainWebsite.Controllers
{
    public class OpenDataController : MvControllerBase
    {
        private readonly IApiService _apiService;
        private readonly IOpenDataService _openDataService;

        public OpenDataController(IOpenDataService openDataService, IApiService apiService)
        {
            _apiService = apiService;
            _openDataService = openDataService;
        }
        //
        // GET: /OpenData/

        public ActionResult Index(string id)
        {
            var list = new List<Field>();

            
            var model = CreateModel<OpenDataModel>();

            if (string.IsNullOrEmpty(id))
            {
                var states = new List<UsState>();
                //Display states ...
                states.Add(new UsState
                {
                    StateAbbreviation = "MO",
                    StateFullName = "Missouri"
                    
                });
                model.CurrentState = string.Empty;
                model.UsStates = states;
            }
            else
            {
                list.Add(new Field
                {
                    Id = "dymb-xy5c",
                    Name = "Missouri New Liquor Licenses"
                });
                list.Add(new Field
                {
                    Id = "nytw-fmz3",
                    Name = "Missouri Alcohol Licenses Out of Business"
                });

                model.CurrentState = id;
                model.OpenDataDatasets = list;

               //Display state datasets ...
               // return View(model);
            }


            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(string state, string id)
        {
            var model = CreateModel<OpenDataModel>();
            //use the id to retreive and list all of data available for this state...
            var apiName = string.Format("{0}{1}", model.ApiNamePrefix, state);
            var apiSvc = _apiService.GetApiConfigByName(apiName);

            var list = _openDataService.GetNewLiquorLicenses(apiName, id);
            model.LiquorLicenses = list;

            return View(model);
        }
      
    }
}
