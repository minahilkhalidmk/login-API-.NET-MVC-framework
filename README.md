Secure .NET Identity API (APIlogin)
A secure, fully-featured backend API built with .NET 9 and Entity Framework Core. This project provides pre-built user authentication, secure database storage, and protected data endpoints using ASP.NET Core Identity.

Features
Built-in Authentication: Out-of-the-box user registration, login, and token generation using .AddIdentityApiEndpoints().

Opaque Token Security: Generates highly secure, encrypted Data Protection tokens (valid for 1 hour by default) to prevent token tampering.

Secured Endpoints: Route protection using the [Authorize] attribute to ensure only authenticated users can access specific data.

Interactive API Documentation: Fully integrated Swagger UI featuring a functional "padlock" for testing authenticated requests.

Relational Database: Entity Framework Core integration configured for SQL Server (SQLEXPRESS).

Tech Stack
Language: C#

Framework: .NET 9.0

ORM: Entity Framework Core

Database: SQL Server

Security: ASP.NET Core Identity

API Documentation: Swashbuckle.AspNetCore (Swagger)

Prerequisites
To run this project locally, ensure you have the following installed on your machine:

Visual Studio 2022 (or newer)

.NET SDK (Version 8.0 or 9.0)

SQL Server Express (or a standard SQL Server instance)

Getting Started
Follow these steps to set up the database and run the API locally.

1. Clone or Open the Project
Open the APIlogin.sln file in Visual Studio.

2. Verify the Connection String
Open appsettings.json and ensure the DefaultConnection string points to your local SQL Server instance. It is currently configured for a local SQLEXPRESS instance.

3. Set Up the Database
Open the Package Manager Console in Visual Studio and run the following command to build your SQL database and apply all tables (including Users and Products):

PowerShell
Update-Database
4. Run the Application
Press F5 or click the green Play button in Visual Studio to launch the application. The Swagger UI will automatically open in your default web browser.

How to Test Authentication in Swagger
This API requires an Access Token to access protected routes (like /secret-data or the Products controller).

Step 1: Register a New User
Scroll to the POST /register endpoint. Click Try it out, enter an email and a strong password in the JSON body, and click Execute.

Step 2: Generate an Access Token
Scroll to the POST /login endpoint. Enter the exact same email and password you just created and click Execute. Look at the server response and copy the long string of characters next to "accessToken". Do not copy the quotation marks.

Step 3: Unlock the Padlock
Scroll to the top of the Swagger page and click the green Authorize padlock button. In the text box, type the word Bearer, add a single space, and paste your token.

Example: Bearer CfDJ8Ls2AE4LLp...

Click Authorize and close the window.

Step 4: Access Secure Data
You can now scroll to any secured GET or POST endpoint and execute the request. Because you have provided the key, the server will process your request and return a 200 OK response!
