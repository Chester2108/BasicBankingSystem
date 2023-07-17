#Instructions
  Build a simple application with dotnet core providing APIs below:
  - Create Account API: Input: Username, Initial Balance. Output: Account Id.
  - Transfer Money API: Input: Source Account Id, Destination Account Id, Transfer Amount. Output Transaction Id.
  Requirements: 
  - Username have to be unique.
  - Account Balance cannot be lower than 0.
  - Application has to run correctly under concurrency situation (multiple requests submitted at once). 
  - Use MSSQL or MySQL as DB
#How to run
  -This a code first project
    -Change the DB connectionstring
    -run the Update-Database in Package Manager Console to Create the database to your declared database connection
#Used Tech
  -Asp.net Core
  -Entity Framework
  -Bootstrap
  -KendoUI
  -JavaScript
  -InMemory(for simulation)
  -MSSQL
