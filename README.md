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

# Proud of:
1. Clean Architecture: The project is structured following the Clean Architecture principles. This approach separates concerns and makes the codebase easier to maintain and evolve over time. It makes the solution more scalable.
2. Error Handling: The project includes comprehensive error handling. This allows for better debugging and helps to provide clear information on what went wrong when an error occurs.
3. Using CQRS pattern: A pattern that separates read and update operations for a data store. Implementing CQRS in the application can maximize its performance, scalability, and security.

# Less satisfied with:
1. Error Messaging: Currently, the error messages are somewhat generic, mostly because of the error handling on low level in the repositories. It can be good things to catch them in the repository, but that makes them more generic, as if the application scale in future it will not be so clear for the user. There could be way to make more strict error messages, but we should complicate things a little bit. Anyway, there is error handling on each layer, so, if some error is not catched in the repository will be catched in the service layer or in the controller where the message will be more strict.
2. Geolocation Accuracy: There's a discrepancy between the distances calculated by the application and the distances provided by other mapping services like Google Maps. This is due to using a simplified model of the Earth for the distance calculations, where it calculates the distance in a straight line, not as Google Maps by finding the best route. I wanted to use Google Maps API for better accuracy, but had some issues with billing accounts while registering the API key and enabling the Geocoding API and that is why I used Nominatim API instead.
