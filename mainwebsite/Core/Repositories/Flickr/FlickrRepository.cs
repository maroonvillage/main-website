using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Interfaces.Repositories.Flickr;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.Repositories.Flickr
{
    public class FlickrRepository : IFlickrRepository
    {

        public string BaseUrl
        {
            get
            {
                return "https://api.flickr.com/services/rest/";
            }
        }

        /*
         * <photos page="2" pages="89" perpage="10" total="881">
	<photo id="2636" owner="47058503995@N01" 
		secret="a123456" server="2" title="test_04"
		ispublic="1" isfriend="0" isfamily="0" />
	<photo id="2635" owner="47058503995@N01"
		secret="b123456" server="2" title="test_03"
		ispublic="0" isfriend="1" isfamily="1" />
	<photo id="2633" owner="47058503995@N01"
		secret="c123456" server="2" title="test_01"
		ispublic="1" isfriend="0" isfamily="0" />
	<photo id="2610" owner="12037949754@N01"
		secret="d123456" server="2" title="00_tall"
		ispublic="1" isfriend="0" isfamily="0" />
</photos>
         */



        public JToken GetPhotos(IFlickrPhotoSearchParameters requestParameters)
        {
            var requestUrl = new StringBuilder(@"?method=flickr.photos.search");

            if (!string.IsNullOrEmpty(requestParameters.Key))
                requestUrl.Append(string.Format("&api_key={0}", requestParameters.Key));
            else
                throw new Exception("Required request parameter 'api_key' not set.");

            if (!string.IsNullOrEmpty(requestParameters.Tags))
            {
                requestUrl.Append(string.Format("&tags={0}", requestParameters.Tags));
            }

            if (!string.IsNullOrEmpty(requestParameters.Text))
            {
                requestUrl.Append(string.Format("&text={0}", requestParameters.Text));
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


            throw new NotImplementedException("Method: GetPhotos has not been implemented yet.");
        }
    }
}
