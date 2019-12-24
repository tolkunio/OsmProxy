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
        private readonly IOsmProxyContext _cacheContext; 

        public CachedResponseManager(IOsmProxyContext cacheContext)
        {
            this._cacheContext = cacheContext;
        }
        public CachedResponse Get(string searchString)
        {
            return _cacheContext.CachedResponses.Find(cache => cache.SearchText == searchString).FirstOrDefault();
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
            _cacheContext.CachedResponses.InsertOne(cacheResponse);
        }
        public bool IsActualCache(CachedResponse cache)
        {
           return (DateTime.Now - cache.CreatedAt).TotalHours < _cacheContext.CacheDurationHours;
        }

        public void UpdateCachedResponse(ObjectId id, CachedResponse cacheIn)
        {
            _cacheContext.CachedResponses.ReplaceOne(cache => cache.Id == id, cacheIn);
        }
    }
}