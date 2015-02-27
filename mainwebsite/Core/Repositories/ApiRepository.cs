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
                var mySqlCommand = new MySqlCommand();
                mySqlCommand.CommandText = string.Format("CALL mvmasterdb.GetApiConfigByName('{0}')",apiName);
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                //mySqlCommand.Parameters.Add("?apiConfigName", string.Format("'{0}'",apiName));
                //mySqlCommand.Parameters["?apiConfigName"].Direction = ParameterDirection.Input;
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
                                ApiConfigId = ParseInt(ParseString(reader["ApiConfigId"])),
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
            catch (MySqlException msqlEx)
            {
                throw msqlEx;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiConfigId"></param>
        /// <returns></returns>
        public IEnumerable<ApiRequestInput> GetApiRequestInputsByConfigId(int apiConfigId)
        {
            var apiRequestInputs = new List<ApiRequestInput>();
            try
            {
                var mySqlCommand = new MySqlCommand("mvmasterdb.GetApiRequestInputByConfigId");
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.AddWithValue("?apiConfigId", (object)apiConfigId);
                mySqlCommand.Parameters["?apiConfigId"].Direction = ParameterDirection.Input;
                using (var connection = GetMySqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    connection.Open();
                    using (var reader = MySqlHelper.ExecuteReader(connection, mySqlCommand.CommandText))
                    {
                        while (reader.Read())
                        {
                            var requestInput = new ApiRequestInput
                            {
                                RequestInputId = ParseInt(ParseString(reader["RequestInputId"])),
                                ApiConfigId = ParseInt(ParseString(reader["ApiConfig"])),
                                ParameterName = ParseString(reader["ParameterName"]),
                                IsComponent = ParseBoolean(reader["IsComponent"]),
                                IsRequired = ParseBoolean(reader["IsRequired"]),
                                PrimaryId = ParseInt(ParseString(reader["PrimaryId"])),
                                IsKey = ParseBoolean(reader["IsKey"]),
                                Ordinal = ParseInt(ParseString(reader["Ordinal"])),
                                IsFileName = ParseBoolean(reader["IsFileName"]),
                            };
                            apiRequestInputs.Add(requestInput);
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
            return apiRequestInputs;
        }
    }
}
