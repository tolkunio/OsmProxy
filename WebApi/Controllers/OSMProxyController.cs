using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nominatim.API.Geocoders;
using Nominatim.API.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OsmProxyController : ControllerBase
    {
        private readonly ILogger<OsmProxyController> _logger;

        public OsmProxyController(ILogger<OsmProxyController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Task<GeocodeResponse[]> Search(string searchString)
        {
            _logger.LogInformation("");
            
            var forwardGeocoder = new ForwardGeocoder();

            return forwardGeocoder.Geocode(new ForwardGeocodeRequest
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