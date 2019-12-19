using System;
using MongoDB.Bson;
using Nominatim.API.Models;

namespace WebApi.Models
{
    public class CachedResponse
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string SearchText { get; set; }

        public GeocodeResponse Content { get; set; }
    }
}