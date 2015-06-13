using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Interfaces.Repositories;
using MaroonVillage.Core.Interfaces.Services;

namespace MaroonVillage.Core.Services
{
    public class MvPlacesMongoDbService
    {
        private readonly ICacheService _cacheService;
        private readonly IMvPlacesMongoDbRepository _mvPlacesMongoDbRepository;

        /// <summary>
        /// 
        /// </summary>
        public MvPlacesMongoDbService(IMvPlacesMongoDbRepository mvPlacesMongoDbRepository, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _mvPlacesMongoDbRepository = mvPlacesMongoDbRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<SimplePlace> GetAllPlaces()
        {
            return _mvPlacesMongoDbRepository.GetAllPlaces();
        }

    }
}
