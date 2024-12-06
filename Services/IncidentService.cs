using CityPowerAndLight.Model;
using CityPowerAndLight.View;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CityPowerAndLight.Services
{
    internal class IncidentService
    {
        //Get IOrganizationService instance
        private readonly IOrganizationService _organizationService;
        private readonly string _entityLogicalName;

        public IncidentService(IOrganizationService organizationService, string entityLogicalName)
        {
            _organizationService = organizationService;
            _entityLogicalName = entityLogicalName;
        }

        public Guid CreateIncident(String title, String description, Guid accountContact, Guid customerContact)
        {
            try 
            {
                // Create a new incident
                Incident incident = new Incident
                {
                    Title = title,
                    Description = description,
                    PriorityCode = incident_prioritycode.Normal,
                    CustomerId = new EntityReference(Account.EntityLogicalName, accountContact),
                    PrimaryContactId = new EntityReference(Contact.EntityLogicalName, customerContact),
                };
                // Save the incident to the database
                Guid newIncident = _organizationService.Create(incident);
                return newIncident;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating incident: {ex.Message}");
            }
        }

        public List<Incident> GetAllIncidents()
        {
            try
            {
                //Retrieve all incidents
                QueryExpression query = new QueryExpression(_entityLogicalName)
                {
                    ColumnSet = new ColumnSet(true)
                };
                EntityCollection allIncidents = _organizationService.RetrieveMultiple(query);
                List<Incident> incidentsList = allIncidents.Entities.Select(incident => incident.ToEntity<Incident>()).ToList();

                return incidentsList;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving incidents: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific incident by its ID and returns an Incident object.
        /// </summary>
        public Incident GetIncident(Guid incidentId)
        {
            try
            {
                // Retrieve the incident
                Incident incident = _organizationService.Retrieve(_entityLogicalName, incidentId, new ColumnSet(true)).ToEntity<Incident>();

                return incident;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving incident: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates a specific incident by its ID.
        /// </summary>
        public void UpdateIncident(Guid incidentId, string field, string newValue)
        {
            try 
            {
                // Retrieve the incident
                Incident incident = _organizationService.Retrieve(_entityLogicalName, incidentId, new ColumnSet(true)).ToEntity<Incident>();
                // Update the incident on the specified field
                if (field.Equals("title", StringComparison.OrdinalIgnoreCase))
                {
                    incident.Title = newValue;
                }
                else if (field.Equals("priority", StringComparison.OrdinalIgnoreCase))
                {
                    if (Enum.TryParse(newValue, true, out incident_prioritycode priority)) //convert the string value (newValue) to the incident_prioritycode enum
                    {
                        incident.PriorityCode = priority;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid priority value specified.");
                    }
                }
                else if (field.Equals("description", StringComparison.OrdinalIgnoreCase))
                {
                    incident.Description = newValue;
                }
                else
                {
                    throw new ArgumentException("Invalid field specified. Only 'title', 'priority' and 'description' are allowed.");
                }
                // Save the updated incident to the database
                _organizationService.Update(incident);
                Console.WriteLine($"{field} on incident {incidentId} updated.");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating incident: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a specific incident by its ID.
        /// </summary>
        public void DeleteIncident(Guid incidentId)
        {
            try
            {
                // Delete the incident
                _organizationService.Delete(_entityLogicalName, incidentId);
                Console.WriteLine("Incident deleted with ID: " + incidentId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting incident: {ex.Message}");
            }
        }
    }
}
