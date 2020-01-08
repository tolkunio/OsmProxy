using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Nominatim.API.Geocoders;
using WebApi.Data;
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

            services.AddSingleton(it=>new ForwardGeocoder());

            services.AddSingleton<ICachedResponseStore, CachedResponseStore>();
            services.AddSingleton<IOsmProxyService, OsmProxyService>();

            services.AddControllers();
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