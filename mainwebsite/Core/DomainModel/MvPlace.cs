﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Linq;

namespace MaroonVillage.Core.DomainModel
{
    public class MvPlace
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string Name { get; set; }
        public string PlaceDescription { get; set; }
        public int AddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ZipSuffix { get; set; }
        public string Lattitude { get; set; }
        public string Longitude { get; set; }
        public string NearByPlaceTypes { get; set; }
        public string Country { get; set; }
        public string County { get; set; }


        public JToken GeoCodingObject { get; set; }

        public MvPlace()
        {

        }
    }
}
