using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.DomainModel;

namespace MaroonVillage.Core.Interfaces.Repositories.CsvDataSets
{
    public interface IAlprRepository
    {

        IEnumerable<AlprCapture> GetAlprDataByCity(string cityName);
    }
}
