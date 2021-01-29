
# Universe explorer :milky_way:

A simple console application that queries a SQL database for
some solar facts.

*App.UniverseExplorer/GUI/ & App.Core/Scenes/ contains files for display but have not been implemented yet.*

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

Additional features need to be added or updated, see tasks lists.


# TODO :stars:

- [ ] Create derived types for temperature DTO.
	- Celsius.
	- Fahrenheit.
- [ ] remove hardcode 'SolarSystem' data.
- [ ] Implement custom formatting for displaying DTO data.
- [ ] Use body classification object/enum instead of strings.
- [ ] Add GUI.cs views.
- [X] Add Tests.
- [ ] Add tests for a actual db instead of in memory.
- [ ] Add additional tests for actual data.
- [ ] Implement window injection.
- [ ] Implement async requests.
- [ ] Add tracing & logging.

