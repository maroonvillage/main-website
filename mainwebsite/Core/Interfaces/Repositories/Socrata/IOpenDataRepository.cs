using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.Interfaces.Repositories.Socrata
{
    public interface IOpenDataRepository
    {
        string BaseUrl { get; set; }
        JToken GetSocrataDataset(string dataSetId);
    }
}
