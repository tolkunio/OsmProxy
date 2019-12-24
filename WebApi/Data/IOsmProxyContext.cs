using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using WebApi.Models;

namespace WebApi.Data
{
    public interface IOsmProxyContext
    {
        IMongoCollection<CachedResponse> CachedResponses { get; }
        int CacheDurationHours { get; }
    }
}
