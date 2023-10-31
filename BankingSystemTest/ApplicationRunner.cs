using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace BankingSystemTest
{
    public class ApplicationRunner
    {
        public IConfiguration Configuration { get; private set; }


        public ApplicationRunner(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.Test.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public IServiceCollection ConfigureServices(IServiceCollection services)
        {


            // Connection string retrieval
            var connectionString = Configuration.GetConnectionString("DefaultConnection");


            // If you encounter issues with logging, you can register the default logging services.
            services.AddLogging();

            // Register DBInitializer

            return services;


        }
    }

}
