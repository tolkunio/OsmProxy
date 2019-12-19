using Nominatim.API.Models;

namespace WebApi.Models
{
    public interface ICachedResponseManager
    {
        CachedResponse Get(string searchString);
        void CacheResponse(GeocodeResponse geocodeResponse);
        void ClearCache();
    }
}