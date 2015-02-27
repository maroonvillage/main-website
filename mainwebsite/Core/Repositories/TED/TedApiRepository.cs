using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Enumerators.TED;
using MaroonVillage.Core.Interfaces.Repositories.TED;
using MaroonVillage.Core.Interfaces.Services;
using MaroonVillage.Core.Services;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.Repositories.TED
{
    public class TedApiRepository : ITedApiRepository
    {
        private string _apiKey;
        private const string BaseUrl = "";
        private readonly IApiRequest _apiRequest;
        private readonly IApiService _apiService;

        public TedApiRepository(IApiRequest apiRequest, IApiService apiService)
        {
            _apiRequest = apiRequest;
            _apiService = apiService;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tedApiRequestParameters"></param>
        /// <returns></returns>
        public JToken GetCountries(ITedApiRequestParameters tedApiRequestParameters)
        {
            if (string.IsNullOrEmpty(_apiRequest._ApiConfig.ApiKey)) throw new ArgumentNullException("The API Key may not be empty or null.");

            var requestUrl = new StringBuilder(_apiRequest._ApiConfig.BaseUrl);

            if (string.IsNullOrEmpty(requestUrl.ToString())) throw new ArgumentNullException("The Base Url may not be empty or null.");


            requestUrl.Append(string.Format("v1/{0}.json", CoreHelperService.GetEnumDescription(TedApiResourceTypes.Countries)));
            //Get the parameters to be appended to the request uri
            var requestInputs = _apiService.GetApiRequestInputsByConfigId(_apiRequest._ApiConfig.ApiConfigId).OrderBy(x => x.Ordinal);

            //Build request url ...
            if (!string.IsNullOrEmpty(tedApiRequestParameters.Fields))
            {
                requestUrl.Append("&fields=" + tedApiRequestParameters.Fields.Trim());
            }

            if (!string.IsNullOrEmpty(tedApiRequestParameters.Filter))
            {
                requestUrl.Append("&filter=" + tedApiRequestParameters.Filter.Trim());
            }

            if (!string.IsNullOrEmpty(tedApiRequestParameters.Language))
            {
                requestUrl.Append("&language=" + tedApiRequestParameters.Language.Trim());
            }


            if (tedApiRequestParameters.Limit > 0)
            {
                requestUrl.Append("&limit=" + Convert.ToString(tedApiRequestParameters.Limit));
            }

            if (tedApiRequestParameters.Offset > 0)
            {
                requestUrl.Append("&offset=" + Convert.ToString(tedApiRequestParameters.Offset));
            }

            if (!string.IsNullOrEmpty(tedApiRequestParameters.Order))
            {
                requestUrl.Append("&order=" + tedApiRequestParameters.Order.Trim());
            }

            if (!string.IsNullOrEmpty(tedApiRequestParameters.Sort))
            {
                requestUrl.Append("&sort=" + tedApiRequestParameters.Sort.Trim());
            }

            if (tedApiRequestParameters.SuppressErrorCodes)
            {
                requestUrl.Append("&suppress_error_codes=false");
            }


            // request parameter is complete ...
            HttpClient client = new HttpClient { BaseAddress = new Uri(_apiRequest._ApiConfig.BaseUrl) };
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

    }
}
