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
        private IOsmProxyDatabaseSettings settings;

        public CachedResponseManager(IOsmProxyDatabaseSettings settings)
        {
            this.settings = settings;

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _cachedResponses = database.GetCollection<CachedResponse>(settings.OsmCollectionName);
        }
        public CachedResponse Get(string searchString)
        {
            return _cachedResponses.Find<CachedResponse>(cache => cache.SearchText == searchString).FirstOrDefault();
        }

        public void CacheResponse(GeocodeResponse[] geocodeResponse, string searchText)
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
           var dateDiff= DateTime.Now.Subtract(TimeSpan.FromHours(settings.CacheDurationHours));
            _cachedResponses.DeleteMany(cache=>cache.CreatedAt<dateDiff);
        }
    }
}