using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nominatim.API.Geocoders;
using Nominatim.API.Models;
using WebApi.Models;

namespace WebApi.Services
{
    public class OsmProxyService : IOsmProxyService
    {
        private readonly ICachedResponseManager _cacheManager;

        public OsmProxyService(ICachedResponseManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task<GeocodeResponse[]> Search(string searchText)
        {
            var normalizedSearchText = searchText.ToLower();

            var cacheResponse = _cacheManager.Get(searchText);

            if (cacheResponse == null)
            {
                var response = await RemoteSearch(normalizedSearchText);
                _cacheManager.CacheResponse(response, normalizedSearchText);
                return response;
            }

            if (_cacheManager.IsActualCache(cacheResponse))
            {
                return await Task.FromResult(cacheResponse.Content);
            }

            var geocodeResponses = await RemoteSearch(normalizedSearchText);

            var actualCache = new CachedResponse
            {
                Id = cacheResponse.Id,
                SearchText = cacheResponse.SearchText,
                CreatedAt = DateTime.Now,
                Content = geocodeResponses
            };

            _cacheManager.UpdateCachedResponse(cacheResponse.Id, actualCache);
            return geocodeResponses;
        }

        private static async Task<GeocodeResponse[]> RemoteSearch(string normalizedSearchText)
        {
            var forwardGeocoder = new ForwardGeocoder();

            return await forwardGeocoder.Geocode(new ForwardGeocodeRequest
            {
                queryString = normalizedSearchText,
                BreakdownAddressElements = true,
                ShowExtraTags = true,
                ShowAlternativeNames = true,
                ShowGeoJSON = true
            });
        }
    }
}