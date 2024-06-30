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

- Onion architecture can solve problem of separation of concern and tightly coupled components from N-layered architecture.
- All layers are depended on inner layer.
- The core of the application is the domain layer.
- Provide more testability than N-layered architecture.

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

# Domain model

Domain model are of 2 types


## Validations in Domain driven design:



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

