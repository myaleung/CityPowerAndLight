using Microsoft.Extensions.Configuration;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using DotNetEnv;

namespace CityPowerAndLight.Services
{
    internal class ConnectionService
    {
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Constructor for ConnectionService class.
        /// </summary>
        public ConnectionService()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("Config/appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }

        /// <summary>
        /// Connect to Dataverse using IOrganizationService interface
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IOrganizationService GetOrganizationService()
        {
            DotNetEnv.Env.Load();

            string? secretId = Environment.GetEnvironmentVariable("SECRET_ID");
            string? appId = Environment.GetEnvironmentVariable("APP_ID");
            string? tenantId = Environment.GetEnvironmentVariable("TENANT_ID");
            string? instanceUrl = Configuration["ConnectionStrings:InstanceUrl"];

            if (string.IsNullOrEmpty(secretId) || string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(tenantId) || string.IsNullOrEmpty(instanceUrl))
            {
                throw new Exception("One or more configuration values are not set.");
            }

            Console.WriteLine($"Connected to url: {instanceUrl}");

            // This service connection string uses the info provided above.
            string connectionString = $@"AuthType=ClientSecret;
                        SkipDiscovery=true;url={instanceUrl};
                        Secret={secretId};
                        ClientId={appId};
                        TenantId={tenantId};
                        RequireNewInstance=true";

            return new ServiceClient(connectionString);
        }
    }
}
