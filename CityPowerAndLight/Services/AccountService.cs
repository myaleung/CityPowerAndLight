using CityPowerAndLight.Model;
using Microsoft.Identity.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CityPowerAndLight.Services
{
    public class AccountService
    {
        //Get IOrganizationService instance
        private readonly IOrganizationService _organizationService;
        private readonly string _entityLogicalName;

        public AccountService(IOrganizationService organizationService, string entityLogicalName = "account")
        {
            _organizationService = organizationService;
            _entityLogicalName = entityLogicalName;
        }

        /// <summary>
        /// Retrieves all accounts from the Dataverse and returns a list of Account objects.
        /// </summary>
        public List<Account> GetAllAccounts()
        {
            try 
            {
                //Retrieve all accounts in accounts table
                QueryExpression query = new QueryExpression(_entityLogicalName)
                {
                    ColumnSet = new ColumnSet(true)
                };
                EntityCollection allAccounts = _organizationService.RetrieveMultiple(query);
                List<Account> accountList = allAccounts.Entities.Select(account => account.ToEntity<Account>()).ToList();

                return accountList;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving accounts: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific account by its ID and returns an Account object.
        /// </summary>
        public Account GetAccount(Guid accountId)
        {
            try
            {
                //Retrieve the account that matches ID
                Entity accountFound = _organizationService.Retrieve(_entityLogicalName, accountId, new ColumnSet(true));
                //Cast entity to an Account object
                Account account = accountFound.ToEntity<Account>();
                return account;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving account: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a new account with the specified details in the Dataverse and returns the ID of the new account.
        /// </summary>
        public Guid CreateAccount(string accountName, string email, string telephone, string addressCity, string addressLine1, string addressPostCode, string addressCountry)
        {
            try 
            {
                // Create a new account
                Account account = new Account
                {
                    Name = accountName,
                    EMailAddress1 = email,
                    Telephone1 = "123-456-7890",
                    Address1_City = "New York",
                    Address1_Line1 = "123 Main Street",
                    Address1_PostalCode = "10001",
                    Address1_Country = "USA",
                };
                // Save the account to the database
                Guid newAccountId = _organizationService.Create(account);
                return newAccountId;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating account: {ex.Message}");
            }
        }

        /// <summary>
        /// Update Account overloads to update the name of the account.
        /// </summary>
        public Account UpdateAccount(Guid accountId, string updateField, string newValue)
        {
            try 
            {
                // Update the account
                Account account = new Account
                {
                    Id = accountId,
                    Name = newValue,
                };

                return UpdateAccountDetails(account, accountId);
            }
            catch (Exception ex) 
            {
                throw new Exception($"An error occurred while updating account: {ex.Message}");
            }
        }

        /// <summary>
        /// Update Account overloads to update the primary contact of the account.
        /// </summary>
        public Account UpdateAccount(Guid accountId, Guid primaryContactId)
        {
            try
            {
                // Update the account primary contact
                Account account = new Account
                {
                    Id = accountId,
                    PrimaryContactId = new EntityReference("contact", primaryContactId)
                };

                return UpdateAccountDetails(account, accountId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating account: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates the specified account in the Dataverse and returns the updated Account object.
        /// </summary>
        /// <param name="accountToUpdate"></param>
        /// <param name="accountToUpdateId"></param>
        /// <returns></returns>
        private Account UpdateAccountDetails(Account accountToUpdate, Guid accountToUpdateId)
        {
            // Save the account to the database
            _organizationService.Update(accountToUpdate);
            // Use GetAccount method to retrieve the updated account
            var updatedAccount = GetAccount(accountToUpdateId);
            return updatedAccount;
        }

        /// <summary>
        /// Deletes a specific account by its ID.
        /// </summary>
        public void DeleteAccount(Guid accountId)
        {
            try
            {
                // Delete the account
                _organizationService.Delete(_entityLogicalName, accountId);
                Console.WriteLine($"Account deleted with ID: {accountId}");
            }
            catch (Exception ex)
            { 
                throw new Exception($"An error occurred while deleting account: {ex.Message}");
            }
        }
    }

}
