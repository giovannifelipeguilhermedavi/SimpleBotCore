using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SimpleBotCore.Models
{
    [BsonIgnoreExtraElements]
    public class Question
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        [BsonElement("content")]
        public string Content { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("creation")]
        public DateTime Creation { get; set; } = DateTime.Now;

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("update")]
        public DateTime Update { get; set; } = DateTime.Now;
    }
}