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
        public Task<GeocodeResponse[]> Search(string searchString)
        {
            var gList = new List<GeocodeResponse>();
            _logger.LogInformation("");

            if (string.IsNullOrEmpty(searchString))
                return null;

            var cacheList = _cacheManager.Get(searchString);
            if (cacheList.Count>0)
            {
                foreach (var cache in cacheList)
                {
                    gList.Add(cache.Content);
                }

                return new Task<GeocodeResponse[]>(gList.ToArray);
            }

            var forwardGeocoder = new ForwardGeocoder();

            var geocodeList = forwardGeocoder.Geocode(new ForwardGeocodeRequest
            {
                queryString = searchString,
                BreakdownAddressElements = true,
                ShowExtraTags = true,
                ShowAlternativeNames = true,
                ShowGeoJSON = true
            });
            return geocodeList;
        }
    }
}