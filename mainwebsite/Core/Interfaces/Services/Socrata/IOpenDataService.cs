using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.DomainModel;

namespace MaroonVillage.Core.Interfaces.Services.Socrata
{
    public interface IOpenDataService
    {
        List<LiquorLicense> GetNewLiquorLicenses(string apiName, string dataSetId);
    }
}
