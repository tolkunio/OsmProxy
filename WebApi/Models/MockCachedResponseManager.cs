using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using Nominatim.API.Models;

namespace WebApi.Models
{
    public class MockCachedResponseManager : ICachedResponseManager
    {
        readonly List<CachedResponse> mockCachedResponses;
        public MockCachedResponseManager()
        {
            mockCachedResponses = InitializeMockCachedResponse();
        }
        public CachedResponse Get(string searchString)
        {
            return mockCachedResponses.Find(x => x.SearchText == searchString);
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
            mockCachedResponses.Add(cacheResponse);

        }

        public bool IsActualCache(CachedResponse cache)
        {
            return (DateTime.Now - cache.CreatedAt).TotalHours < 1;
        }

        public void UpdateCachedResponse(ObjectId id, CachedResponse cacheIn)
        {
            throw new NotImplementedException();
        }

        public List<CachedResponse> InitializeMockCachedResponse()
        {
            mockCachedResponses.Add(new CachedResponse
            {
                Id = new ObjectId("5e01a3b5ce7e59107cd17953"),
                CreatedAt = new DateTime(2019, 12, 24, 12, 00, 00),
                SearchText = "karakol",
                Content = SeedData.InitializeMockGeocode().ToArray()
            });
            return mockCachedResponses;
        }
    }
}
