# Football League Management System

## Overview

This project implements a football league management system, providing a comprehensive solution for creating and managing teams, seasons, and match results. The system utilizes **CQRS** and the **Result pattern**, adhering to **Clean Architecture** principles to ensure maintainabi


## Features

- **Team Management**: Create, retrieve, and delete teams within the league.
- **Season Management**: Create and manage seasons, linking multiple teams for competition.
- **Match Generation**: Automatically generate matches for each season, including home and away games.
- **Match Results**: Update results for previously played matches, allowing for accurate statistics.
- **Statistics Retrieval**: Fetch current standings, past matches, and upcoming fixtures for teams in the active season.
- **Error Handling**: Custom middleware for exception handling, ensuring user-friendly error messages.
- **Cancellation Token Support**: Operations can be canceled with a timeout of 40 seconds to prevent long-running requests.
- **Filtering**: Implement pagination and filtering for retrieving teams. This includes a GetAllTeamsFilter that allows users to search for teams by name, enhancing data retrieval efficiency.
- **Validation**: Utilize FluentValidation to validate commands, such as team creation requests. For example, CreateTeamCommandValidator ensures that team names and president details meet specific criteria before processing.

## Technologies Used

- **ASP.NET Core**: For building the Web API.
- **Entity Framework Core**: For database interactions and ORM.
- **SQL Server**: As the database for storing league data.
- **AutoMapper**: This is for mapping between entities and DTOs.
- **FluentValidation**: For input validation.
- **Dependency Injection**: This is used to manage dependencies across the application.

## Getting Started

1. **Clone the Repository**:  
   Clone this repository to your local machine using `git clone <repository-url>`.

2. **Set Up Database**:  
   Update the connection string in `appsettings.json` to point to your database server.

3. **Create Sample Data**:  
   1. create 4 teams
   2. create a season with name and team IDs
   3. go to match endpoint and auto-generate matches
   4. you can update results for past matches

5. **Run the Application**:  
   Start the application using Visual Studio or the command line.

6. **API Endpoints**:  
   Access the API documentation (Swagger) for detailed information on available endpoints.
