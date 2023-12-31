# Vertical Slice Minimal Api Template

[![Build](https://github.com/EnisMulic/VerticalSliceMinimalApi/actions/workflows/ci.yml/badge.svg)](https://github.com/EnisMulic/VerticalSliceMinimalApi/actions/workflows/ci.yml)

## Getting Started

Install the template from Nuget Gallery

```
dotnet new install VerticalSliceMinimalApi::7.0.0
```

## Local Development

1. Clone the repository

```sh
git clone https://github.com/EnisMulic/VerticalSliceMinimalApi
```

2. Install the template

```sh
dotnet new install ./VerticalSliceMinimalApi
```

3. Use the installed template to create your API

```sh
dotnet new vsma --name ProjectName
```

## Synopsis

```
dotnet new vsma --name [ProjectName]
    [-gh|--git-host [Github|AzureDevOps|None]]
    [-db|--database [MsSql|PostgreSql|None]]
    [-oa|--auth [Entra|None]]
```

### Options

- `--name [ProjectName]`  
  Specify the name of the project you are creating, this will replace all occurrences of `ProjectName` in the template with the name you pass in.

- `-gh|--git-host [Github|AzureDevOps|None]`  
  Choose the platform you will host your projects git repository, this will give you a base CI workflow, pull request template, and anything specific to the platform that might be of use. The default value is `None`.
- `-db|--database [MsSql|PostgreSql|None]`  
  Choose what database to use for your project. The default is `None`
- `-oa|--auth [Entra|None]`
  Choose a auth provider to use for your project.
  The default is `None` which will configure a JwtBearer Auth that you can use with `dotnet user-jwts`.

## Configuration

Configure the application using `appsettings.json`, `appsettings.Development.json` or [dotnet secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows)

```sh
dotnet user-secrets init --project src/Api
```

### Define Constants

To test/develop the template with specific options add a `<DefineConstants>` block to `Directory.Build.props` file.

```diff
<PropertyGroup>
  <TargetFramework>net7.0</TargetFramework>
  <Nullable>enable</Nullable>
  <ImplicitUsings>enable</ImplicitUsings>

  <AnalysisLevel>7-recommended</AnalysisLevel>
  <EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>

+ <DefineConstants>UseDatabase;UseMsSql</DefineConstants>
</PropertyGroup>
```

## How to Run

To start the app run:

```sh
dotnet run --project src/Api
```

## Database

The template uses MsSql migrations by default, so if you select PostgreSql as your database you will need to remove the `Application/Infrastructure/Persistance/Migrations` folder and create a new migration script.

When you run the application the database will be created (if it doesn't exist) and the migrations will be applied.

To run the migrations you will need to add the following flags to your ef commands.

- `-p | --project src/Application`
- `-s | --startup-project src/Api`
- `-o | --output-dir Infrastructure/Persistance/Migrations`

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
