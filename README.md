
# Universe explorer :milky_way:

A simple console application that queries a SQL database for
some solar facts.

Additional features need to be added or updated, see todo list.

**Main branch uses simple print readline statements, on branch 'Develop'
there are some CLI gui files and an interface for window injection.
Not fully implemented yet.**


Migrations
===
Set connectionstring in 'appsettings.json' in the 'DevelopUniverseDb' section. Appsettings file is located in 'App.Core'
To run migrations apply the following commands from the service folder 'UniverseExplorer\Services.UniverseEf\'

Create migration:
```
dotnet ef --startup-project ..\App.Core\App.Core.csproj migrations add UniverseMigration
```

Apply migration:
```
dotnet ef --startup-project ..\App.Core\App.Core.csproj database update UniverseMigration
```



# TODO :stars:

- [ ] Create derived types for temperature DTO.
	- Celsius.
	- Fahrenheit.
- [ ] Implement IConfiguration usage for seperate 'Appconfig' files.
	- Remove remaining duplicate connectionstrings.
	- Move credentials to ENV.
- [ ] Remove hardcode 'SolarSystem' data.
- [ ] Update model serializer.
- [ ] Remove temp tokenizer for serializer again.
- [ ] Implement custom formatting for displaying DTO data.
- [ ] Use body classification object/enum instead of strings.
- [ ] Add GUI.cs views.
- [X] Add Tests.
- [ ] Add tests for a actual db instead of in memory.
- [ ] Add additional tests for actual data.
- [ ] Implement window injection.
- [ ] Implement async requests.
- [ ] Add tracing & logging.

