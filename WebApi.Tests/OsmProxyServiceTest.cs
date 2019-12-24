using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using Moq;
using Newtonsoft.Json;
using Nominatim.API.Geocoders;
using Nominatim.API.Models;
using WebApi.Models;
using WebApi.Services;
using Xunit;

namespace WebApi.Integration.Tests
{
    public class OsmProxyServiceTest
    {
        public TestServer Server { get; set; }
        public HttpClient Client { get; set; }

        public Mock<ICachedResponseStore> CachedResponseStoreMock;

        public OsmProxyServiceTest()
        {
            CachedResponseStoreMock = new Mock<ICachedResponseStore>();

            Server = new TestServer(new WebHostBuilder()
                .ConfigureTestServices(services =>
                {
                    services.AddScoped<ICachedResponseStore>(it => CachedResponseStoreMock.Object);
                    services.AddSingleton(it => new ForwardGeocoder());
                })
                .UseStartup<Startup>());

            Client = Server.CreateClient();
        }

        [Fact]
        public async Task Search_ReturnsBadRequest_IfSearchTextIsEmpty()
        {
            var response = await Client.GetAsync("/OsmProxy/Search?SearchString=");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode); ;
        }

        [Fact]
        public async Task Search_ReturnsResult_IfSearchTextIsSet()
        {

            CachedResponseStoreMock.Setup(repository =>
                    repository.Get("Sample"))
                .Returns(new CachedResponse
                {
                    Content = new GeocodeResponse[]
                    {
                        new GeocodeResponse
                        {
                            DisplayName = "SampleDisplayName"
                        }
                    },
                    CreatedAt = DateTime.UnixEpoch,
                    Id = new ObjectId("5dfbaa73bcd96515dc0284df"),
                    SearchText = "Sample"
                });

            CachedResponseStoreMock
                .Setup(repository => repository.IsActualCache(It.IsAny<CachedResponse>()))
                .Returns(true);

            var response = await Client.GetAsync("/OsmProxy/Search?SearchString=Sample");

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeAnonymousType(json,new []
            {
                new
                {
                    displayName = string.Empty
                }
            });

            Assert.Equal("SampleDisplayName",result.First().displayName);
        }
    }
}
