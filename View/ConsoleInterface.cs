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
            try
            {
                Console.WriteLine();
                Console.WriteLine($"Contact ID: {contactToPrint.Id}");
                Console.WriteLine($"Full Name: {contactToPrint.FullName}");
                Console.WriteLine($"Email: {contactToPrint.EMailAddress1}");
                Console.WriteLine($"Company Name: {contactToPrint.ParentCustomerId?.Name ?? "N/A"}");
                Console.WriteLine($"Business Phone: {contactToPrint.Telephone1}");
                Console.WriteLine(newContact ? $"Created On: {contactToPrint.CreatedOn}" : $"Modified On: {contactToPrint.ModifiedOn}");
                Console.WriteLine();
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"A value has returned null: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while printing details: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Prints the details of an account to the console.
        /// </summary>
        public static void PrintAccount(Account accountToPrint, Boolean newAccount = false)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine($"Account ID: {accountToPrint.Id}");
                Console.WriteLine($"Full Name: {accountToPrint.Name}");
                Console.WriteLine($"Email: {accountToPrint.EMailAddress1}");
                Console.WriteLine($"Main Phone: {accountToPrint.Telephone1}");
                Console.WriteLine($"City: {accountToPrint.Address1_City}");
                Console.WriteLine($"Primary Contact: {accountToPrint.PrimaryContactId?.Name ?? "N/A"}");
                Console.WriteLine(newAccount ? $"Created On: {accountToPrint.CreatedOn}" : $"Modified On: {accountToPrint.ModifiedOn}");
                Console.WriteLine();
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"A value has returned null: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while printing details: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Prints the details of an incident to the console.
        /// </summary>
        public static void PrintIncident(Incident incidentToPrint)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine($"Incident Number: {incidentToPrint.TicketNumber}");
                Console.WriteLine($"Status: {incidentToPrint.StatusCode}");
                Console.WriteLine($"Priority: {incidentToPrint.PriorityCode}");
                Console.WriteLine($"Origin: {incidentToPrint.CaseOriginCode}");
                Console.WriteLine($"Title: {incidentToPrint.Title}");
                Console.WriteLine($"Description: {incidentToPrint.Description}");
                Console.WriteLine($"Type: {incidentToPrint.CaseTypeCode}");
                Console.WriteLine($"Customer: {incidentToPrint.CustomerId.Name}");
                Console.WriteLine($"Contact name: {incidentToPrint.PrimaryContactId?.Name ?? "N/A"}");
                Console.WriteLine($"Created On: {incidentToPrint.CreatedOn}");
                Console.WriteLine();
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"A value has returned null: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while printing details: {ex.Message}");
                throw;
            }
        }
    }
}
