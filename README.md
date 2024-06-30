## Steps to Setup

- Clone the repository and enter the directory
- Run the command `dotnet restore`
- Run the command `dotnet run`



### Yo need to generate a Bearer token to access Weather forcast API
Step- 1: Register a user (use sample request)

* POST `/auth/register`

    * Adds the user's details to the database and returns the JWT token along with the user information after the user enters their information.
    * Post Http Request Link: `https://<YOUR-DOMAIN:PORT>/auth/register`
    * Request Body Example:

        ```json
        {
            "name": "Abhiram Padhi",
            "userName": "abhirampadhi",
            "password": "pass123",
            "roles": [
                "User", 
                "Admin"
            ]
        }
        ```

    * Response Example:

        ```json
        {
            "userName": "abhirampadhi",
            "name": "Abhiram Padhi",
            "roles": [
                "User",
                "Admin"
            ],
            "isActive": false,
            "token": null,
            "password": "$argon2id$v=19$m=65536,t=3,p=1$gFcsc5mOvzCclGj+o2CqeQ$TBCPrC6HW1+kCmtCc7vai9JJv3SOgPQK/mMjiJf7X8M"
        }
        ```
Step- 2
* POST `/auth/login`

  
    
    * Request Body Example:

        ```json
        {
            "userName": "abhirampadhi",
            "password": "pass123"
        }
        ```

    * Response Example:

        ```json
        {
            "userName": "abhirampadhi",
            "name": "Abhiram Padhi",
            "roles": [
                "User",
                "Admin"
            ],
            "isActive": true,
            "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkaXR5YW9iZXJhaTEiLCJnaXZlbl9uYW1lIjoiQWRpdHlhIE9iZXJhaSIsInJvbGUiOlsiVXNlciIsIkFkbWluIl0sIm5iZiI6MTY5OTI3OTQyNywiZXhwIjoxNjk5MjgxMjI3LCJpYXQiOjE2OTkyNzk0MjcsImlzcyI6IlRlc3RJc3N1ZXIiLCJhdWQiOiJUZXN0QXVkaWVuY2UifQ.d9bAAqm1iHWmf7klIBWA2tFf2Pkvzfkee1lBvhv0_Ag",
            "password": "$argon2id$v=19$m=65536,t=3,p=1$gFcsc5mOvzCclGj+o2CqeQ$TBCPrC6HW1+kCmtCc7vai9JJv3SOgPQK/mMjiJf7X8M"
        }
        ```
         
        > Note: Token returned will be different from the example





  * Returns claims from the JWT sent as the **Bearer token** in the `Authorization` header with **User** role.
    * Get Http Request Link: `https://<YOUR-DOMAIN:PORT>/auth/usertest`
    * Request Header Example:

        ```
        Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkaXR5YTEiLCJnaXZlbl9uYW1lIjoiQWRpdHlhIE9iZXJhaSIsInJvbGUiOiJVc2VyIiwibmJmIjoxNjk5Mjc5NjA2LCJleHAiOjE2OTkyODE0MDYsImlhdCI6MTY5OTI3OTYwNiwiaXNzIjoiVGVzdElzc3VlciIsImF1ZCI6IlRlc3RBdWRpZW5jZSJ9.JpCzjncNg14Ptx1c1fRt4fZmUAIcuBSowL_WoVYZo6s
        ```


# Docker-compose commands
Build and run the Docker containers using Docker Compose: docker-compose up --build
Docker Logs: docker-compose logs
Container Status: docker ps











# Onion Architecture / Clean Architecture

<p align="center">
<img src="https://raw.githubusercontent.com/NilavPatel/dotnet-onion-architecture/main/docs/ddd-banner.png">
</p>

- Onion architecture can solve problem of separation of concern and tightly coupled components from N-layered architecture.
- All layers are depended on inner layer.
- The core of the application is the domain layer.
- Provide more testability than N-layered architecture.

<p align="center">
<img src="https://raw.githubusercontent.com/NilavPatel/dotnet-onion-architecture/main/docs/dotnet-onion-architecture.png">
</p>

## Layers

### Domain Layer:

This layer does not depend on any other layer. This layer contains entities, enums, specifications etc.  
Add repository and unit of work contracts in this layer.

### Application Layer:

This layer contains business logic, services, service interfaces, request and response models.  
Third party service interfaces are also defined in this layer.  
This layer depends on domain layer.  

### Infrastructure Layer:

This layer contains database related logic (Repositories and DbContext), and third party library implementation (like logger and email service).  
This implementation is based on domain and application layer.

### Presentation Layer:

This layer contains Webapi or UI.

## Domain model

Domain model are of 2 types

1. Domain entity (data only)
	- This model contains only fields
    - This is an anti pattern used widely. Read blog from Martin Fowler (<a href="https://martinfowler.com/bliki/AnemicDomainModel.html">here</a>)

2. Domain model (data + behaviour)
	- This model has fields and behaviours. Fields can be modify only within behaviours.
	- Follow Aggregate pattern with Aggregate root, Value object, Entity, Bounded context, Ubiqutous language

## Validations in Domain driven design:

There are 2 types of validations in DDD:
1. Model Field validations
	- Properties having valid length
    - required field validations
    - regex

    Model validations can be validated in Application layer or Domain layer.

    - Use DataAnnotation (https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-7.0)
    - Use Guard pattern (https://github.com/NilavPatel/Guard-Pattern)
        - mostly used when you have single source of truth (validations in aggregate pattern)
    - Use fluent validations pattern (https://docs.fluentvalidation.net/en/latest/aspnet.html)
        - Used when validating models instead of single properties like request models, commands

2. Business validations
	- Balance should be more than Withdraw amount
	- User should be active 
	- User name should not be exist

    Business validations can be validated in Applciation layer or Domain layer.

    Business validations have two types:

    1. Validations in same domain model
        - Balance should be more than Withdraw amount
        - User should be active 
    2. Validations against other domain models
        - User name should not be already exist

For Aggregate pattern add both types of validations inside domain layer.  
  
Problem occurs when validating domain model against other domain models.  
  
- In this case use Func<> methods to pass validations to domain model from Application layer.  
And run this Func<> from domain models.  
````csharp
public WithdrawMoney(double amount, Func<string, bool> isBankAccountActive){
    if(!isBankAccountActive(BankAccountNumber)){
        throw new DomainValidationException("Bank account is not in active state");
    }
}
````  

- Otherwise pass domain model of other type as parameter and then validate.
````csharp
public WithdrawMoney(double amount, BankAccount bankAccount){
    if(bankAccount.Status != Active){
        throw new DomainValidationException("Bank account is not in active state");
    }
}
````  

**For more details <a target="_blank" href="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice">read</a>**

### Technologies Used:

- .Net 8
- Rest API
- Entity Framework
- NLog
- Swagger
- Xunit
- Moq
- Generic Repository Pattern
- Specification pattern
