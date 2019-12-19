using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApi.Models;

namespace WebApi.BackgroundTasks
{
    public class ClearCacheStarter :BackgroundService
    {
        private readonly ILogger<ClearCacheStarter> _logger;
        private readonly ICachedResponseManager _cacheManager;

        public ClearCacheStarter(ILogger<ClearCacheStarter> logger, ICachedResponseManager cacheManager)
        {
            _logger = logger;
            _cacheManager = cacheManager;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                _logger.LogInformation("Clear cache started..");
                _cacheManager.ClearCache();
            });
        }
    }
}
