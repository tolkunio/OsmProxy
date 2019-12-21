using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nominatim.API.Models;

namespace WebApi.Services
{
    public interface IOsmProxyService
    {
        Task<GeocodeResponse[]> Search(string searchText);
    }
}
