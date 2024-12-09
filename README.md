# EquipLease

This repository contains a simple backend application.

## Description

This project is a backend application built using:

- **Backend**: ASP.NET Core

The backend provides a set of RESTful API endpoints.

## Features

- **Contract Creation**:
  - **Endpoint**: Allows users to create new contracts to place equipment in production facilities. Parameters such as production facility code, process equipment type code, and number of units are required.
  - **Validation**: Checks the available space in the production facility to ensure it can accommodate the specified number of equipment.
  - **Asynchronous Processing**: After creating a contract, launches an asynchronous background process to log the query results without blocking the user's query.

- **Get Contracts**:
  - **Endpoint**: Retrieves the current list of active contracts.
  - **Response Details**: Returns information including the name of the production facility, the name of the process equipment, and the number of equipment for each contract.

- **Business Validation**:
  - **Space Check**: Verifies the available space in the production facility before allowing equipment placement. Returns an error if there isn’t enough space.

- **Azure Integration**:
  - **Database Hosting**: Uses an Azure SQL database to store production facilities, equipment types, and contracts.
  - **Service Hosting**: The API is hosted in Azure App Service, providing public access to the endpoints.

- **Security**:
  - **API Key Access**: Utilizes a static API key to securely access the API. Requests are validated against this key to prevent unauthorized access.

- **Testing**:
  - **Unit Tests**: Implements tests for controller methods to ensure correct functionality. These tests cover the contract creation and retrieval endpoints, including validation scenarios.

- **Background Processing**:
  - **Asynchronous Queue**: Implements an asynchronous background processor to handle post-creation tasks, such as logging contract details. This processor is powered by Azure Storage Queue and runs through ASP.NET Core BackgroundService.

- **GitHub Actions Workflow**:
  - **Publish 🚀🚀**: This workflow is designed to automate the process of building, testing, publishing, and deploying ASP.NET Core application to Azure App Service.

#### This comprehensive set of features allows the service to efficiently manage on-premises technology equipment rental contracts, while ensuring both security and scalability.

## Contact
If you have any questions, create an issue, or contact me using [Instagram](https://www.instagram.com/benotar_) or Telegram (@benotaar).