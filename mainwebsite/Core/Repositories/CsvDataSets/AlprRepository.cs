using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MaroonVillage.Core.DomainModel;
using MaroonVillage.Core.Interfaces.Repositories.CsvDataSets;
using MySql.Data.MySqlClient;

namespace MaroonVillage.Core.Repositories.CsvDataSets
{
    /// <summary>
    /// This class that represents a layer which retreives ALPR data in various formats and endpoints
    /// (e.g. RESTFUL API, Relational Database, NoSQL database, xml, csv, JSON)
    /// </summary>
    public class AlprRepository : BaseRepository, IAlprRepository
    {

        public AlprRepository()
        {

        }

        public IEnumerable<AlprCapture> GetAlprDataByCity(string cityName)
        {
            using (var mySqlCommand = new MySqlCommand("mvmasterdb.GetOaklandPDALPR_Data"))
            using (var connection = GetMySqlConnection())
            {
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                connection.ConnectionString = ConnectionString;
                connection.Open();
                using (var reader = MySqlHelper.ExecuteReader(connection, mySqlCommand.CommandText))
                {
                    return MultipleAlprCaptures<AlprCapture>(reader).ToList();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private IEnumerable<T> MultipleAlprCaptures<T>(IDataReader reader, Action<IDataReader, T> action = null)
           where T : AlprCapture, new()
        {
            while (reader.Read())
            {
                yield return SingleAlprCapture(reader, action);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private T SingleAlprCapture<T>(IDataReader reader, Action<IDataReader, T> action = null)
          where T : AlprCapture, new()
        {
            var capture = new T
                             {
                                 CaptureDate = ParseDate(ParseString(reader["CaptureDate"])),
                                 CaptureTime = ParseDate(ParseString(reader["CaptureTime"])),
                                 Lattitude = ParseFloat(ParseString(reader["Lattitude"])),
                                 Longitude = ParseFloat(ParseString(reader["Longitude"])),
                             };

            if (action != null)
            {
                action(reader, capture);
            }

            return capture;
        }
        //public IEnumerable<AlprCapture> GetAlprDataByCity(string cityName)
        //{
        //    var aplrCaptures = new List<AlprCapture>();
        //    try
        //    {
        //        var mySqlCommand = new MySqlCommand("mvmasterdb.GetOaklandPDALPR_Data");
        //        mySqlCommand.CommandType = CommandType.StoredProcedure;

        //        using (var connection = GetMySqlConnection())
        //        {
        //            connection.ConnectionString = ConnectionString;
        //            connection.Open();
        //            using (var reader = MySqlHelper.ExecuteReader(connection, mySqlCommand.CommandText))
        //            {
        //                while (reader.Read())
        //                {
        //                    var capture = new AlprCapture
        //                    {
        //                        CaptureDate = ParseDate(ParseString(reader["CaptureDate"])),
        //                        CaptureTime = ParseDate(ParseString(reader["CaptureTime"])),
        //                        Lattitude = ParseFloat(ParseString(reader["Lattitude"])),
        //                        Longitude = ParseFloat(ParseString(reader["Longitude"])),
        //                    };
        //                    aplrCaptures.Add(capture);
        //                }
        //            }

        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //    return aplrCaptures;
        //}


    }
}
