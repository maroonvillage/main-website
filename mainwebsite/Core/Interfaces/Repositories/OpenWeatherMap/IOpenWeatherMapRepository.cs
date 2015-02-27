using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.DomainModel;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.Interfaces.Repositories.OpenWeatherMap
{
    public interface IOpenWeatherMapRepository
    {
        JToken GetCityWeather(ApiConfig apiConfig, IOpenWeatherMapRequestParameters requestParams);
    }
}
