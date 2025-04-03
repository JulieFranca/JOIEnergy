# Welcome to PowerDale

PowerDale is a small town with around 100 residents. Most houses have a smart meter installed that can save and send information about how much energy a house has used.

There are three major providers of energy in town that charge different amounts for the power they supply.

- _Dr Evil's Dark Energy_
- _The Green Eco_
- _Power for Everyone_

# Introducing JOI Energy

JOI Energy (formerly EcoMart) is a start-up in the energy industry. Rather than selling energy they want to differentiate themselves from the market by recording their customers' energy usage from their smart meters and recommending the best supplier to meet their needs.

You have been placed in a small support team along with their development team, whose current goal is to produce an API which their customers and smart meters will interact with.

Unfortunately, two members of the support team are on annual leave, and another one has called in sick! You are left with another ThoughtWorker to support the application and provide value to the client by having minimum/no disruptions in the application

## Problem Statement

The immediate challenge is to increase the stability of the application by maintaining and improving the code base while in parallel the development of the API for customer and smart meter interaction is ongoing

## Users

To trial the new JOI software 5 people from the JOI accounts team have agreed to test the service and share their energy data.

| User    | Smart Meter ID  | Power Supplier        |
| ------- | --------------- | --------------------- |
| Sarah   | `smart-meter-0` | Dr Evil's Dark Energy |
| Peter   | `smart-meter-1` | The Green Eco         |
| Charlie | `smart-meter-2` | Dr Evil's Dark Energy |
| Andrea  | `smart-meter-3` | Power for Everyone    |
| Alex    | `smart-meter-4` | The Green Eco         |

These values are used in the code and in the following examples too.

## Overview

JOI Energy is a new energy company that uses data to ensure customers are able to be on the best price plan for their energy consumption.

## API

Below is a list of API endpoints with their respective input and output. Please note that the application needs to be running. For more information about how to run the application, please refer to [run the application](#run-the-application) section below.

### Store Readings

Endpoint

```
POST /readings/store
```

Example of body

```json
{
    "smartMeterId": <smartMeterId>,
    "electricityReadings": [
        { "time": <time>, "reading": <reading> },
        { "time": <time>, "reading": <reading> },
        ...
    ]
}
```

Parameters

| Parameter      | Description                                |
| -------------- | ------------------------------------------ |
| `smartMeterId` | One of the smart meters' id listed above   |
| `time`         | The time when the _delta_ is measured      |
| `reading`      | The _delta_ reading since the last reading |

Example readings

| Date (`GMT`)                        | Reading (`kW`) |
| ----------------------------------- | -------------: |
| `2020-11-11T08:00:00.0000000+00:00` |         0.0503 |
| `2020-11-12T08:00:00.0000000+00:00` |         0.0213 |

In the above example, `0.0213 kW` where consumed between `2020-11-11 8:00` and `2020-11-12 8:00`.

Posting readings using CURL

```console
$ curl \
  -X POST \
  -H "Content-Type: application/json" \
  "http://localhost:5000/readings/store" \
  -d '{"smartMeterId":"smart-meter-0","electricityReadings":[{"time":"2020-11-11T08:00:00.0000000+00:00","reading":0.0503},{"time":"2020-11-12T08:00:00.0000000+00:00","reading":0.0213}]}'
```

The above command returns 200 OK and `{}`.

### Get Stored Readings

Endpoint

```
GET /readings/read/<smartMeterId>
```

Parameters

| Parameter      | Description                              |
| -------------- | ---------------------------------------- |
| `smartMeterId` | One of the smart meters' id listed above |

Retrieving readings using CURL

```console
$ curl "http://localhost:5000/readings/read/smart-meter-0"
```

Example output

```json
[
  { "time": "2020-11-11T08:00:00.000000Z", "reading": 0.0503 },
  { "time": "2020-11-12T08:00:00.000000Z", "reading": 0.0213 },
  ...
]
```

### View Current Price Plan and Compare Usage Cost Against all Price Plans

Endpoint

```
GET /price-plans/compare-all/<smartMeterId>
```

Parameters

| Parameter      | Description                              |
| -------------- | ---------------------------------------- |
| `smartMeterId` | One of the smart meters' id listed above |

Retrieving readings using CURL

```console
$ curl "http://localhost:5000/price-plans/compare-all/smart-meter-0"
```

Example output

```json
{
  "DrEvilsDarkEnergy": 94.87181867550794,
  "TheGreenEco": 18.974363735101587,
  "PowerForEveryone": 9.487181867550794
}
```

### View Recommended Price Plans for Usage

Endpoint

```
GET /price-plans/recommend/<smartMeterId>[?limit=<limit>]
```

Parameters

| Parameter      | Description                                          |
| -------------- | ---------------------------------------------------- |
| `smartMeterId` | One of the smart meters' id listed above             |
| `limit`        | (Optional) limit the number of plans to be displayed |

Retrieving readings using CURL

```console
$ curl "http://localhost:5000/price-plans/recommend/smart-meter-0?limit=2"
```

Example output

```json
[
  {
    "key": "PowerForEveryone",
    "value": 9.487181867550794
  },
  {
    "key": "TheGreenEco",
    "value": 18.974363735101587
  }
]
```

## Requirements

The project requires [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).

## Compatible IDEs

Tested on:

- Visual Studio 2022 (17.1)
- Visual Studio for Mac (8.10)
- Visual Studio Code (1.64)

## Useful commands

From the terminal/shell/command line tool, use the following commands to build, test and run the API.

### Build the project

```console
$ dotnet build
```

### Run the tests

```console
$ dotnet test JOIEnergy.Tests
```

### Run the application

Run the application which will be listening on port `5000`.

```console
$ dotnet run --project JOIEnergy
```

# JOIEnergy API

API for electrical energy management developed in ASP.NET Core.

## 🚀 Features

### Authentication and Authorization
- JWT (JSON Web Token) authentication implementation
- Login endpoint for token generation
- Route protection with authentication middleware
- Custom token validation settings

### Reading Management
- Endpoint to retrieve readings from specific meters
- Automatic generation of test reading data
- Meter and reading validation

### Price Plans
- Multiple energy providers
- Different unit rates per provider
- Meter association with price plans

## 🛠️ Technologies Used

- ASP.NET Core
- JWT Authentication
- Swagger/OpenAPI
- CORS
- Entity Framework Core (for future implementations)

## 📋 Prerequisites

- .NET 6.0 or higher
- Visual Studio 2022 or VS Code
- Postman or similar for API testing

## 🔧 Setup

1. Clone the repository
2. Navigate to the project folder
3. Run `dotnet restore`
4. Run `dotnet build`
5. Run `dotnet run`

## 🔐 Authentication

To access protected endpoints:

1. Login at `/auth/login` with credentials:
   ```json
   {
     "username": "admin",
     "password": "admin"
   }
   ```

2. Use the returned token in the Authorization header:
   ```
   Authorization: Bearer {your-token}
   ```

## 📝 Endpoints

### Authentication
- `POST /auth/login` - Authenticates user and returns JWT token

### Readings
- `GET /readings/read/{smartMeterId}` - Retrieves readings from a specific meter

### Price Plans
- `GET /price-plans` - Lists all available price plans
- `GET /price-plans/{smartMeterId}` - Retrieves price plan for a specific meter

## 🔒 Security

- JWT Bearer Authentication implementation
- Token validation with expiration
- Protection against common attacks
- CORS configured for development

## 📦 Project Structure

```
JOIEnergy/
├── Controllers/
│   ├── AuthController.cs
│   ├── MeterReadingController.cs
│   └── PricePlanController.cs
├── Services/
│   ├── Auth/
│   │   ├── AuthService.cs
│   │   ├── IAuthService.cs
│   │   └── ApiResponse.cs
│   ├── Account/
│   ├── MeterReading/
│   └── Price/
├── Domain/
├── Enums/
├── Generator/
├── Settings/
└── Startup.cs
```

## 🔄 Recent Changes

### Authentication
- Authentication service implementation
- JWT middleware addition
- Authentication events configuration
- Error response standardization

### Swagger
- Swagger UI configuration
- Endpoint documentation
- Swagger authentication setup
- API usage examples

### Middleware
- Middleware order reorganization
- CORS implementation
- Routing configuration
- Authentication error handling

### Responses
- Response standardization with ApiResponse
- Consistent error handling
- Error messages in English

## 🚧 Next Steps

- [ ] Implement unit tests
- [ ] Add logging
- [ ] Implement caching
- [ ] Add detailed documentation
- [ ] Implement rate limiting
- [ ] Add monitoring

## 📄 License

This project is under the MIT license.
