using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Demo.Games.Domain.Models
{
    public class Game
   {
       [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Released { get; set; }
        public int Rating { get; set; }
    }
}
