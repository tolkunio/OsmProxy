using System;
using MongoDB.Bson;
using MongoDB.Driver;
using Nominatim.API.Models;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Services
{
    public class CachedResponseStore : ICachedResponseStore
    {
        private readonly IMongoCollection<CachedResponse> _cachedResponses;
        private IOsmProxyDatabaseSettings settings;

        public CachedResponseStore(IOsmProxyDatabaseSettings settings)
        {
            this.settings = settings;

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _cachedResponses = database.GetCollection<CachedResponse>(settings.OsmCollectionName);
        }

        public CachedResponse Get(string searchString)
        {
            return _cachedResponses.Find(cache => cache.SearchText == searchString).FirstOrDefault();
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
        public bool IsActualCache(CachedResponse cache)
        {
           return (DateTime.Now - cache.CreatedAt).TotalHours <settings.CacheDurationHours;
        }

        public void UpdateCachedResponse(ObjectId id, CachedResponse cacheIn)
        {
            _cachedResponses.ReplaceOne(cache => cache.Id == id, cacheIn);
        }
    }
}