# Dotnet Language Extensions (>= v3.0.0)  

The core flagscript library also includes extensions to core dotnet constructs.  

## Using Dotnet Language Extensions   

Flagscript dotnet extensions are in the namespace Flagscript.Extensions.Dotnet.   

```csharp
using Flagscript.Extensions.Dotnet
```

## Enum Extensions  

The following extensions are available for the enum keyword.  

### GetEnumDescription()  

The **_GetEnumDescription()+** extension will provide the description of an enum member marked with the **_System.ComponentModel.DescriptionAttribute_** decorator.  

If no attribute is present, the name of the enum member itself will be returned.  

```csharp

public enum TestEnum
{

  ValueOne,

  [Description("TwoValue")]
  ValueTwo

}

string valueOne = TestEnum.ValueOne.GetEnumDescription();
string valueTwo = TestEnum.ValueTwo.GetEnumDescription();

// valueOne == "ValueOne";
// valueTwo == "TwoValue";

```
