# Console Quest

## Introduction
The Food Randomizer application is an ASP.NET Core MVC project that allows users to explore a variety of foods by randomly generating food suggestions based on user-selected criteria. This application uses a backend API to fetch food items and provides users with filtering options for specific cuisines, keeping them engaged and helping them discover new foods.

## Motivation

FunWithFood was designed to help users decide on a meal when they’re unsure of what to eat. Beyond serving as a food randomizer, this project also aims to support aspiring ASP.NET developers by offering a practical introduction to the ASP.NET Core MVC framework. It provides a hands-on approach to understanding the MVC architecture and the flow of data between application and domain services.

## Prerequisites

1. **.NET SDK 8.0**  
   Make sure you have the .NET 6 SDK installed on your machine. You can download it from the official .NET website:  
   [Download .NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

2. 1. **Microsoft SQL Server**  
   Make sure you have the MSQL Server installed on your machine. You can download it from the official Microsoft website:  
   [Download Microsoft SQL Server](https://www.microsoft.com/en-sg/sql-server/sql-server-downloads)
   1. 
3. **Git**  
   Ensure that Git is installed to clone the repository. If not, download it here:  
   [Download Git](https://git-scm.com/downloads)

4. **IDE/Text Editor**  
   You can use any text editor or IDE that supports .NET development. Some popular options include:
   - [Visual Studio Code](https://code.visualstudio.com/)
   - [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

## Step-by-Step Instructions

1. **Clone the Repository**
    ```bash
    git clone https://github.com/calvinsim2/food-randomizer.git

2. **Navigate to the Project Directory**
    ```bash
    cd food-randomizer

3. **Configure the Database**
    Set up a SQL Server database, and update the connection string in appsettings.Development.json or environment variables for Azure.

4. **Restore Dependencies Run the following command to restore the necessary dependencies for the project:**
    ```bash
    dotnet restore

5. **Apply Migrations**
    ```bash
    dotnet ef database update

6. **Build the Project. To ensure everything is set up correctly, build the project using:**
    ```bash
    dotnet build

7. **Run the Application Start the application using:**
    ```bash
    dotnet run

8. **Access the Application**
    Open a browser and navigate to http://localhost:5000 (check launchSettings.json for the correct port) to access the app.

## Usage
1. Randomize Food: Click on the randomize button at the Home page to get a random food suggestion.
2. Filter by Cuisine: Use the cuisine dropdown menu to filter food suggestions by type.
3. Login (Admin only): Login using admin account to manage current existing cuisine and foods.
4. Edit and Manage Foods (Admin only): Admin can edit food items and upload images.


## Contributors

Calvin Sim



