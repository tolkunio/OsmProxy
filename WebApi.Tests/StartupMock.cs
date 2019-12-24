using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Models;

namespace WebApi.Integration.Tests
{
    public class StartupMock: Startup
    {
        private ICachedResponseManager _cachedManager;

        public StartupMock(IConfiguration configuration,ICachedResponseManager cachedManager) : base(configuration)
        {
            this._cachedManager = cachedManager;
        }

        public override void ConfigureCustomServices(IServiceCollection services)
        {
            services.AddSingleton<ICachedResponseManager, MockCachedResponseManager>();
        }
    }
}
