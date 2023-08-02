# Project Name

A .NET Core application demonstrating the use of RabbitMQ, multi-threading, and ORM.

## Description

This project showcases the implementation of a .NET Core application that utilizes RabbitMQ for message queueing, multi-threading for parallel processing, and ORM (Entity Framework Core) for database operations.

## Features

- Message queueing using RabbitMQ
- Multi-threading for concurrent tasks
- ORM (Entity Framework Core) for database operations

## Future Improvements

- Design a paginated product listing page using Elasticsearch
- Organize project files according to the MVC (Model-View-Controller) pattern
- Implement caching using Redis for improved performance

## Getting Started

### Prerequisites

- .NET Core SDK [Download Here](https://dotnet.microsoft.com/download)

### Installation

1. Clone the repository: `git clone https://github.com/oguzkagankoc/Case`
2. Navigate to the project directory: `cd project-directory`
3. Restore dependencies: `dotnet restore`

### Usage

1. Set up the necessary configuration values in `appsettings.json`:
   ```json
   {
       "ConnectionStrings": {
           "DefaultConnection": "Host=localhost;Port=5432;Database=case;Username=postgres;Password=122333"
   }
Run the application:
`dotnet run`