using Buffalo;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.IO;
using System.Net.Http;

namespace ALCare.IntegrationTest
{
    public class TestFixture : IDisposable
    {
        protected readonly TestServer _server;
        protected readonly HttpClient _client;
        public TestFixture()
        {
            var integrationTestsPath = PlatformServices.Default.Application.ApplicationBasePath; 
            var applicationPath = Path.GetFullPath(Path.Combine(integrationTestsPath, "../../../../../1. Web/React"));

            _server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseContentRoot(applicationPath)
                .UseEnvironment("Development"));
            _client = _server.CreateClient();
        }

        public void Dispose()
        {
            _client.Dispose();
            _server.Dispose();
        }
    }
}
