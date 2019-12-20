using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Nominatim.API.Models;

namespace WebApi.Models
{
    public class CachedResponse
    {
        public ObjectId Id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedAt { get; set; }

        public string SearchText { get; set; }

        public GeocodeResponse[] Content { get; set; }
    }
}