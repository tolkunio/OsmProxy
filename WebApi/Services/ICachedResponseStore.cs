using MongoDB.Bson;
using Nominatim.API.Models;
using WebApi.Models;

namespace WebApi.Services
{
    public interface ICachedResponseStore
    {
        CachedResponse Get(string searchString);
        void CacheResponse(GeocodeResponse[] geocodeResponse,string searchText);
        bool IsActualCache(CachedResponse cache);
        void UpdateCachedResponse(ObjectId id, CachedResponse cacheIn);
    }
}