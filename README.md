[![Nuget](https://img.shields.io/nuget/v/TypeCaster)](https://www.nuget.org/packages/TypeCaster/)
![Nuget](https://img.shields.io/nuget/dt/TypeCaster)

![GitHub Workflow Status](https://img.shields.io/github/workflow/status/GuzzoLM/TypeCaster/Tests)
![GitHub Workflow Status](https://img.shields.io/github/workflow/status/GuzzoLM/TypeCaster/Main%20Branch)

# TypeCaster

Every now and then developers need to map an entity to another class, with a similar structure, for the sake of decoupling and separation of concerns.
To avoid manual mapping, which is boring and error prone, TypeCaster offers a simple and elegant solution to automatically cast on class type into anohter.

## Installation

TypeCaster runs on Windows, Linux, and macOS using [.NET Core](https://github.com/dotnet/core).

You can install the TypeCaster NuGet package from the .NET Core CLI using:
```
dotnet add package TypeCaster
```

or from the NuGet package manager:
```
Install-Package TypeCaster
```

Or alternatively, you can add the TypeCaster package from within Visual Studio's NuGet package manager or via [Paket](https://github.com/fsprojects/Paket).

## Usage

Imagine a class with the following properties

```csharp
public class TestClassA
{
  public int Id { get; set; }
  public string Name { get; set; }
  public MyEnum Type { get; set; }
}
```

And another claass with a similar structure, but with a string intead of enum in the Type property

```csharp
public class TestClassB
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Type { get; set; }
}
```

Casting a clas of type ClassA to type ClassB is pretty simple

```csharp
public void ConvertClassAToClassB()
{
  var classA = new TestClassA
  {
    Id = 1,
    Name = "MyClass",
    Type = MyEnum.TypeA
  };
  
  var classB = CastToClass<TestClassB>.From(classA);
}
```

The other way around works too

```csharp
public void ConvertClassBToClassA()
{
  var classB = new TestClassB
  {
    Id = 1,
    Name = "MyClass",
    Type = "TypeA"
  };
  
  var classA = CastToClass<TestClassA>.From(classB);
}
```

Since TypeCaster relies on the class property names, it can do some simple conversions to preserve the class tructure. When a conversion is not possible, the property is set to its type default value.

### The CastTo class
TypeCaster relies on two main classes. The CastToClass which was used in last example, and CastTo class.
CastTo is responsible for type conversion of properties, and can be used directly in your code, like in the following example.

```csharp
//Convert string into enum
var typeString = "TypeA";
var type = CastTo<TestEnum>.From(typeString);

//Convert int into double
var myInt = 1;
var myDouble = CastTo<double>.From(myInt);
```

## Benchmark

TypeCaster comes with a cost in application performance. It's not a big impact, but is significantly slower than both Manual mappings and AutoMapper and this should be taken in consideration when using this feature in your project.
The following table shows the difference in performance between these methods.


|     Method |        Mean |     Error |    StdDev |
|----------- |------------:|----------:|----------:|
| TypeCaster | 7,762.83 ns | 69.038 ns | 67.804 ns |
|  ManualMap |    53.97 ns |  1.076 ns |  0.954 ns |
|    AutoMap |   160.26 ns |  2.939 ns |  2.294 ns |
