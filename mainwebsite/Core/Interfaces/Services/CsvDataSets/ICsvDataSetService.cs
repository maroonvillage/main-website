using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.DomainModel;

namespace MaroonVillage.Core.Interfaces.Services.CsvDataSets
{
   public interface ICsvDataSetService
    {
        IEnumerable<AlprCapture> GetAlprDataByCity(string cityName);
    }
}
