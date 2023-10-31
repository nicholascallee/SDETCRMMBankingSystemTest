using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Testing;
using RadancyTest;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace BankingSystemTest
{
    public class ApplicationRunner
    {

        private readonly WebApplicationFactory<Program> _factory;  // Assuming your startup class is named "Startup"

        public HttpClient client { get; private set; }
        public IConfiguration Configuration { get; private set; }


        public ApplicationRunner()
        {

            _factory = new WebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            

        }

    }

}
