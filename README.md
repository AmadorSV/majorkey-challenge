

# Major Key Challenge 

This is project to fulfill the challenge of MajorKey/Cohesion

This project has been created using the know architecture "CleanArchitecture" and  "CQRS".

## How to run it

To run this project you need to install the .NET 5 sdk and one of the next IDE or Code Editor: 

* [Visual Studio](https://visualstudio.microsoft.com/vs/community/)
* [Visual Studio Code](https://code.visualstudio.com/)
* [Rider](https://www.jetbrains.com/rider/)

After installing one of the IDE you have to open the next file: 

```
major-key.sln
```

Located in the folder:  
```
./src/
```

To run this project you have the option to use a Postgres database or using a in memory database. 
By default the aplication will try to run using a Postgres database. In case you want to use the In Memory database
you'll have to change an option located in the ```appsettings.json``` file wich is located in the folder ```./src/RESTApi/```

You'll have to change
```
"UseInMemory":false
```
for 
```
"UseInMemory":true
```

## Technologies

* [NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
* ASP.NET Core 5
* [Entity Framework Core 5](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [Mapster](https://github.com/MapsterMapper/Mapster)
* [FluentValidation](https://fluentvalidation.net/)
* [Docker](https://www.docker.com/)


