# Migrations Project

This project contains all the migrations for the legacy database.

## Getting Started


## Commands

All below commands must be run from the **root folder of the migrations project**.

```shell
cd src\Infrastructure\Migrations
```

### How to run migrations for both databases (run project)
If you run the project and provide `migrate` as the first argument, it will run all migrations for both databases.
```shell
dotnet run migrate
```

### How to run migrations for the app database (update database)
```shell
dotnet ef database update -c FundamentalDbContext
```

### How to create a new migration for the app database
```shell
dotnet ef migrations add NewMigration -c FundamentalDbContext -o Fundamental
```
