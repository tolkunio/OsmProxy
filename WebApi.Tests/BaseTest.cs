using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WebApi.Models;

namespace WebApi.Integration.Tests
{
    public abstract class BaseTest
    {
        protected MockCachedResponseManager MockCachedResponseService;
        protected Mock<ICachedResponseManager> CachedResponseMock;
        public BaseTest()
        {
            CachedResponseMock = new Mock<ICachedResponseManager>();
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<StartupMock>()
                .ConfigureServices(services => { services.AddSingleton(CachedResponseMock.Object); }));
            var httpClient = server.CreateClient();
            MockCachedResponseService = new MockCachedResponseManager();
        }

        
        public void BaseTearDown()
        {
            CachedResponseMock.Reset();
        }
    }
}
