# Accounts Service

## API

Generating new controller:
```sh
dotnet aspnet-codegenerator controller -name [ControllerName] -async -api -actions -outDir .\Controllers\
```
## Database

This service uses a PostgreSQL database.

```bash
## Create the migration
dotnet ef migrations add [Name] --startup-project .\Accounts.API\Accounts.API.csproj --project .\Accounts.Infrastructure\Accounts.Infrastructure.csproj --output-dir .\Database\Migrations

## Update the database
dotnet ef database update --startup-project .\Accounts.API\Accounts.API.csproj --project .\Accounts.Infrastructure\Accounts.Infrastructure.csproj
```