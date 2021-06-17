# TypeCaster

Every now and then developers need to map an entity to another class, with a similar structure, for the sake of decoupling and separation of concerns.
To avoid manual mapping, which is boring and error prone, TypeCaster offers a simple and elegant solution to automatically cast on class type into anohter.

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
