🏠 Rentals App

Rentals App is a modern web application designed to simplify renting items such as cars, bikes, tools, or other equipment. Whether you're a renter looking for convenience or an owner wanting to list your items, this platform makes it easy to connect and transact securely.

Project Structure
src/Rental.API: The main API project. This is the entry point of the application.
src/Rental.Application: Contains the application logic. This layer is responsible for the application's behavior and policies.
src/Rental.Domain: Contains enterprise logic and types. This is the core layer of the application.
src/Rental.Infrastructure: Contains infrastructure-related code such as database and file system interactions. This layer supports the higher layers.

Packages and Libraries
This project uses several NuGet packages and libraries to achieve its functionality:

Serilog: This library is used for logging. It provides a flexible and easy-to-use logging API.

MediatR: This library is used to implement the Command Query Responsibility Segregation (CQRS) pattern. In this solution, commands (which change the state of the system) and queries (which read the state) are separated for clarity and ease of development.

Entity Framework: This is an open-source ORM framework for .NET. It enables developers to work with data using objects of domain-specific classes without focusing on the underlying database tables and columns where this data is stored.

Azure Storage Account: This service is used for handling blobs. Blobs, or Binary Large Objects, are a type of data that can hold large amounts of unstructured data such as text or binary data, including images, documents, streaming media, and archive data.

Microsoft Identity Package: This package is used for handling user identity in the web API. It provides features such as authentication, authorization, identity, and user access control.

Please refer to the official documentation of each package for more details and usage examples.

Getting Started
Prerequisites
.NET 8.0
Visual Studio 2022 or later
Building
To build the project, open the Rental.sln file in Visual Studio and build the solution.

Running
To run the project, set Rental.API as the startup project in Visual Studio and start the application.

API Endpoints
GET /api/rental

Parameters: searchPhrase, pageSize, pageNumber, sortBy, sortDirection
Authorization Bearer token
GET /api/customers/{id}

Parameters: id,
DELETE /api/customers/{id}

Parameters: id
Authorization: Bearer token
POST /api/restaurants

Body: JSON object with properties Name, Email, ContactNumber, DateCreated, DateUpdated, DateDeleted, IsDeleted, customerImageUrl, City, Street, Postal
Authorization: Bearer token
