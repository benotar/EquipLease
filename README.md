# EquipLease  
This repository contains a backend application for managing technology equipment placement contracts.  

## Description  
EquipLease is a backend service built using:  

- **Backend**: ASP.NET Core  
- **Database**: Azure SQL Database (Code First with EF Core)  

The service provides RESTful API endpoints to manage contracts, verify facility space availability, and retrieve contract details.  

## Features  

### Contract Management  
- **Contract Creation**:  
  - **Endpoint**: Allows users to create new contracts for placing equipment in production facilities.  
  - **Validation**: Checks the available space in the production facility to ensure it can accommodate the specified equipment.  
  - **Asynchronous Processing**: Initiates a background process to log contract details after successful creation.  

- **Get Contracts**:  
  - **Endpoint**: Retrieves a list of active contracts.  
  - **Response Details**: Includes facility names, equipment types, and the number of units for each contract.  

### Data Access  
- **Entity Framework Core**:  
  - Leverages EF Core as the ORM for database operations, using the Code First approach.  
- **Repository Pattern**:  
  - Implements repository classes for decoupled data access, promoting cleaner and more testable code.  
  - Example: The `ContractRepository` handles contract-specific data operations.  
- **Unit of Work**:  
  - Manages transactions across multiple repository operations, ensuring data consistency.  
  - Centralized through the `IUnitOfWork` interface.  

### Business Logic  
- **Space Validation**: Verifies the facility's available space before processing contract requests. Returns an error if there isn’t enough space.  

### Azure Integration  
- **Database Hosting**: The application stores production facility, equipment type, and contract data in an Azure SQL Database.  
- **Service Hosting**: The API is hosted in Azure App Service, providing public access.  

### Security  
- **API Key**: Secures access to API endpoints by validating requests with a static API key.  

### Testing  
- **Unit Tests**:  
  - Covers critical API functionality, including contract creation, retrieval, and validation scenarios.  

### Background Processing  
- **Asynchronous Queue**:  
  - Implements an Azure Storage Queue and `BackgroundService` for post-contract creation logging.  

### CI/CD Pipeline  
- **GitHub Actions Workflow**:  
  - Automates build, test, and deployment processes to Azure App Service.  


## Contact
If you have any questions, create issue, or contact me using [Instagram](https://www.instagram.com/benotar_) or [Telegram](https://t.me/benotaar).
