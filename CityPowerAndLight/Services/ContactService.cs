using CityPowerAndLight.Model;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;

namespace CityPowerAndLight.Services
{
    internal class ContactService
    {
        private readonly IOrganizationService _organizationService;
        private readonly string _entityLogicalName;

        public ContactService(IOrganizationService organizationService, string entityLogicalName)
        {
            _organizationService = organizationService;
            _entityLogicalName = entityLogicalName;
        }

        /// <summary>
        /// Retrieves all contacts from the Dataverse and retuens a list of Contact objects.
        /// </summary>
        public List<Contact> GetAllContacts()
        {
            try
            {
                QueryExpression query = new QueryExpression("contact")
                {
                    ColumnSet = new ColumnSet(true)
                };

                EntityCollection allContacts = _organizationService.RetrieveMultiple(query);
                List<Contact> contactsList = allContacts.Entities.Select(contact => contact.ToEntity<Contact>()).ToList();

                return contactsList;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving contacts: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific contact by its ID and returns a Contact object.
        /// </summary>
        public Contact GetContact(Guid contactId)
        {
            try
            {
                // Retrieve the contact that matches ID and show all fields
                Entity contactFound = _organizationService.Retrieve("contact", contactId, new ColumnSet(true));
                Contact contact = contactFound.ToEntity<Contact>();
                return contact;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving contact: {ex.Message}");
            }   
        }

        /// <summary>
        /// Creates a new contact with specified details and returns the GUID of the new contact.
        /// </summary>
        public Guid CreateContact(String firstName, String lastName, String email, String phone, Guid accountId)
        {
            try
            {
                // Create a new contact
                Entity contact = new Entity("contact")
                {
                    ["firstname"] = firstName,
                    ["lastname"] = lastName,
                    ["emailaddress1"] = email,
                    ["telephone1"] = phone,
                    ["parentcustomerid"] = new EntityReference("account", accountId)
                };
                // Save the contact to the database
                Guid newContact = _organizationService.Create(contact);
                return newContact;
            }
            catch (Exception ex)
            { 
                throw new Exception($"An error occurred while creating contact: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates the email address of a contact with the specified ID and returns the updated Contact object.
        /// </summary>
        public Contact UpdateContact(Guid contactId)
        {
            try 
            {
                // Find contact by ID
                Entity updateContact = new Entity("contact", contactId);
                // Update their email
                updateContact["emailaddress1"] = "helloduckie@outlook.com";
                _organizationService.Update(updateContact);
                //Use GetContact method to retrieve the updated contact
                var updatedContact = GetContact(contactId);
                
                return updatedContact;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating contact: {ex.Message}");
            }
        }

        /// <summary>
        /// Deleted a contact with the specified ID.
        /// </summary>
        public void DeleteContact(Guid contactToRemove)
        {
            try 
            {
                // Find contact entry by ID to delete
                _organizationService.Delete("contact", contactToRemove);
                Console.WriteLine("Contact deleted with ID: " + contactToRemove);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting contact: {ex.Message}");
            }
        }
    }
}
