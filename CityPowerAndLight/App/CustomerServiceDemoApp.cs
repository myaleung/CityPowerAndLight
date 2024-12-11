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
            Console.WriteLine("=========================================");
            Console.WriteLine("Welcome to the Customer Service API Demo");
            Console.WriteLine("=========================================");
            Console.WriteLine();

            //Accounts CRUD Operations
            Console.WriteLine("- - - ACCOUNTS TABLE - - -");
            var allAccounts = accountService.GetAllAccounts();
            Console.WriteLine($"Retrieving list of {allAccounts.Count} accounts:");
            foreach (var account in allAccounts)
            {
                ConsoleInterface.PrintAccount(account);
                Console.WriteLine("-----");
            }
            Console.WriteLine();

            Console.WriteLine("--- Creating a new account ---");
            Guid newAccountId = accountService.CreateAccount("Duckie Towers LLC", "amy@duckietowers.co.uk", "123-456-7890", "New York", "123 Main Street", "10001", "USA");
            var newAccount = accountService.GetAccount(newAccountId);
            ConsoleInterface.PrintAccount(newAccount, true);
            Console.WriteLine();

            Console.WriteLine("--- Updating an account name ---");
            Console.WriteLine($"Updating account name for account ID: {newAccountId} to 'Duckie Towers Corp'.");
            var updatedAccount = accountService.UpdateAccount(newAccountId, "name", "Duckie Towers Corp");
            ConsoleInterface.PrintAccount(updatedAccount, false);
            Console.WriteLine();

            //Contacts CRUD Operations
            Console.WriteLine("- - - CONTACTS TABLE - - -");
            var allContacts = contactService.GetAllContacts();
            Console.WriteLine($"Retrieving list of {allContacts.Count} contacts:");
            foreach (var contact in allContacts)
            {
                ConsoleInterface.PrintContact(contact);
                Console.WriteLine("-----");
            }
            Console.WriteLine();

            Console.WriteLine("--- Getting a specific contact by ID ---");
            Console.WriteLine("Looking in contacts table for ID 'fa33457b-96b1-ef11-b8e8-6045bdcf868c'...");
            var contactReturned = contactService.GetContact(Guid.Parse("fa33457b-96b1-ef11-b8e8-6045bdcf868c"));
            Console.WriteLine("Contact found...");
            ConsoleInterface.PrintContact(contactReturned);
            Console.WriteLine();

            Console.WriteLine("--- Creating a contact ---");
            var newContactId = contactService.CreateContact("Mama", "Duckie", "mamaduckie@outlook.com", "855-555-8888", newAccountId);           
            var createdContact = contactService.GetContact(newContactId);
            Console.WriteLine("New contact created...");
            ConsoleInterface.PrintContact(createdContact, true);
            Console.WriteLine();

            Console.WriteLine("--- Updating a contact ---");
            Contact updatedContact = contactService.UpdateContact(newContactId);
            Console.WriteLine($"Email of contact updated...");
            ConsoleInterface.PrintContact(updatedContact, false);
            Console.WriteLine();

            // update account with contact as primary contact
            Console.WriteLine("--- Updating an account ---");
            Console.WriteLine($"Updating account with contact as primary contact:");
            var updatedAccountContact = accountService.UpdateAccount(newAccountId, newContactId);
            ConsoleInterface.PrintAccount(updatedAccountContact, false);
            ConsoleInterface.PrintContact(contactService.GetContact(newContactId));
            Console.WriteLine();

            //Incidents
            Console.WriteLine("- - - INCIDENTS TABLE - - -");
            List<Incident> allIncidents = incidentService.GetAllIncidents();
            Console.WriteLine($"Retrieving list of {allIncidents.Count} incidents:");
            foreach (var incident in allIncidents)
            {
                ConsoleInterface.PrintIncident(incident);
                Console.WriteLine("-----");
            }
            Console.WriteLine();

            Console.WriteLine("--- Creating a new incident ---");
            // update and set the new contact as the primary contact for the account
            Guid newIncidentId = incidentService.CreateIncident("Product Damage", "Crack on screen of monitor", newAccountId, newContactId);
            Console.WriteLine("New incident created: ");
            var getNewIncident = incidentService.GetIncident(newIncidentId);
            ConsoleInterface.PrintIncident(getNewIncident);
            Console.WriteLine();

            Console.WriteLine("--- Updating an incident ---");
            incidentService.UpdateIncident(newIncidentId, "priority", "high");
            var getUpdatedIncident = incidentService.GetIncident(newIncidentId);
            ConsoleInterface.PrintIncident(getUpdatedIncident);
            Console.WriteLine();

            Console.WriteLine("--- Delete operations ---");
            Console.WriteLine("Deleting newly created incident and associated new contact and account");
            //Clean up of created records
            incidentService.DeleteIncident(newIncidentId);
            contactService.DeleteContact(newContactId);
            accountService.DeleteAccount(newAccountId);

            Console.WriteLine();
            Console.WriteLine("//--- Application END ---//");

            // Pause the console so it does not close.
            Console.WriteLine("Press the <Enter> key to exit.");
            Console.ReadLine();
        }
    }
}
