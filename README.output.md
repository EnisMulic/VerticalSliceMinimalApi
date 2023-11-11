# Vertical Slice Minimal Api

## Getting Started

* [ ] Todo: Add custom getting started steps

## Configuration

Configure the application using `appsettings.json`, `appsettings.Development.json` or [dotnet secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows)

```sh
dotnet user-secrets init --project src/Api
```

## How to Run

To start the app run:

```sh
dotnet run --project src/Api
```

## Database

When you run the application the database will be created (if it doesn't exist) and the migrations will be applied.

To run the migrations you will need to add the following flags to your ef commands.

* `-p | --project src/Application`
* `-s | --startup-project src/Api`
* `-o | --output-dir Infrastructure/Persistance/Migrations`

For example, to add a new migration:

```sh
dotnet ef migrations add \
  -p src/Application \
  -s src/Api \
  -o Infrastructure/Persistance/Migrations "MigrationName"
```

```sh
dotnet ef migrations add -p src/Application -s src/Api -o Infrastructure/Persistance/Migrations "MigrationName"
```


## Auth

Generate a bearer token using [dotnet user-jwts](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/jwt-authn?view=aspnetcore-7.0&tabs=windows) with optional roles and policies

```
dotnet user-jwts create
dotnet user-jwts create --role "Administrator"
```
