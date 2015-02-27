using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Enumerators;
using MaroonVillage.Core.Interfaces.Repositories;
using MaroonVillage.Core.Interfaces.Repositories.CsvDataSets;
using MaroonVillage.Core.Interfaces.Services;
using MaroonVillage.Core.Interfaces.Services.CsvDataSets;
using MaroonVillage.Core.Repositories.CsvDataSets;

namespace MaroonVillage.Core.Services.CsvDataSets
{
    public class CsvDataSetService : ICsvDataSetService
    {
        private readonly ICacheService _cacheService;
        private readonly IAlprRepository _alprRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheService"></param>
        public CsvDataSetService(ICacheService cacheService, IAlprRepository alprRepository)
        {
            _cacheService = cacheService;
            _alprRepository = alprRepository;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public IEnumerable<AlprCapture> GetAlprDataByCity(string cityName)
        {
            IEnumerable<AlprCapture> captureList = null;
            var cacheKey = string.Format(CoreHelperService.GetEnumDescription(CacheKeys.CsvDataSet), cityName);
            try
            {
                    if (_cacheService.Contains(cacheKey))
                    {
                        captureList = (IEnumerable<AlprCapture>)_cacheService[cacheKey];
                    }
                    else
                    {
                        captureList = _alprRepository.GetAlprDataByCity(cityName);
                        if (captureList != null)
                        {
                            _cacheService.Add(cacheKey, captureList, CoreHelperService.GetEnumDefaultValue(CacheKeys.CsvDataSet));
                        }
                    }
            }
            catch (Exception ex)
            {
                throw new Exception("Core.Services: GetVehiclesByDealerId " + ex.Message);

            }// end try-catch

            return captureList;
        }
    }
}
