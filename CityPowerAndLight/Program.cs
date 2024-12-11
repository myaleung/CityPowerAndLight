using CityPowerAndLight.App;
using CityPowerAndLight.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Extensions.Configuration;

namespace CityPowerAndLight
{
    /// <summary>
    /// Demonstrates execution of a Dataverse API connection and CRUD functions.
    /// </summary>
    class Program
    {
        static AccountService? accountService;
        static ContactService? contactService;
        static IncidentService? incidentService;

        static IConfiguration? Configuration { get; set; }

        static void Main(string[] args)
        {
            try 
            {
                /// <summary>
                /// Connect to Dataverse using IOrganizationService interface
                /// </summary>
                var dataverseConnection = new ConnectionService();
                IOrganizationService service = dataverseConnection.GetOrganizationService();

                //Create instances of the services
                accountService = new AccountService(service, "account");
                contactService = new ContactService(service, "contact");
                incidentService = new IncidentService(service, "incident");

                var app = new CustomerServiceDemoApp(accountService, contactService, incidentService);

                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}