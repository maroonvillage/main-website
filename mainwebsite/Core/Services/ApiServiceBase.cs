using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Interfaces.Services;

namespace MaroonVillage.Core.Services
{
    public class ApiServiceBase
    {
        public virtual T ApiRequest<T>(IApiRequestParameters requestParameters)
        {
            return default(T);
        }
    }
}
