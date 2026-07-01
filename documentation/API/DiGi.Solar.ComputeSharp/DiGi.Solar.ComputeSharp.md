#### [DiGi\.Solar\.ComputeSharp](index.md 'index')

## DiGi\.Solar\.ComputeSharp Namespace
### Classes

<a name='DiGi.Solar.ComputeSharp.Create'></a>

## Create Class

```csharp
public static class Create
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Create
### Methods

<a name='DiGi.Solar.ComputeSharp.Create.ShadingSolverResult(thisDiGi.Solar.Enums.ShadingSolverType,System.DateTime,DiGi.Geometry.Spatial.Classes.Plane,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Interfaces.IPolygonalFace2D_)'></a>

## Create\.ShadingSolverResult\(this ShadingSolverType, DateTime, Plane, IEnumerable\<IPolygonalFace2D\>\) Method

Creates an [DiGi\.Solar\.Interfaces\.IShadingSolverResult](https://learn.microsoft.com/en-us/dotnet/api/digi.solar.interfaces.ishadingsolverresult 'DiGi\.Solar\.Interfaces\.IShadingSolverResult') based on the specified [DiGi\.Solar\.Enums\.ShadingSolverType](https://learn.microsoft.com/en-us/dotnet/api/digi.solar.enums.shadingsolvertype 'DiGi\.Solar\.Enums\.ShadingSolverType'), date and time, plane, and polygonal faces\.

```csharp
public static DiGi.Solar.Interfaces.IShadingSolverResult? ShadingSolverResult(this DiGi.Solar.Enums.ShadingSolverType shadingSolverType, System.DateTime dateTime, DiGi.Geometry.Spatial.Classes.Plane? plane, System.Collections.Generic.IEnumerable<DiGi.Geometry.Planar.Interfaces.IPolygonalFace2D>? polygonalFace2Ds);
```
#### Parameters

<a name='DiGi.Solar.ComputeSharp.Create.ShadingSolverResult(thisDiGi.Solar.Enums.ShadingSolverType,System.DateTime,DiGi.Geometry.Spatial.Classes.Plane,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Interfaces.IPolygonalFace2D_).shadingSolverType'></a>

`shadingSolverType` [DiGi\.Solar\.Enums\.ShadingSolverType](https://learn.microsoft.com/en-us/dotnet/api/digi.solar.enums.shadingsolvertype 'DiGi\.Solar\.Enums\.ShadingSolverType')

The [DiGi\.Solar\.Enums\.ShadingSolverType](https://learn.microsoft.com/en-us/dotnet/api/digi.solar.enums.shadingsolvertype 'DiGi\.Solar\.Enums\.ShadingSolverType') that determines the type of shading solver result to be created\.

<a name='DiGi.Solar.ComputeSharp.Create.ShadingSolverResult(thisDiGi.Solar.Enums.ShadingSolverType,System.DateTime,DiGi.Geometry.Spatial.Classes.Plane,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Interfaces.IPolygonalFace2D_).dateTime'></a>

`dateTime` [System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime')

The [System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime') representing the time for which the shading is solved\.

<a name='DiGi.Solar.ComputeSharp.Create.ShadingSolverResult(thisDiGi.Solar.Enums.ShadingSolverType,System.DateTime,DiGi.Geometry.Spatial.Classes.Plane,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Interfaces.IPolygonalFace2D_).plane'></a>

`plane` [DiGi\.Geometry\.Spatial\.Classes\.Plane](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.plane 'DiGi\.Geometry\.Spatial\.Classes\.Plane')

The [DiGi\.Geometry\.Spatial\.Classes\.Plane](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.plane 'DiGi\.Geometry\.Spatial\.Classes\.Plane') used for geometrical shading calculations\. This can be null if a numerical solver is used\.

<a name='DiGi.Solar.ComputeSharp.Create.ShadingSolverResult(thisDiGi.Solar.Enums.ShadingSolverType,System.DateTime,DiGi.Geometry.Spatial.Classes.Plane,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Interfaces.IPolygonalFace2D_).polygonalFace2Ds'></a>

`polygonalFace2Ds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.Geometry\.Planar\.Interfaces\.IPolygonalFace2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.interfaces.ipolygonalface2d 'DiGi\.Geometry\.Planar\.Interfaces\.IPolygonalFace2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An [System\.Collections\.Generic\.IEnumerable&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1') representing the faces involved in shading calculations\.

#### Returns
[DiGi\.Solar\.Interfaces\.IShadingSolverResult](https://learn.microsoft.com/en-us/dotnet/api/digi.solar.interfaces.ishadingsolverresult 'DiGi\.Solar\.Interfaces\.IShadingSolverResult')  
An implementation of [DiGi\.Solar\.Interfaces\.IShadingSolverResult](https://learn.microsoft.com/en-us/dotnet/api/digi.solar.interfaces.ishadingsolverresult 'DiGi\.Solar\.Interfaces\.IShadingSolverResult') if a valid solver type is provided and necessary parameters are present; otherwise, null\.