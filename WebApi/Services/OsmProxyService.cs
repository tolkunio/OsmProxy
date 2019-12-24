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
        private readonly ICachedResponseStore _cacheStore;
        private readonly ForwardGeocoder _geocoder;

        public OsmProxyService(ICachedResponseStore cacheStore, ForwardGeocoder geocoder)
        {
            _cacheStore = cacheStore;
            _geocoder = geocoder;
        }

        public async Task<GeocodeResponse[]> Search(string searchText)
        {
            var normalizedSearchText = searchText.ToLower();

            var cacheResponse = _cacheStore.Get(searchText);

            if (cacheResponse == null)
            {
                var response = await RemoteSearch(normalizedSearchText);
                _cacheStore.CacheResponse(response, normalizedSearchText);
                return response;
            }

            if (_cacheStore.IsActualCache(cacheResponse))
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

            _cacheStore.UpdateCachedResponse(cacheResponse.Id, actualCache);
            return geocodeResponses;
        }

        private async Task<GeocodeResponse[]> RemoteSearch(string normalizedSearchText)
        {
            return await _geocoder.Geocode(new ForwardGeocodeRequest
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