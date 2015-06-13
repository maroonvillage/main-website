using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;


namespace TestHarnessConsole
{
    public class Place
    {
        [BsonId]
        public ObjectId _id { get; set; }

        public string Name { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string State { get; set; }

        public int ZipCode { get; set; }

        public int ZipPlus4 { get; set; }

        public string Country { get; set; }

        public double Lattitude { get; set; }

        public double Longitude { get; set; }


    }
}
