using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Enumerators;
using MaroonVillage.Core.Enumerators.Google;
using MaroonVillage.Core.Interfaces.Repositories.GoogleApi;
using MaroonVillage.Core.Services;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.Repositories.GoogleApi
{
    public class GooglePlacesRepository : IGooglePlacesRepository
    {
        private const string BaseUrl = "https://maps.googleapis.com/maps/api/place/";
        private string _apiKey;
        //TODO: INJECT INTERFACE with necessary properties instead of separate ones
        public GooglePlacesRepository()
        {
            _apiKey = ConfigurationManager.AppSettings["GoogleApiKey"] != null ? Convert.ToString(ConfigurationManager.AppSettings["GoogleApiKey"]) : string.Empty;
        }

        public ApiConfig GetGooglePlacesApiConfiguration()
        {
            throw new NotImplementedException("");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        public JToken GetNearBySearch(IGooglePlacesRequestParameters requestParams)
        {
            var requestUrl = new StringBuilder(@"nearbysearch/");
            requestUrl.Append(string.Format("{0}", CoreHelperService.GetEnumDescription(ResponseOutputTypes.Json), "/"));

            //Add required parameters 
            //key
            if (!string.IsNullOrEmpty(_apiKey))
                requestUrl.Append(string.Format("?key={0}", _apiKey));
            else
                throw new Exception("Required request parameter 'key' not set.");

            if (!string.IsNullOrEmpty(requestParams.PageToken))
            {
                requestUrl.Append(string.Format("{0}", requestParams.PageToken));
            }
            else
            {
                //location
                if (string.IsNullOrEmpty(requestParams.Lattitude) | string.IsNullOrEmpty(requestParams.Longitude))
                    throw new Exception("Required request parameter 'location' not set.");
                else
                    requestUrl.Append(string.Format("&location={0}", string.Format("{0},{1}", requestParams.Lattitude, requestParams.Longitude)));

                //radius
                var radius = requestParams.Radius > 0 ? string.Format("{0}", Convert.ToString(requestParams.Radius)) : string.Empty;
                if (requestParams.Radius > 0)
                    requestUrl.Append(string.Format("&radius={0}", Convert.ToString(requestParams.Radius)));
                else
                    throw new Exception("Required request parameter 'radius' not set.");

                if (requestParams.Sensor == null) throw new Exception("Required request parameter 'sensor' not set.");
                var sensor = string.Format("&sensor={0}", requestParams.Sensor.ToString().ToLower());


                //Append optional parameters onto request if any
                //keyword
                if (!string.IsNullOrEmpty(requestParams.Keyword)) requestUrl.Append(string.Format("&keyword={0}", requestParams.Keyword));

                //min price
                if (requestParams.MinPrice > CoreHelperService.GetEnumDefaultValue(GooglePlacesPriceTypes.NotSet)) 
                    requestUrl.Append(string.Format("&minprice={0}", Convert.ToString(requestParams.MinPrice)));
                //max price
                if (requestParams.MaxPrice > CoreHelperService.GetEnumDefaultValue(GooglePlacesPriceTypes.NotSet) && requestParams.MaxPrice > requestParams.MinPrice)
                    requestUrl.Append(string.Format("&maxprice={0}", Convert.ToString(requestParams.MaxPrice)));

                //name
                if (!string.IsNullOrEmpty(requestParams.Name))
                    requestUrl.Append(string.Format("&name={0}", Convert.ToString(requestParams.Name)));

                //opennow
                if (requestParams.ShowOnlyOpenNow) 
                    requestUrl.Append(string.Format("&{0}", requestParams.OpenNow));
                
                //types
                if(!string.IsNullOrEmpty(requestParams.Types))
                    requestUrl.Append(string.Format("&types{0}", requestParams.Types));

            }

            // request parameter is complete ...
            HttpClient client = new HttpClient { BaseAddress = new Uri(BaseUrl) };
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

        public JToken GetTextSearch(IGooglePlacesRequestParameters requestParams)
        {
            throw new NotImplementedException("");
        }

        public JToken GetRadarSearch(IGooglePlacesRequestParameters requestParams)
        {
            throw new NotImplementedException("");
        }
    }
}
