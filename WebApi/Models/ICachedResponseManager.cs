using System.Collections.Generic;
using MongoDB.Bson;
using Nominatim.API.Models;

namespace WebApi.Models
{
    public interface ICachedResponseManager
    {
        CachedResponse Get(string searchString);
        void CacheResponse(GeocodeResponse[] geocodeResponse,string searchText);
        bool IsActualCache(CachedResponse cache);
        void UpdateCachedResponse(ObjectId id, CachedResponse cacheIn);
    }
}