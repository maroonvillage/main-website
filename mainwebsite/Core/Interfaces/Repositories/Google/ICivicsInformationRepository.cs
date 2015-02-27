using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.DomainModel;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.Interfaces.Repositories.Google
{
    public interface ICivicsInformationRepository
    {

        JToken GetRepresentativesByAddress(ApiConfig apiConfig, ICivicsInformationRequestParameters requestParameters);

        JToken GetRepresentativesByDivision(ApiConfig apiConfig, ICivicsInformationRequestParameters requestParameters);
    }
}
