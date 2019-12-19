using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using Nominatim.API.Models;
using WebApi.Data;

namespace WebApi.Models
{
    public class CachedResponseManager : ICachedResponseManager
    {
        private readonly IMongoCollection<CachedResponse> _cachedResponses;

        public CachedResponseManager(IOsmProxyDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _cachedResponses = database.GetCollection<CachedResponse>(settings.OsmCollectionName);
        }
        public List<CachedResponse> Get(string searchString)
        {
            return _cachedResponses.Find<CachedResponse>(cache => cache.SearchText == searchString).ToList();
        }

        public void CacheResponse(GeocodeResponse geocodeResponse, string searchText)
        {
            var cacheResponse = new CachedResponse
            {
                Id = ObjectId.GenerateNewId(),
                SearchText = searchText,
                CreatedAt = DateTime.Now,
                Content = geocodeResponse
            };
            _cachedResponses.InsertOne(cacheResponse);
        }

        public void ClearCache()
        {
            _cachedResponses.DeleteMany(cache => true);
        }
    }
}