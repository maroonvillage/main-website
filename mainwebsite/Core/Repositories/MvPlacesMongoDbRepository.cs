using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaroonVillage.Core.Interfaces.Repositories;
using Newtonsoft.Json.Linq;
using MongoDB.Driver;
using MaroonVillage.Core.DomainModel;

namespace MaroonVillage.Core.Repositories
{
    public class MvPlacesMongoDbRepository : IMvPlacesMongoDbRepository
    {
        private MongoServer _server;
        private MongoDatabase _db;
        private string _dbName = "test";
        private string _connectionString = "mongodb://127.0.0.1";

        public MvPlacesMongoDbRepository()
        {
            _server = MongoServer.Create(_connectionString);
            _db = _server.GetDatabase(_dbName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<SimplePlace> GetAllPlaces()
        {
            var places = new List<SimplePlace>();

            var collection = _db.GetCollection<SimplePlace>("maroon_place");

            var cursor = collection.FindAll();

            var result = from r in cursor
                         select r;

            foreach (var p in result)
            {
                places.Add(new SimplePlace
                {
                    Name = p.Name,
                    Address1 = p.Address1,
                    City = p.City,
                    State = p.State,
                    ZipCode = p.ZipCode,
                    County = p.County,
                    Country = p.Country
                });
            }

            return places;
        }


        public List<SimplePlace> GetPlaceByName(string name)
        {
            return new List<SimplePlace>();
        }

        public List<SimplePlace> GetPlaceByCityName(string cityName)
        {
            return new List<SimplePlace>();

        }

    }
}
