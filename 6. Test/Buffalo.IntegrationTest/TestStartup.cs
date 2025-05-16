using Buffalo;
using Microsoft.Extensions.Configuration;

namespace ALCare.IntegrationTest
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
