using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuDemoApp.Models
{
    public class Movie
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("title")]
        public string title { get; set; }
        [BsonElement("director")]
        public string director { get; set; }
        [BsonElement("actors")]
        public string actors { get; set; }
        [BsonElement("image")]
        public string image { get; set; }
        [BsonElement("year")]
        public int year { get; set; }
    }
}