﻿using System;
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
    public class MvPlacesRepository : BaseRepository, IMvPlacesRepository
    {
        public MvPlacesRepository()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MvPlace> GetAllMvPlaces()
        {
            var places = new List<MvPlace>();
            try
            {
                var mySqlCommand = new MySqlCommand("mvmasterdb.GetAllMaroonVillagePlaces");
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("?usr", (object)"tony wv"); // cast if a constant
                //cmd.Parameters["?usr"].Direction = ParameterDirection.Input;
                using (var connection = GetMySqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    connection.Open();
                    using (var reader = MySqlHelper.ExecuteReader(connection, mySqlCommand.CommandText))
                    {
                        while (reader.Read())
                        {
                            var mvPlace = new MvPlace
                            {
                                PlaceId = ParseInt(ParseString(reader["PlaceId"])),
                                PlaceName = ParseString(reader["PlaceName"]),
                                PlaceDescription = ParseString(reader["PlaceDescription"]),
                                Address1 = ParseString(reader["Address1"]),
                                Address2 = ParseString(reader["Address2"]),
                                City = ParseString(reader["City"]),
                                State = ParseString(reader["State"]),
                                ZipCode = ParseString(reader["ZipCode"]),
                                Country = ParseString(reader["Country"]),
                                NearByPlaceTypes = ParseString(reader["NearByPlaceTypes"])
                            };
                            places.Add(mvPlace);
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

            return places;

        }
        /// <summary>
        /// Retrieves photos from a service (e.g. Flickr, ..., etc.)
        /// Meant to be generic 
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public IEnumerable<ServicePhoto> GetServicePhotos(string serviceName)
        {
            var servicePhotos = new List<ServicePhoto>();
            try
            {
                var mySqlCommand = new MySqlCommand("mvmasterdb.GetServicePhotosByName");
                mySqlCommand.CommandText = string.Format("CALL mvmasterdb.GetServicePhotosByName('{0}')", serviceName);
                mySqlCommand.CommandType = CommandType.StoredProcedure;
               
                using (var connection = GetMySqlConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    connection.Open();
                    using (var reader = MySqlHelper.ExecuteReader(connection, mySqlCommand.CommandText))
                    {
                        while (reader.Read())
                        {
                            var svcPhoto = new ServicePhoto
                            {
                                ServiceName = ParseString(reader["ServiceName"]),
                                UserId = ParseString(reader["UserId"]),
                                UserName = ParseString(reader["UserName"]),
                                PhotoId = ParseString(reader["PhotoId"]),
                                ProfileUrl = ParseString(reader["Url"]),
                                TagList = ParseString(reader["Tags"])
                            };
                            servicePhotos.Add(svcPhoto);
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

            return servicePhotos;
        }

        //private IEnumerable<T> MultipleMvPlaces<T>(IDataReader reader, Action<IDataReader, T> action = null)
        //   where T : MvPlace, new()
        //{
        //    while (reader.Read())
        //    {
        //        yield return SingleMvPlace(reader, action);
        //    }
        //}


        //private T SingleMvPlace<T>(IDataReader reader, Action<IDataReader, T> action = null)
        //  where T : MvPlace, new()
        //{
        //    var place = new T
        //    {
        //        PlaceId = Convert.ToInt32(Convert.ToString(reader["PlaceId"])),
        //        PlaceName = CoreHelperService.ParseString(reader["PlaceName"]),
        //        PlaceDescription = CoreHelperService.ParseString(reader["PlaceDescription"]),
        //        Address1 = CoreHelperService.ParseString(reader["Address1"]),
        //        Address2 = CoreHelperService.ParseString(reader["Address2"]),
        //        City = CoreHelperService.ParseString(reader["City"]),
        //        State = CoreHelperService.ParseString(reader["CA"]),
        //        ZipCode = CoreHelperService.ParseString(reader["ZipCode"]),
        //        ZipSuffix = CoreHelperService.ParseString(reader["ZipSuffix"])
        //    };

        //    if (action != null)
        //    {
        //        action(reader, place);
        //    }

        //    return place;
        //}


    }
}
