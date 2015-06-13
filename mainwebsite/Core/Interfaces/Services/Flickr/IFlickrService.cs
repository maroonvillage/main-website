using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlickrNet;
using MaroonVillage.Core.DomainModel;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.Interfaces.Services.Flickr
{
    public interface IFlickrService
    {
        JToken GetPhotos(IFlickrPhotoSearchParameters requestParameters);
        Place GetPlacesByLatLon(double latt, double lon);
        JToken GetPlacesByQuery(string query);
        PhotoCollection SimpleSearch(ApiConfig apiConfig, string token);
        JToken GetServicePhotosJson(string serviceName);
        IList<ServicePhoto> GetServicePhotos(string serviceName);
    }
}
