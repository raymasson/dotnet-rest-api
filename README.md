# dotnet-rest-api

REST API in .Net Core 

## Resources

https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-3.1&tabs=visual-studio-code

https://jeremylindsayni.wordpress.com/2019/09/09/healthcheck-endpoints-in-c-in-mvc-projects-using-asp-net-core-and-writing-results-to-azure-application-insights/

https://github.com/SoftwareDeveloperBlog/Mssql-docker-initialization-demo

https://zelig880.com/how-to-setup-sql-and-create-tables-with-entity-framework-using-net-core

https://code-maze.com/filtering-aspnet-core-webapi/

https://code-maze.com/sorting-aspnet-core-webapi/

https://code-maze.com/aspnetcore-webapi-best-practices/

https://www.codemag.com/Article/1607041/Simplest-Thing-Possible-Dynamic-Lambda-Expressions

https://code-maze.com/unit-testing-aspnetcore-web-api/

https://code-maze.com/global-error-handling-aspnetcore/

## Run local MSSQL DB

``` bash
cd db/mssql
make build-db
make run-db
```

## Run Api

``` bash
make run
```

Service health: `http://localhost:5000/health`

Service swagger (API doc) : `http://localhost:5000/index.html`

## Test

``` bash
make test
```