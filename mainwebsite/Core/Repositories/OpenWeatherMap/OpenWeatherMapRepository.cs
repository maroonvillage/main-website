using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Interfaces.Repositories.OpenWeatherMap;
using MaroonVillage.Core.Services;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.Repositories.OpenWeatherMap
{
    public class OpenWeatherMapRepository : IOpenWeatherMapRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiConfig"></param>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        public JToken GetCityWeather(ApiConfig apiConfig, IOpenWeatherMapRequestParameters requestParams)
        {
            try
            {
                //The free version of this API does not have an API Key
                var requestUrl = new StringBuilder(apiConfig.BaseUrl);

                if (requestParams.Id > 0)
                {
                    requestUrl.Append(string.Format("?id={0}", requestParams.Id));
                }
                else if (!string.IsNullOrEmpty(requestParams.Query))
                {
                    requestUrl.Append(string.Format("?q={0}", requestParams.Query));
                }

                if(requestParams.Units != Enumerators.UnitTypes.None)
                {
                    requestUrl.Append(string.Format("&units={0}", CoreHelperService.GetEnumDescription(requestParams.Units)));
                }


                // request parameter is complete ...
                HttpClient client = new HttpClient { BaseAddress = new Uri(apiConfig.BaseUrl) };
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("applicaiton/json"));

                var svcResponse = client.GetAsync(requestUrl.ToString()).Result;
                if (svcResponse.IsSuccessStatusCode)
                {
                    //MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
                    // Use the JSON formatter to create the content of the request body.
                    return svcResponse.Content.ReadAsAsync<JToken>().Result;
                }
                return null;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return null;

        }// end  method: GetCityWeather

    }
}
