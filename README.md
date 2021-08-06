# SelfCheckoutMachineAPI

This is an ASP.NET Web application that I used as an API. The application runs via IIS Express(Google Chrome). I used Postman to test the different API endpoints.
This API simulates a self-checkout machine in a supermarket that can be restocked and calculates the change, that it'll give back.

The target framework of the application is: .NET Framework 4.7.2
The IDE in which I developed is: Visual Studio 2019

The SelfCheckoutController contains 3 different API endpoints:
- POST /api/v1/Stock
- GET /api/v1/Stock
- POST /api/v1/Checkout

The application stores the data in memory. There is no database behind the application.
