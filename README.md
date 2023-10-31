# Vertical Slice Minimal Api

[![Build](https://github.com/EnisMulic/VerticalSliceMinimalApi/actions/workflows/ci.yml/badge.svg)](https://github.com/EnisMulic/VerticalSliceMinimalApi/actions/workflows/ci.yml)

## Getting Started 

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

## How to Run

To start the app run:
```sh
dotnet run --project src/Api
```

## Configuration

Configure the application using `appsettings.json`, `appsettings.Development.json` or [dotnet secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows)

```sh
dotnet user-secrets init --project src/Api
```

## Auth

Generate a bearer token using [dotnet user-jwts](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/jwt-authn?view=aspnetcore-7.0&tabs=windows) with optional roles and policies

```
dotnet user-jwts create
dotnet user-jwts create --role "Administrator"
```
