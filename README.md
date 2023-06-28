# AddressesAPI
A simple API for address management. This is an ASP.NET Core application that manages addresses, including geocoding and distance calculation.

# Prerequisites
To run this project, you will need:
- .NET Core 6.0 (recommended)
- Visual Studio (recommended) or a similar IDE

# Getting Started
To get a local copy up and running follow these simple steps:
1. This is the link to the github repository:
https://github.com/andrejivanov98/AddressesAPI
2. Clone the repository to your local machine:
git clone https://github.com/andrejivanov98/AddressesAPI.git
3. Navigate to the project directory:
cd AddressesAPI(for example)
4. Open AddressesApp.sln in Visual Studio
5. In Visual Studio, make sure Addresses.API is set as the startup project
6. Start the application by pressing "F5" or by clicking the "Start Debugging" button.

# Using the API
Once the application is running, you will be automatically redirected to the Swagger UI.
This provides a user-friendly way to interact with the API, and you can use it to send requests to the various endpoints:
- GET /addresses: Returns a list of all addresses.
- GET /addresses/{id}: Returns the address with the specified ID.
- POST /addresses: Creates a new address.
- PUT /addresses/{id}: Updates the address with the specified ID.
- DELETE /addresses/{id}: Deletes the address with the specified ID.
- GET /addresses/{id1}/{id2}/distance: Returns the distance (in kilometers) between the two addresses with the specified IDs.

# Contact
For any further questions, you can reach me at ivanovandrej10@yahoo.com.
