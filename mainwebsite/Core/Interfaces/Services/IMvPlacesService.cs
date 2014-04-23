using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Interfaces.Repositories;

namespace MaroonVillage.Core.Interfaces.Services
{
    public interface IMvPlacesService
    {
        IEnumerable<MvPlace> GetAllMvPlaces();

        MvPlace GetPlaceByName(string name);


    }
}
