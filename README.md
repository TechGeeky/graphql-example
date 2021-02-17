This repo is exact replica from here - https://github.com/graphql-dotnet/examples/.
I am just using "AspNetCore" and "StarWars" folder. I also copied "GraphQLMiddleware.cs" file from "AspNetCoreCustom" folder into "AspNetCore" one in this repo as I needed that middleware for my project.

Navigate to AspNetCore folder and then execute - 

    ./run.sh

Once server starts up, you will see this line getting printed out on console - 

```
Inside CustomerServiceImpl
In
Hello World: abc.json
Inside CatalogServiceImpl
```

In the above logs `In` and `Hello World: abc.json` lines are getting printed out from `DataImpl` class during server startup. Meaning it is getting initialized during startup for the first time which is fine. So I don't expect it to be called again whenever I make GraphQL request atleast that's what my understanding is.

Now after that if I open `GraphQL UI` playground url (http://localhost:3000/ui/playground) for the first time then the same logs gets printed out on console again. I am not sure why. And if I hit same `GraphQL UI` playground url again for second time then above logs isn't getting printed out. This happens only on first GraphQL request after server startup.

It looks like I am missing something very minor here. Is it because I have `ICatalogService catalogService, ICustomerService customerService` in the `StarWarsQuery` constructor? I need to use `ICatalogService` and `ICustomerService` in my `StarsWarQuery` class because I need to get data from them.

```
Legacy code base:
BaseMiddleware
HealthServiceMiddleware
DependencyBootstrap
CatalogServiceImpl
ICatalogService
CustomerServiceImpl
ICustomerService
DataImpl
IData
```

All other classes related to `GraphQL` are new code which I am trying to use to migrate my use case one by one.
