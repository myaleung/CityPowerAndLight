using CityPowerAndLight.Model;
using CityPowerAndLight.Services;
using CityPowerAndLight.View;

namespace CityPowerAndLight.App
{
    internal class CustomerServiceDemoApp
    {
        private readonly AccountService accountService;
        private readonly ContactService contactService;
        private readonly IncidentService incidentService;

        public CustomerServiceDemoApp(AccountService accountService, ContactService contactService, IncidentService incidentService)
        {
            this.accountService = accountService;
            this.contactService = contactService;
            this.incidentService = incidentService;
        }

        /// <summary>
        /// Demo program to demonstrate the Customer Service API
        /// </summary>
        public void Run()
        {
            //Application
            Console.WriteLine();
            Console.WriteLine("//---Application START---//");
            Console.WriteLine("Welcome to the Customer Service API Demo");
            Console.WriteLine("=========================================");
            Console.WriteLine();
            //Contacts
            Console.WriteLine("- - - CONTACTS TABLE - - -");
            var allContacts = contactService.GetAllContacts();
            Console.WriteLine($"Retrieving list of {allContacts.Count} contacts:");
            ConsoleInterface.PrintLoop(allContacts);
            Console.WriteLine();

            Console.WriteLine("--- Getting a specific contact by ID ---");
            Console.WriteLine("Looking in contacts table for ID 'fa33457b-96b1-ef11-b8e8-6045bdcf868c'...");
            var contactReturned = contactService.GetContact(Guid.Parse("fa33457b-96b1-ef11-b8e8-6045bdcf868c"));
            Console.WriteLine("Contact found...");
            ConsoleInterface.PrintContact(contactReturned);
            Console.WriteLine();

            Console.WriteLine("--- Creating a contact ---");
            var newId = contactService.CreateContact("Mama", "Duckie", "mamaduckie@outlook.com", "855-555-8888");
            var createdContact = contactService.GetContact(newId);
            Console.WriteLine("New contact created...");
            ConsoleInterface.PrintContact(createdContact, true);
            Console.WriteLine();

            Console.WriteLine("--- Updating a contact ---");
            Contact updatedContact = contactService.UpdateContact(newId);
            Console.WriteLine($"Email of contact updated...");
            ConsoleInterface.PrintContact(updatedContact, false);
            Console.WriteLine();

            Console.WriteLine("--- Deleting a contact ---");
            contactService.DeleteContact(newId);
            Console.WriteLine();

            //Accounts
            Console.WriteLine("- - - ACCOUNTS TABLE - - -");
            var allAccounts = accountService.GetAllAccounts();
            Console.WriteLine($"Retrieving list of {allAccounts.Count} accounts:");
            ConsoleInterface.PrintLoop(allAccounts);
            Console.WriteLine();

            Console.WriteLine("--- Creating a new account ---");
            Guid newAccountId = accountService.CreateAccount("Duckie Towers LLC", "amy@duckietowers.co.uk");
            var newAccount = accountService.GetAccount(newAccountId);
            //Console.WriteLine($"Account ID {newAccount.AccountId} with name {newAccount.Name} created on {newAccount.CreatedOn}.");
            ConsoleInterface.PrintAccount(newAccount, true);
            Console.WriteLine();

            Console.WriteLine("--- Updating an account name ---");
            Console.WriteLine($"Updating account name for account ID: {newAccountId} to 'Duckie Towers Corp'.");
            var updatedAccount = accountService.UpdateAccount(newAccountId, "Duckie Towers Corp");
            //Console.WriteLine($"Account name updated to: {updatedAccount.Name}.");
            ConsoleInterface.PrintAccount(updatedAccount, false);
            Console.WriteLine();

            Console.WriteLine("--- Deleting an account ---");
            accountService.DeleteAccount(newAccountId);
            Console.WriteLine();

            //Incidents
            Console.WriteLine("- - - INCIDENTS TABLE - - -");
            List<Incident> allIncidents = incidentService.GetAllIncidents();
            Console.WriteLine($"Retrieving list of {allIncidents.Count} incidents:");
            ConsoleInterface.PrintLoop(allIncidents);
            Console.WriteLine();

            Console.WriteLine("--- Creating a new incident ---");
            Guid newIncidentAccountId = accountService.CreateAccount("Bunny Meadows", "amy@bunnymeadows.com");
            Guid newIncidentCustomerId = contactService.CreateContact("Test", "Bunny", "test@email.com", "09899899899");
            Guid newIncidentId = incidentService.CreateIncident("Product Damage", "Crack on screen of monitor", newIncidentAccountId, newIncidentCustomerId);
            Console.WriteLine("New incident created: ");
            var getNewIncident = incidentService.GetIncident(newIncidentId);
            ConsoleInterface.PrintIncident(getNewIncident);
            Console.WriteLine();

            Console.WriteLine("--- Updating an incident ---");
            incidentService.UpdateIncident(newIncidentId, "description", "Monitor won't turn on");
            var getUpdatedIncident = incidentService.GetIncident(newIncidentId);
            ConsoleInterface.PrintIncident(getUpdatedIncident);
            Console.WriteLine();

            Console.WriteLine("--- Delete incident ---");
            Console.WriteLine("Deleting newly created incident and associated new contact and account");
            incidentService.DeleteIncident(newIncidentId);
            //Clean up of newly created account and contact
            accountService.DeleteAccount(newIncidentAccountId);
            contactService.DeleteContact(newIncidentCustomerId);

            Console.WriteLine();
            Console.WriteLine("//--- Application END ---//");

            // Pause the console so it does not close.
            Console.WriteLine("Press the <Enter> key to exit.");
            Console.ReadLine();
        }
    }
}
