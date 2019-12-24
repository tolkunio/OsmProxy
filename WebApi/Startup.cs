using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApi.Data;
using WebApi.Models;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<OsmProxyDatabaseSettings>(
                Configuration.GetSection(nameof(OsmProxyDatabaseSettings)));
            services.AddSingleton<IOsmProxyDatabaseSettings>(sp =>
            sp.GetRequiredService<IOptions<OsmProxyDatabaseSettings>>().Value);
            ConfigureCustomServices(services);
            services.AddControllers(); }

        public virtual void ConfigureCustomServices(IServiceCollection services)
        {
            services.AddTransient<IOsmProxyContext, OsmProxyContext>();
            services.AddSingleton<ICachedResponseManager, CachedResponseManager>();
            services.AddSingleton<IOsmProxyService, OsmProxyService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}