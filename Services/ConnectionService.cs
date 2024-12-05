using Microsoft.Extensions.Configuration;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;

namespace CityPowerAndLight.Services
{
    internal class ConnectionService
    {
        public IConfiguration Configuration { get; }
        public ConnectionService()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public IOrganizationService GetOrganizationService()
        {
            // Read secrets from environment variables/appsettings.json
            string? secretId = Configuration["Dataverse:SECRET_ID"];
            string? appId = Configuration["Dataverse:APP_ID"];
            string? tenantId = Configuration["Dataverse:TENANT_ID"];
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
