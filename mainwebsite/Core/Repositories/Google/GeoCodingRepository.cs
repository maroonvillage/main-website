using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Enumerators;
using MaroonVillage.Core.Interfaces.Repositories.GoogleApi;
using MaroonVillage.Core.Services;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.Repositories.GoogleApi
{
    public class GeoCodingRepository : IGeoCodingRepository
    {
        private const string BaseUrl = "https://maps.googleapis.com/maps/api/geocode/";
        private string _apiKey;
        //TODO: INJECT INTERFACE with necessary properties instead of separate ones
        public GeoCodingRepository()
        {
            _apiKey = ConfigurationManager.AppSettings["GoogleApiKey"] != null ? Convert.ToString(ConfigurationManager.AppSettings["GoogleApiKey"]) : string.Empty;
        }

        public ApiConfig GetGeoCodingApiConfiguration()
        {
            return new ApiConfig();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        public JToken GeoCode(IGeoCodingRequestParameters requestParams)
        {
            var requestUrl = new StringBuilder();
            requestUrl.Append(string.Format("{0}", CoreHelperService.GetEnumDescription(ResponseOutputTypes.Json), "/"));
            //TODO: BUILD PARAMETERS BASED UPON WHAT IS PASSED INTO THE METHOD
            //Check for required parameters and throw an exception if not present
            var address = string.Empty;
            var components = string.Empty;

            if (requestParams.Sensor == null) throw new Exception("Required request parameter 'sensor' not set.");
            var sensor = string.Format("&sensor={0}", requestParams.Sensor.ToString().ToLower());
            if (string.IsNullOrEmpty(requestParams.Address))
            {
                //check for components since address is empty
                if (!requestParams.RequestComponents.IsSet) throw new Exception("Required request parameter 'address' or 'component' not set.");

            }
            address = string.Format("?address={0}", requestParams.Address);

            //Add this conditionally ...
            //components = string.Format("&components={0}", requestParams.RequestComponents.GetComponentFilterString());


            //Append required parameters onto requrest
            requestUrl.Append(address);
            //requestUrl.Append(components);
            requestUrl.Append(sensor);

            //Append optional parameters onto request if any
            //TEMPORARY: WILL ONLY ADD Key, and Language for now to get this working
            if (!string.IsNullOrEmpty(_apiKey)) requestUrl.Append(string.Format("&key={0}", _apiKey));
            if (!string.IsNullOrEmpty(requestParams.Language)) requestUrl.Append(string.Format("&language={0}", requestParams.Language));

            // request parameter is complete ...

            HttpClient client = new HttpClient { BaseAddress = new Uri(BaseUrl) };
            ////Request.Cookies["DealerLoginCookie"]["DLUserName"]
            ////Request.Cookies["DealerLoginCookie"]["DLPassword"].ToString()

            ////client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("applicaiton/json"));

            var svcResponse = client.GetAsync(requestUrl.ToString()).Result;

            if (svcResponse.IsSuccessStatusCode)
            {
                //MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
                // Use the JSON formatter to create the content of the request body.
                //HttpContent content = new ObjectContent<AutoCheckReport>(scoreReport, jsonFormatter);
                return svcResponse.Content.ReadAsAsync<JToken>().Result;
            }
            return null;
        }

    }
}
