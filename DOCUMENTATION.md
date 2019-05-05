# Flagscript Documentation (>= v3.2.0)

Thanks for using Flagscript! If you have any quesitons, feel free to post a question on the [issues board](../../issues). 

## Contents

1. Exceptions
   - Contains information on exceptions in the Flagscript libraries.
2. Simple Memory Cache
   - Flagscript Typed Memory Cache

## Exceptions

The following exceptions can be thrown by Flagscript libraries. Handling these exceptions allows you process exceptions specific to the Flagscript framework.

### Base Exception

The exception class **_FlagscriptException_** is at the root of hierarchy for all exceptions thrown in the Flagscript framework. You may catch this exception to distinguish Flagscript exceptions from dotnet or other library exceptions.

A short example of distinguishing all Flagscript Exceptions:

```csharp
using Flagscript;

try
{
  // Code using Flagscript libraries.
}
catch (FlagscriptException fe)
{
}
```

### Configuration Exception

The exception class **_FlagscriptConfigurationException_** is thrown when there is an error with a Flagscript framework configuration. You may catch this exception to distinguish Flagscript configuraiton exceptions from other Flagscript exceptions.

A short example of handling Flagscript configuration exceptions:

```csharp
using Flagscript;

try
{
  // Code using Flagscript libraries.
}
catch (FlagscriptConfigurationException fce)
{
}
```

## Simple Memory Cache

A Sync and Async version of a simple memory cache are available for use. 

### Usage

A typed cache can be set up by extending `SimpleSyncMemoryCacheBase` or `SimpleAsyncMemoryCacheBaseTest`. The base should be chosen based upon whether the create method for the cached object is synchronous or asynchronous. The examples below will use the sychronous method for the sake of brevity.

```csharp
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

public class SyncMemoryCache : SimpleSyncMemoryCacheBase<StringBuilder>
{

	protected SyncMemoryCache(MemoryCacheOptions memoryCacheOptions, ILogger logger) : base(memoryCacheOptions, logger)
	{
	}	

	public override object GenerateCacheKey(object identifier) => identifier;

}
```

The cache can then be put into th eDI framework, and used with the `GetOrCreate` or `GetOrCreateAsync` methods in the corresponding sync and async interfaces.
