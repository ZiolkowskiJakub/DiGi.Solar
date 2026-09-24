#### [DiGi\.Solar\.ComputeSharp](DiGi.Solar.ComputeSharp.Overview.md 'DiGi\.Solar\.ComputeSharp\.Overview')

## DiGi\.Solar\.ComputeSharp\.Classes Namespace
### Classes

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolver'></a>

## ShadingSolver Class

Provides a solver implementation to calculate shading effects on objects using ComputeSharp for GPU acceleration\.

Derives from the CPU [DiGi\.Solar\.Classes\.ShadingSolver](https://learn.microsoft.com/en-us/dotnet/api/digi.solar.classes.shadingsolver 'DiGi\.Solar\.Classes\.ShadingSolver'), which it falls back to when [ComputeDeviceType](DiGi.Solar.ComputeSharp.Classes.md#DiGi.Solar.ComputeSharp.Classes.ShadingSolver.ComputeDeviceType 'DiGi\.Solar\.ComputeSharp\.Classes\.ShadingSolver\.ComputeDeviceType') is [Default](DiGi.Solar.ComputeSharp.Enums.md#DiGi.Solar.ComputeSharp.Enums.ComputeDeviceType.Default 'DiGi\.Solar\.ComputeSharp\.Enums\.ComputeDeviceType\.Default') and no supported hardware device is available.

```csharp
public class ShadingSolver : DiGi.Solar.Classes.ShadingSolver
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Solar\.Classes\.ShadingSolver](https://learn.microsoft.com/en-us/dotnet/api/digi.solar.classes.shadingsolver 'DiGi\.Solar\.Classes\.ShadingSolver') → ShadingSolver
### Constructors

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolver.ShadingSolver(DiGi.Solar.Classes.ShadingModel,DiGi.Solar.Classes.ShadingSolverOptions)'></a>

## ShadingSolver\(ShadingModel, ShadingSolverOptions\) Constructor

Initializes a new instance of the [ShadingSolver](DiGi.Solar.ComputeSharp.Classes.md#DiGi.Solar.ComputeSharp.Classes.ShadingSolver 'DiGi\.Solar\.ComputeSharp\.Classes\.ShadingSolver') class with the specified shading model and options\.

```csharp
public ShadingSolver(DiGi.Solar.Classes.ShadingModel? shadingModel, DiGi.Solar.Classes.ShadingSolverOptions? shadingSolverOptions);
```
#### Parameters

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolver.ShadingSolver(DiGi.Solar.Classes.ShadingModel,DiGi.Solar.Classes.ShadingSolverOptions).shadingModel'></a>

`shadingModel` [DiGi\.Solar\.Classes\.ShadingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.solar.classes.shadingmodel 'DiGi\.Solar\.Classes\.ShadingModel')

The shading model to be used for calculations\.

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolver.ShadingSolver(DiGi.Solar.Classes.ShadingModel,DiGi.Solar.Classes.ShadingSolverOptions).shadingSolverOptions'></a>

`shadingSolverOptions` [DiGi\.Solar\.Classes\.ShadingSolverOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.solar.classes.shadingsolveroptions 'DiGi\.Solar\.Classes\.ShadingSolverOptions')

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

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolver.ComputeDeviceType'></a>

## ShadingSolver\.ComputeDeviceType Property

Gets or sets the compute device the shaders run on\. Defaults to [Default](DiGi.Solar.ComputeSharp.Enums.md#DiGi.Solar.ComputeSharp.Enums.ComputeDeviceType.Default 'DiGi\.Solar\.ComputeSharp\.Enums\.ComputeDeviceType\.Default')\.

```csharp
public DiGi.Solar.ComputeSharp.Enums.ComputeDeviceType ComputeDeviceType { get; set; }
```

#### Property Value
[ComputeDeviceType](DiGi.Solar.ComputeSharp.Enums.md#DiGi.Solar.ComputeSharp.Enums.ComputeDeviceType 'DiGi\.Solar\.ComputeSharp\.Enums\.ComputeDeviceType')
### Methods

<a name='DiGi.Solar.ComputeSharp.Classes.ShadingSolver.Solve()'></a>

## ShadingSolver\.Solve\(\) Method

Executes the shading calculation process, utilizing GPU shaders to determine intersections and project shading results onto objects\.

Every receiver receives one result per daytime timestamp, including fully sunlit ones (shaded area 0).

If the merge of one receiver's shadows fails, the unmerged shadows are clipped to the receiver and used instead, capped at its area: that sample's shaded area is then overstated at worst, but it never exceeds the receiver and never reads as full sun. A sample with no receiver face to cap against gets no result at all, so TryGetShadingFactor returns false for it.

When no supported device matching [ComputeDeviceType](DiGi.Solar.ComputeSharp.Classes.md#DiGi.Solar.ComputeSharp.Classes.ShadingSolver.ComputeDeviceType 'DiGi\.Solar\.ComputeSharp\.Classes\.ShadingSolver\.ComputeDeviceType') can be created (see [GraphicsDevice\(this ComputeDeviceType\)](DiGi.Solar.ComputeSharp.md#DiGi.Solar.ComputeSharp.Create.GraphicsDevice(thisDiGi.Solar.ComputeSharp.Enums.ComputeDeviceType) 'DiGi\.Solar\.ComputeSharp\.Create\.GraphicsDevice\(this DiGi\.Solar\.ComputeSharp\.Enums\.ComputeDeviceType\)')), [Default](DiGi.Solar.ComputeSharp.Enums.md#DiGi.Solar.ComputeSharp.Enums.ComputeDeviceType.Default 'DiGi\.Solar\.ComputeSharp\.Enums\.ComputeDeviceType\.Default') falls back to the CPU solve of the base class,
            while an explicit [Hardware](DiGi.Solar.ComputeSharp.Enums.md#DiGi.Solar.ComputeSharp.Enums.ComputeDeviceType.Hardware 'DiGi\.Solar\.ComputeSharp\.Enums\.ComputeDeviceType\.Hardware') request returns false. The obsolete `ComputeDeviceType.Software` (WARP) never gets a device, so it always returns false (ZiolkowskiJakub/DiGi.Solar#10).

```csharp
public override bool Solve();
```

Implements [Solve\(\)](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.isolver.solve 'DiGi\.Core\.Interfaces\.ISolver\.Solve')

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True if the solving operation completed successfully; otherwise, false\.