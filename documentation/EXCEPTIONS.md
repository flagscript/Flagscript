# Exceptions (>= v3.0.0)  

The following explains the use of Flagscript exceptions in v3.0.0.  

## Base Exception

The exception class **_FlagscriptException_** is at the root of hierarchy for all exceptions thrown in the Flagscript framework. You may catch this exception to distinguish Flagscript exceptions from dotnet or other library exceptions.  

**_FlagscriptException_** is in the Flagscript namespace. The following is a short example of distinguishing Flagscript Exceptions.   

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
