using Nominatim.API.Models;

namespace WebApi.Models
{
    public class CachedResponseManager : ICachedResponseManager
    {
        
        public CachedResponse Get(string searchString)
        {
            throw new System.NotImplementedException();
        }

        public void CacheResponse(GeocodeResponse geocodeResponse)
        {
            throw new System.NotImplementedException();
        }

        public void ClearCache()
        {
            throw new System.NotImplementedException();
        }
    }
}