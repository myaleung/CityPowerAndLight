using CityPowerAndLight.Model;

/// <summary>
/// Methods for printing to the console
/// </summary>
namespace CityPowerAndLight.View
{
    internal class ConsoleInterface
    {
        /// <summary>
        /// Prints the details of a contact to the console.
        /// </summary>
        public static void PrintContact(Contact contactToPrint, Boolean newContact = false)
        {
            Console.WriteLine();
            Console.WriteLine($"Contact ID: {contactToPrint.Id}");
            Console.WriteLine($"Name: {contactToPrint.FirstName} {contactToPrint.LastName}");
            Console.WriteLine($"Email: {contactToPrint.EMailAddress1}");
            Console.WriteLine($"Telephone: {contactToPrint.Telephone1}");
            Console.WriteLine(newContact ? $"Created On: {contactToPrint.CreatedOn}" : $"Modified On: {contactToPrint.ModifiedOn}");
            Console.WriteLine();
        }

        /// <summary>
        /// Prints the details of an account to the console.
        /// </summary>
        public static void PrintAccount(Account accountToPrint, Boolean newAccount = false)
        {
            Console.WriteLine();
            Console.WriteLine($"Account ID: {accountToPrint.Id}");
            Console.WriteLine($"Name: {accountToPrint.Name}");
            Console.WriteLine($"Email: {accountToPrint.EMailAddress1}");
            Console.WriteLine($"Telephone: {accountToPrint.Telephone1}");
            Console.WriteLine(newAccount ? $"Created On: {accountToPrint.CreatedOn}" : $"Modified On: {accountToPrint.ModifiedOn}");
            Console.WriteLine();
        }

        /// <summary>
        /// Prints the details of an incident to the console.
        /// </summary>
        public static void PrintIncident(Incident incidentToPrint)
        {
            Console.WriteLine();
            Console.WriteLine($"Incident ID: {incidentToPrint.Id}");
            Console.WriteLine($"Status: {incidentToPrint.StateCode}");
            Console.WriteLine($"Priority: {incidentToPrint.PriorityCode}");
            Console.WriteLine($"Service Stage: {incidentToPrint.ServiceStage}");
            Console.WriteLine($"Title: {incidentToPrint.Title}");
            Console.WriteLine($"Description: {incidentToPrint.Description}");
            Console.WriteLine($"Type: {incidentToPrint.CaseTypeCode}");
            Console.WriteLine($"Account: {incidentToPrint.CustomerId.Name}");
            Console.WriteLine();
        }
    }
}
