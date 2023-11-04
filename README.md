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

## Synopsis
```
dotnet new vsma --name [ProjectName]
    [-gh|--git-host [Github|AzureDevOps|None]]
```

### Options

* `--name [ProjectName]`  
  Specify the name of the project you are creating, this will replace all occurrences of `ProjectName` in the template with the name you pass in.

* `-gh|--git-host [Github|AzureDevOps|None]`  
  Choose the platform you will host your projects git repository, this will give you a base CI workflow, pull request template, and anything specific to the platform that might be of use. The default value is `None`.

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
