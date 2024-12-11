# C# Dataverse API Demo

## Overview

This C# application demonstrates CRUD (Create, Read, Update, Delete) operations across the Account, Contact, and Incident tables in Microsoft Dataverse. The application connects to Microsoft Dataverse, using the Dataverse SDK and performs various operations to showcase the capabilities of the Dataverse Web API.

## Prerequisites

- .NET 8 SDK
- Visual Studio 2022
- Microsoft Power Platform Dataverse Client
- Microsoft.Identity.Client (MSAL)

## Features

- **Create**: Adds new records to the Account, Contact, and Incident tables.
- **Read**: Retrieves and displays records from the Account, Contact, and Incident tables.
- **Update**: Modifies existing records in the Account, Contact, and Incident tables.
- **Delete**: Removes records from the Account, Contact, and Incident tables.

## Application Demo

The application performs the following operations:

- **Contacts**:
  - Retrieves and displays all contacts.
  - Creates new contacts.
  - Retrieves and displays contact details.
  - Updates existing contact information.

- **Accounts**:
  - Retrieves and displays all accounts.
  - Creates new accounts.
  - Retrieves and displays account details.
  - Updates existing account information.

- **Incidents**:
  - Retrieves and displays all cases.
  - Creates new cases.
  - Retrieves and displays case details.
  - Updates the title, description or priority of an existing case.

- **Clean Up**
  - Deletes a case.
  - Deletes a contact.
  - Deletes an account.

## How to Run the Application

1. Open the solution in Visual Studio 2022.
2. Create a .env file in the root of the application and provide your credentials in the following format:
``` plaintext
SECRET_ID=<secret value for your app>
APP_ID=<your app id>
TENANT_ID=<your tenant id>
```
3. Update the `appsettings.json` file with the following values:
``` json
{
  "ConnectionStrings": {
	"InstanceUrl": "https://<your environment>.dynamics.com"
  }
}
````
This should now link to your environment.

4. Press the play button or use 'F5' to run the application.
5. The console view will launch, showcasing the CRUD operations on the Account, Contact, and Case tables in Dataverse.
