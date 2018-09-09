# Flagscript 

The base Flagscript library is a class library used to provide core support for 
Flagscript website management. It is the highest level of promotion for all classes
and interfaces in the framework. 

## Overview

As of the current version, the core functionality is to provide generic data access
functionality and exception bases. 

### Exceptions

The currently available exception hierarchy is:

* Flagscript.FlagscriptException
  * Flagscript.Data.FlagscriptDataException

### Flagscript.Data.Entity Namespace

Provides base classes for EF Core entities with generically typed Ids and view model
tagging interfaces. 

### Flagscript.Data.Repository Namespace

Provides interface and base classes to provide a DAL layer with generics for 
Flagscript.Data.Entity types. 

## Usage

Flagscript is available as a NuGet package:

### NuGet (PM Console)

```bash
PM> Install-Package Flagscript -Version 2.0.0 -Source https://www.myget.org/F/flagscript/api/v3/index.json
```

### NuGet.exe

```bash
> nuget.exe install Flagscript -Version 2.0.0 -Source https://www.myget.org/F/flagscript/api/v3/index.json
```

### .NET CLI

```bash
> dotnet add package Flagscript --version 2.0.0 --source https://www.myget.org/F/flagscript/api/v3/index.json
```

###  .csproj

```xml
<PackageReference Include="Flagscript" Version="2.0.0" />
```

## Contributing

Although contributions for this project are not yet open, please read 
[CONTRIBUTING.md](https://github.com/flagscript/Flagscript/blob/master/CONTRIBUTING.md) 
for details on our code of conduct.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see 
the [tags on this repository](https://github.com/flagscript/Flagscript/releases). 

## Authors

* **Greg Kaestle** - *Initial work* - [Flagscript](https://flagscript.net)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

* Hat tip to [Chris Pratt](https://cpratt.co) for some of the generic repo ideas being expanded upon.