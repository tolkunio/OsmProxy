using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApi.Models;

namespace WebApi.Data
{
    public class OsmProxyContext : IOsmProxyContext
    {
        private readonly IMongoDatabase database;

        public OsmProxyContext(IOptions<OsmProxyDatabaseSettings> settings)
        {
            var client =new MongoClient(settings.Value.ConnectionString);
            database = client.GetDatabase(settings.Value.DatabaseName);
            CacheDurationHours = settings.Value.CacheDurationHours;
        }

        public IMongoCollection<CachedResponse> CachedResponses =>
            database.GetCollection<CachedResponse>("CachedResponse");

        public int CacheDurationHours { get; }
    }
}
