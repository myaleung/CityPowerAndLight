using CityPowerAndLight.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CityPowerAndLight.Services
{
    internal class AccountService
    {
        //Get IOrganizationService instance
        private readonly IOrganizationService _organizationService;
        private readonly string _entityLogicalName;

        public AccountService(IOrganizationService organizationService, string entityLogicalName)
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
        public Guid CreateAccount(string accountName, string email)
        {
            try 
            {
                // Create a new account
                Account account = new Account
                {
                    Name = accountName,
                    EMailAddress1 = email
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
        /// Updates the name of the specified account in the Dataverse and returns the updated Account object.
        /// </summary>
        public Account UpdateAccount(Guid accountId, string accountName)
        {
            try 
            {
                // Update the account
                Account account = new Account
                {
                    Id = accountId,
                    Name = accountName
                };
                // Save the account to the database
                _organizationService.Update(account);
                // Use GetAccount method to retrieve the updated account
                var updatedAccount = GetAccount(accountId);
                return updatedAccount;
            }
            catch (Exception ex) 
            {
                throw new Exception($"An error occurred while updating account: {ex.Message}");
            }
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
