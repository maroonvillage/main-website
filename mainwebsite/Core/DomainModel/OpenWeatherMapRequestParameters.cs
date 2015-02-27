using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.Enumerators;

namespace MaroonVillage.Core.DomainModel
{
    public interface IOpenWeatherMapRequestParameters
    {
        string Query { get; set; }
        int Id { get; set; }
        UnitTypes Units { get; set; }

    }
    public class OpenWeatherMapRequestParameters : IOpenWeatherMapRequestParameters
    {
        public string Query { get; set; }
        public int Id { get; set; }
        public UnitTypes Units { get; set; }
    }
}
