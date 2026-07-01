#### [DiGi\.Solar\.ComputeSharp](index.md 'index')

## DiGi\.Solar\.ComputeSharp\.Classes Namespace
### Classes

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolver'></a>

## ShadingSolver Class

Provides a solver implementation to calculate shading effects on objects using ComputeSharp for GPU acceleration\.

```csharp
public class ShadingSolver : DiGi.Solar.Interfaces.IShadingObject, DiGi.Solar.Interfaces.ISolarObject, DiGi.Core.Interfaces.IObject, DiGi.Core.Interfaces.ISolver, DiGi.Core.Interfaces.IEvaluator
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → ShadingSolver

Implements [DiGi\.Solar\.Interfaces\.IShadingObject](https://learn.microsoft.com/en-us/dotnet/api/digi.solar.interfaces.ishadingobject 'DiGi\.Solar\.Interfaces\.IShadingObject'), [DiGi\.Solar\.Interfaces\.ISolarObject](https://learn.microsoft.com/en-us/dotnet/api/digi.solar.interfaces.isolarobject 'DiGi\.Solar\.Interfaces\.ISolarObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Core\.Interfaces\.ISolver](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.isolver 'DiGi\.Core\.Interfaces\.ISolver'), [DiGi\.Core\.Interfaces\.IEvaluator](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.ievaluator 'DiGi\.Core\.Interfaces\.IEvaluator')
### Constructors

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolver.ShadingSolver(DiGi.Solar.Classes.ShadingModel,DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions)'></a>

## ShadingSolver\(ShadingModel, ShadingSolverOptions\) Constructor

Initializes a new instance of the [ShadingSolver](DiGi.Solar.ComputeSharp.Classes.md#DiGi.Solar.ComputeSharp.Classes.ShadingSolver 'DiGi\.Solar\.ComputeSharp\.Classes\.ShadingSolver') class with the specified shading model and options\.

```csharp
public ShadingSolver(DiGi.Solar.Classes.ShadingModel? shadingModel, DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions? shadingSolverOptions);
```
#### Parameters

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolver.ShadingSolver(DiGi.Solar.Classes.ShadingModel,DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions).shadingModel'></a>

`shadingModel` [DiGi\.Solar\.Classes\.ShadingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.solar.classes.shadingmodel 'DiGi\.Solar\.Classes\.ShadingModel')

The shading model to be used for calculations\.

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolver.ShadingSolver(DiGi.Solar.Classes.ShadingModel,DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions).shadingSolverOptions'></a>

`shadingSolverOptions` [ShadingSolverOptions](DiGi.Solar.ComputeSharp.Classes.md#DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions 'DiGi\.Solar\.ComputeSharp\.Classes\.ShadingSolverOptions')

The options that configure the solver's behavior\.

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolver.ShadingSolver(DiGi.Solar.Classes.ShadingModel,System.DateTime[])'></a>

## ShadingSolver\(ShadingModel, DateTime\[\]\) Constructor

Initializes a new instance of the [ShadingSolver](DiGi.Solar.ComputeSharp.Classes.md#DiGi.Solar.ComputeSharp.Classes.ShadingSolver 'DiGi\.Solar\.ComputeSharp\.Classes\.ShadingSolver') class with the specified shading model and a collection of date\-times\.

```csharp
public ShadingSolver(DiGi.Solar.Classes.ShadingModel? shadingModel, System.DateTime[]? dateTimes);
```
#### Parameters

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolver.ShadingSolver(DiGi.Solar.Classes.ShadingModel,System.DateTime[]).shadingModel'></a>

`shadingModel` [DiGi\.Solar\.Classes\.ShadingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.solar.classes.shadingmodel 'DiGi\.Solar\.Classes\.ShadingModel')

The shading model to be used for calculations\.

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolver.ShadingSolver(DiGi.Solar.Classes.ShadingModel,System.DateTime[]).dateTimes'></a>

`dateTimes` [System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

An array of date\-time values for which shading should be calculated\.
### Properties

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolver.ShadingModel'></a>

## ShadingSolver\.ShadingModel Property

Gets or sets the [ShadingModel](DiGi.Solar.ComputeSharp.Classes.md#DiGi.Solar.ComputeSharp.Classes.ShadingSolver.ShadingModel 'DiGi\.Solar\.ComputeSharp\.Classes\.ShadingSolver\.ShadingModel') associated with this solver\.

```csharp
public DiGi.Solar.Classes.ShadingModel? ShadingModel { get; set; }
```

#### Property Value
[DiGi\.Solar\.Classes\.ShadingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.solar.classes.shadingmodel 'DiGi\.Solar\.Classes\.ShadingModel')

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolver.ShadingSolverOptions'></a>

## ShadingSolver\.ShadingSolverOptions Property

Gets or sets the [ShadingSolverOptions](DiGi.Solar.ComputeSharp.Classes.md#DiGi.Solar.ComputeSharp.Classes.ShadingSolver.ShadingSolverOptions 'DiGi\.Solar\.ComputeSharp\.Classes\.ShadingSolver\.ShadingSolverOptions') that define the parameters for the solving process\.

```csharp
public DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions? ShadingSolverOptions { get; set; }
```

#### Property Value
[ShadingSolverOptions](DiGi.Solar.ComputeSharp.Classes.md#DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions 'DiGi\.Solar\.ComputeSharp\.Classes\.ShadingSolverOptions')
### Methods

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolver.Solve()'></a>

## ShadingSolver\.Solve\(\) Method

Executes the shading calculation process, utilizing GPU shaders to determine intersections and project shading results onto objects\.

```csharp
public bool Solve();
```

Implements [Solve\(\)](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.isolver.solve 'DiGi\.Core\.Interfaces\.ISolver\.Solve')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True if the solving operation completed successfully; otherwise, false\.

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions'></a>

## ShadingSolverOptions Class

Provides configuration options for the shading solver, including tolerances and time series settings\.

```csharp
public class ShadingSolverOptions : DiGi.Core.Classes.SerializableObject, DiGi.Solar.Interfaces.IShadingSerializableObject, DiGi.Solar.Interfaces.IShadingObject, DiGi.Solar.Interfaces.ISolarObject, DiGi.Core.Interfaces.IObject, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject, DiGi.Core.Interfaces.IOptions
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → ShadingSolverOptions

Implements [DiGi\.Solar\.Interfaces\.IShadingSerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.solar.interfaces.ishadingserializableobject 'DiGi\.Solar\.Interfaces\.IShadingSerializableObject'), [DiGi\.Solar\.Interfaces\.IShadingObject](https://learn.microsoft.com/en-us/dotnet/api/digi.solar.interfaces.ishadingobject 'DiGi\.Solar\.Interfaces\.IShadingObject'), [DiGi\.Solar\.Interfaces\.ISolarObject](https://learn.microsoft.com/en-us/dotnet/api/digi.solar.interfaces.isolarobject 'DiGi\.Solar\.Interfaces\.ISolarObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject'), [DiGi\.Core\.Interfaces\.IOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.ioptions 'DiGi\.Core\.Interfaces\.IOptions')
### Constructors

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions.ShadingSolverOptions()'></a>

## ShadingSolverOptions\(\) Constructor

Initializes a new instance of the [ShadingSolverOptions](DiGi.Solar.ComputeSharp.Classes.md#DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions 'DiGi\.Solar\.ComputeSharp\.Classes\.ShadingSolverOptions') class with default values\.

```csharp
public ShadingSolverOptions();
```

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions.ShadingSolverOptions(DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions)'></a>

## ShadingSolverOptions\(ShadingSolverOptions\) Constructor

Initializes a new instance of the [ShadingSolverOptions](DiGi.Solar.ComputeSharp.Classes.md#DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions 'DiGi\.Solar\.ComputeSharp\.Classes\.ShadingSolverOptions') class by copying values from an existing options instance\.

```csharp
public ShadingSolverOptions(DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions? shadingSolverOptions);
```
#### Parameters

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions.ShadingSolverOptions(DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions).shadingSolverOptions'></a>

`shadingSolverOptions` [ShadingSolverOptions](DiGi.Solar.ComputeSharp.Classes.md#DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions 'DiGi\.Solar\.ComputeSharp\.Classes\.ShadingSolverOptions')

The source shading solver options to copy from\.

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions.ShadingSolverOptions(System.Text.Json.Nodes.JsonObject)'></a>

## ShadingSolverOptions\(JsonObject\) Constructor

Initializes a new instance of the [ShadingSolverOptions](DiGi.Solar.ComputeSharp.Classes.md#DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions 'DiGi\.Solar\.ComputeSharp\.Classes\.ShadingSolverOptions') class using a JSON object\.

```csharp
public ShadingSolverOptions(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions.ShadingSolverOptions(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JSON object containing the options data\.
### Properties

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions.AngleTolerance'></a>

## ShadingSolverOptions\.AngleTolerance Property

Gets or sets the angle tolerance used by the shading solver\.

```csharp
public double AngleTolerance { get; set; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions.ShadingSolverType'></a>

## ShadingSolverOptions\.ShadingSolverType Property

Gets or sets the type of shading solver to be employed\.

```csharp
public DiGi.Solar.Enums.ShadingSolverType ShadingSolverType { get; set; }
```

#### Property Value
[DiGi\.Solar\.Enums\.ShadingSolverType](https://learn.microsoft.com/en-us/dotnet/api/digi.solar.enums.shadingsolvertype 'DiGi\.Solar\.Enums\.ShadingSolverType')

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions.TimeSeries'></a>

## ShadingSolverOptions\.TimeSeries Property

Gets or sets the time series used for shading calculations\.

```csharp
public DiGi.Core.Interfaces.ITimeSeries TimeSeries { get; set; }
```

#### Property Value
[DiGi\.Core\.Interfaces\.ITimeSeries](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.itimeseries 'DiGi\.Core\.Interfaces\.ITimeSeries')

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolverOptions.Tolerance'></a>

## ShadingSolverOptions\.Tolerance Property

Gets or sets the distance tolerance used by the shading solver\.

```csharp
public double Tolerance { get; set; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')