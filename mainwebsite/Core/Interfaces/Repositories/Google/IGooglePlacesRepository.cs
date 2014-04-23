using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.DomainModel;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.Interfaces.Repositories.Google
{
    public interface IGooglePlacesRepository
    {
        ApiConfig GetGooglePlacesApiConfiguration();
        JToken GetNearBySearch(IGooglePlacesRequestParameters requestParams);
        JToken GetTextSearch(IGooglePlacesRequestParameters requestParams);
        JToken GetRadarSearch(IGooglePlacesRequestParameters requestParams);

    }
}
