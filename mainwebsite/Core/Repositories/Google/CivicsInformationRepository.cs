using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Interfaces.Repositories.Google;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.Repositories.Google
{
    public class CivicsInformationRepository : ICivicsInformationRepository
    {
        public CivicsInformationRepository()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestParameters"></param>
        /// <returns></returns>
        public JToken GetRepresentativesByAddress(ApiConfig apiConfig, ICivicsInformationRequestParameters requestParameters)
        {
            try
            {
                if (string.IsNullOrEmpty(apiConfig.ApiKey)) throw new ArgumentNullException("API Key cannot be null or empty!");

                //The free version of this API does not have an API Key
                var requestUrl = new StringBuilder(apiConfig.BaseUrl);

                if (!string.IsNullOrEmpty(requestParameters.Address))
                {
                    requestUrl.Append(string.Format("?address={0}", requestParameters.Address));
                }
                else if (!string.IsNullOrEmpty(requestParameters.OcId))
                {
                    requestUrl.Append(string.Format("/{0}", requestParameters.OcId));
                }

                requestUrl.Append(string.Format("&includeOffices={0}", "true"));


                requestUrl.Append(string.Format("&key={0}", apiConfig.ApiKey));

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
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestParameters"></param>
        /// <returns></returns>
        public JToken GetRepresentativesByDivision(ApiConfig apiConfig, ICivicsInformationRequestParameters requestParameters)
        {
            throw new NotImplementedException("Not yet.");

        }


    }
}
