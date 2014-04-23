using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Interfaces.Repositories;
using MaroonVillage.Core.Services;
using MySql.Data.MySqlClient;

namespace MaroonVillage.Core.Repositories
{
    public class ApiRepository : BaseRepository, IApiRepository
    {

        public ApiRepository()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApiConfig> GetApiConfigurations()
        {
            throw new NotImplementedException("GetApiConfigurations");
        }

        public ApiConfig GetApiConfigById(int apiConfigId)
        {

            throw new NotImplementedException("GetApiConfigById");

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiName"></param>
        /// <returns></returns>
        public ApiConfig GetApiConfigByName(string apiName)
        {
            var apiConfig = new ApiConfig();
            try
            {
                var mySqlCommand = new MySqlCommand("mvmasterdb.GetApiConfigByName");
                mySqlCommand.CommandType = CommandType.StoredProcedure;

                using (var connection = GetMySqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    connection.Open();
                    using (var reader = MySqlHelper.ExecuteReader(connection, mySqlCommand.CommandText))
                    {
                        while (reader.Read())
                        {
                            apiConfig = new ApiConfig
                            {
                                ApiConfigId = ParseInt(ParseString(reader["ApiConfig"])),
                                ApiName = ParseString(reader["ApiName"]),
                                ApiDescription = ParseString(reader["ApiDescription"]),
                                BaseUrl = ParseString(reader["BaseUrl"]),
                                ApiKey = ParseString(reader["ApiKey"]),
                                RequestQuota = ParseInt(ParseString(reader["RequestQuota"])),
                                RequestPeriod = ParseString(reader["RequestPeriod"])
                            };

                            break;
                        }
                    }

                }
            }
            catch (SqlException sqlEx)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            return apiConfig;

        }
        public ApiConfig GetApiConfigByKey(string apiKey)
        {
            throw new NotImplementedException("GetApiConfigByKey");
        }
    }
}
