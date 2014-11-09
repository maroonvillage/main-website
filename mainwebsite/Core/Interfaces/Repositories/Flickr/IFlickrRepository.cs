using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.DomainModel;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.Interfaces.Repositories.Flickr
{
    public interface IFlickrRepository
    {
        JToken GetPhotos(IFlickrPhotoSearchParameters requestParameters);
    }
}
