using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.DomainModel;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.Interfaces.Services.OpenWeatherMap
{
    public interface IOpenWeatherMapService
    {
        JToken GetCityWeather(IOpenWeatherMapRequestParameters requestParams);
    }
}
