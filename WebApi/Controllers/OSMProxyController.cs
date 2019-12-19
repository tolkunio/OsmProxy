using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            _logger.LogInformation("");

            if (string.IsNullOrEmpty(searchString))
                return null;

            var cacheResponse = _cacheManager.Get(searchString);
            if (cacheResponse != null)
            {
                return await Task.FromResult(cacheResponse.Content);
            }

            var forwardGeocoder = new ForwardGeocoder();

            var geocodeList = await forwardGeocoder.Geocode(new ForwardGeocodeRequest
            {
                queryString = searchString,
                BreakdownAddressElements = true,
                ShowExtraTags = true,
                ShowAlternativeNames = true,
                ShowGeoJSON = true
            });
            _cacheManager.CacheResponse(geocodeList, searchString);

            return geocodeList;
        }
    }
}