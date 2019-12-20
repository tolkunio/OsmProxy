using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Nominatim.API.Geocoders;
using Nominatim.API.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OSMProxyController : ControllerBase
    {
        private readonly ILogger<OSMProxyController> _logger;
        private readonly ICachedResponseManager _cacheManager;

        public OSMProxyController(ILogger<OSMProxyController> logger, ICachedResponseManager cacheManager)
        {
            _logger = logger;
            _cacheManager = cacheManager;
        }

        [HttpGet]
        public async Task<GeocodeResponse[]> Search(string searchString)
        {
            string searchRequest = searchString.ToLower();
            _logger.LogInformation("");

            if (string.IsNullOrEmpty(searchString))
                return null;

            var cacheResponse = _cacheManager.Get(searchRequest);
           
            if (cacheResponse != null)
            {
                if (_cacheManager.IsActualCache(cacheResponse))
                {
                    return await Task.FromResult(cacheResponse.Content);
                }
                var actualCache = new CachedResponse
                {
                    Id = cacheResponse.Id,
                    SearchText = cacheResponse.SearchText,
                    CreatedAt = DateTime.Now,
                    Content = await GetGeocodeResponse(searchRequest)
                };
                _cacheManager.UpdateCachedResponse(cacheResponse.Id, actualCache);
                return actualCache.Content;
            }

            var geocodeArray = await GetGeocodeResponse(searchString);
            _cacheManager.CacheResponse(geocodeArray, searchString);

            return geocodeArray;
        }

        public async Task<GeocodeResponse[]> GetGeocodeResponse(string searchString)
        {
            return await new ForwardGeocoder().Geocode(new ForwardGeocodeRequest
            {
                queryString = searchString,
                BreakdownAddressElements = true,
                ShowExtraTags = true,
                ShowAlternativeNames = true,
                ShowGeoJSON = true
            });
        }
    }
}