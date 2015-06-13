using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.DomainModel;

namespace MaroonVillage.Core.Interfaces.Repositories
{
    public interface IMvPlacesRepository
    {
        IEnumerable<MvPlace> GetAllMvPlaces();
        IEnumerable<ServicePhoto> GetServicePhotos(string serviceName);
    }
}
