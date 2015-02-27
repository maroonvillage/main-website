using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.DomainModel;

namespace MaroonVillage.Core.Interfaces.Services.TED
{
    public interface ITedApiService
    {
        IEnumerable<Country> GetCountries(ITedApiRequestParameters tedApiRequestParameters);

    }
}
