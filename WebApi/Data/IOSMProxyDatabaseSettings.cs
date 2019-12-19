using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Data
{
    public interface IOsmProxyDatabaseSettings
    {
         string OsmCollectionName { get; set; }
         string ConnectionString { get; set; }
         string DatabaseName { get; set; }
         int CacheDurationHours { get; set; }
    }
}
