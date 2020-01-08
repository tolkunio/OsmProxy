using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nominatim.API.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OSMProxyController : ControllerBase
    {
        private readonly ILogger<OSMProxyController> _logger;
        private readonly IOsmProxyService _proxyService;

        public OSMProxyController(ILogger<OSMProxyController> logger, IOsmProxyService proxyService)
        {
            _logger = logger;
            _proxyService = proxyService;
        }

        [HttpGet]
        public async Task<GeocodeResponse[]> Search([Required] string searchString)
        {
            return await _proxyService.Search(searchString);
        }
    }
}