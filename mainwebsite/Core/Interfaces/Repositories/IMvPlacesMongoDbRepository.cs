using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using MaroonVillage.Core.DomainModel;

namespace MaroonVillage.Core.Interfaces.Repositories
{
    public interface IMvPlacesMongoDbRepository
    {
        List<SimplePlace> GetAllPlaces();

        List<SimplePlace> GetPlaceByName(string name);

        List<SimplePlace> GetPlaceByCityName(string cityName);

      }
}
