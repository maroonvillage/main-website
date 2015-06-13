using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlickrNet;
using MaroonVillage.Core.DomainModel;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.Interfaces.Repositories.Flickr
{
    public interface IFlickrRepository
    {
        JToken GetPhotos(IFlickrPhotoSearchParameters requestParameters);
        Place GetPlacesByLatLon(double latt, double lon);
        PlaceCollection GetPlacesByQuery(string query);
        PhotoCollection GetPhotosByTag(Place place);
        PhotoCollection SimpleSearch(ApiConfig apiConfig, string token);
        FlickrNet.PhotoInfo GetPhotoInfoById(ApiConfig apiConfig, string userId, string photoId);
        PhotoCollection GetPhotosByUserId(ApiConfig apiConfig, string userId, int page = 1);


    }
}
