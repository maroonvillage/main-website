using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Interfaces.Repositories.Socrata;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.Repositories.Socrata
{
    public class OpenDataRepository : BaseRepository, IOpenDataRepository
    {
        ApiConfig _apiConfiguration;

        private string _baseUrl;
        public string BaseUrl
        {
            get
            {
                return !string.IsNullOrEmpty(_baseUrl) ? _baseUrl : string.Empty;
            }
            set
            {
                _baseUrl = !string.IsNullOrEmpty(value) ? value : string.Empty;
            }
        }

        public OpenDataRepository()
        {
            _apiConfiguration = new ApiConfig();
        }

        public OpenDataRepository(ApiConfig apiConfig)
        {
            _apiConfiguration = apiConfig;
        }

        public JToken GetSocrataDataset(string dataSetId)
        {
            //Get base Url and other inputs from the ApiConfig ...
            var requestUrl = new StringBuilder();


            requestUrl.Append(string.Format("{0}{1}", dataSetId, ".json"));

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
    }
}
