## User Management Sample API

This is a sample .NET Core Web API project that demonstrates CRUD operations with Entity Framework Core and SQL Server, with support for Redis cache and Docker integration. This sample is using Sagger default UI to demonstrate how User API works on CRUD.

## Technologies Used

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Redis (for caching)
- Docker

## Features

- User CRUD operations over API
- DTO for data display and transfer
- Redis cache for improving read performance
- Exception handling and proper HTTP responses

## Getting Started

## Prerequisites

- .NET 6/7 SDK
- Docker
- Redis containers (see Docker section below)

## Docker & Redis Container

Make sure you have Docker installed in your local machine.
Open Terminal and run docker run -d -p 6379:6379 --name UserManagementRedisCache redis

## EF Core Migration

Make sure to clone / download the project in your local machine
Open Terminal and cd to correct project path. Eg: ..\source\repos\SampleUserManagement\SampleUserManagement
Run the following code in Terminal
1) dotnet ef migrations add InitialUserManagement
2) dotnet ef database update

## Running the Project

dotnet run

The project will be running in local available port. Eg: http://localhost:5196/