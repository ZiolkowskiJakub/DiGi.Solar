#### [DiGi\.Solar](DiGi.Solar.Overview.md 'DiGi\.Solar\.Overview')

## DiGi\.Solar Namespace
### Classes

<a name='DiGi.Solar.Create'></a>

## Create Class

```csharp
public static class Create
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Create
### Methods

<a name='DiGi.Solar.Create.IrradianceResult(thisDiGi.Geometry.Spatial.Classes.Vector3D,DiGi.Geometry.Spatial.Classes.Vector3D,double,double,double,double)'></a>

## Create\.IrradianceResult\(this Vector3D, Vector3D, double, double, double, double\) Method

Computes the three irradiance components incident on a surface for a single hour, using the Liu and Jordan isotropic sky model\.

The direct beam component is the direct normal radiation projected onto the surface normal, and is zero when the sun is behind the surface. The sky diffuse component uses the isotropic sky view factor, and the ground-reflected component uses the complementary ground view factor.

The tilt of the surface is taken from [surfaceNormal](DiGi.Solar.md#DiGi.Solar.Create.IrradianceResult(thisDiGi.Geometry.Spatial.Classes.Vector3D,DiGi.Geometry.Spatial.Classes.Vector3D,double,double,double,double).surfaceNormal 'DiGi\.Solar\.Create\.IrradianceResult\(this DiGi\.Geometry\.Spatial\.Classes\.Vector3D, DiGi\.Geometry\.Spatial\.Classes\.Vector3D, double, double, double, double\)\.surfaceNormal') and is not supplied separately, so the two can never disagree. Tilts beyond 90 degrees are valid and describe a downward-facing surface, which sees more ground than sky.

[sunDirection](DiGi.Solar.md#DiGi.Solar.Create.IrradianceResult(thisDiGi.Geometry.Spatial.Classes.Vector3D,DiGi.Geometry.Spatial.Classes.Vector3D,double,double,double,double).sunDirection 'DiGi\.Solar\.Create\.IrradianceResult\(this DiGi\.Geometry\.Spatial\.Classes\.Vector3D, DiGi\.Geometry\.Spatial\.Classes\.Vector3D, double, double, double, double\)\.sunDirection') is the sun ray propagation direction as returned by [SunDirection\(this SolarTimes\)](DiGi.Solar.md#DiGi.Solar.Query.SunDirection(thisSolarTimes) 'DiGi\.Solar\.Query\.SunDirection\(this SolarTimes\)'), which points away from the sun and therefore downwards while the sun is above the horizon.

```csharp
public static DiGi.Solar.Classes.IrradianceResult? IrradianceResult(this DiGi.Geometry.Spatial.Classes.Vector3D? surfaceNormal, DiGi.Geometry.Spatial.Classes.Vector3D? sunDirection, double globalHorizontalRadiation, double directNormalRadiation, double diffuseHorizontalRadiation, double albedo);
```
#### Parameters

<a name='DiGi.Solar.Create.IrradianceResult(thisDiGi.Geometry.Spatial.Classes.Vector3D,DiGi.Geometry.Spatial.Classes.Vector3D,double,double,double,double).surfaceNormal'></a>

`surfaceNormal` [DiGi\.Geometry\.Spatial\.Classes\.Vector3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.vector3d 'DiGi\.Geometry\.Spatial\.Classes\.Vector3D')

The outward normal of the surface\. Need not be unit length\.

<a name='DiGi.Solar.Create.IrradianceResult(thisDiGi.Geometry.Spatial.Classes.Vector3D,DiGi.Geometry.Spatial.Classes.Vector3D,double,double,double,double).sunDirection'></a>

`sunDirection` [DiGi\.Geometry\.Spatial\.Classes\.Vector3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.vector3d 'DiGi\.Geometry\.Spatial\.Classes\.Vector3D')

The sun ray propagation direction, pointing away from the sun\. Need not be unit length\.

<a name='DiGi.Solar.Create.IrradianceResult(thisDiGi.Geometry.Spatial.Classes.Vector3D,DiGi.Geometry.Spatial.Classes.Vector3D,double,double,double,double).globalHorizontalRadiation'></a>

`globalHorizontalRadiation` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The global horizontal radiation, in W/m2\.

<a name='DiGi.Solar.Create.IrradianceResult(thisDiGi.Geometry.Spatial.Classes.Vector3D,DiGi.Geometry.Spatial.Classes.Vector3D,double,double,double,double).directNormalRadiation'></a>

`directNormalRadiation` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The direct normal radiation, in W/m2\.

<a name='DiGi.Solar.Create.IrradianceResult(thisDiGi.Geometry.Spatial.Classes.Vector3D,DiGi.Geometry.Spatial.Classes.Vector3D,double,double,double,double).diffuseHorizontalRadiation'></a>

`diffuseHorizontalRadiation` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The diffuse horizontal radiation, in W/m2\.

<a name='DiGi.Solar.Create.IrradianceResult(thisDiGi.Geometry.Spatial.Classes.Vector3D,DiGi.Geometry.Spatial.Classes.Vector3D,double,double,double,double).albedo'></a>

`albedo` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The ground reflectance as a decimal fraction, as resolved by [Albedo\(Nullable&lt;double&gt;, bool\)](DiGi.Solar.md#DiGi.Solar.Query.Albedo(System.Nullable_double_,bool) 'DiGi\.Solar\.Query\.Albedo\(System\.Nullable\<double\>, bool\)')\.

#### Returns
[IrradianceResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.IrradianceResult 'DiGi\.Solar\.Classes\.IrradianceResult')  
A [IrradianceResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.IrradianceResult 'DiGi\.Solar\.Classes\.IrradianceResult') carrying the three components and the ground reflectance used, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') if either vector is null or has no length, or if any radiation value is not a number or is negative, or if the ground reflectance is not a number or lies outside the range 0 to 1\.

<a name='DiGi.Solar.Create.ShadingSolverResult(thisDiGi.Solar.Enums.ShadingSolverType,System.DateTime,DiGi.Geometry.Spatial.Classes.Plane,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Interfaces.IPolygonalFace2D_)'></a>

## Create\.ShadingSolverResult\(this ShadingSolverType, DateTime, Plane, IEnumerable\<IPolygonalFace2D\>\) Method

Creates an [IShadingSolverResult](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSolverResult 'DiGi\.Solar\.Interfaces\.IShadingSolverResult') based on the specified [ShadingSolverType](DiGi.Solar.Enums.md#DiGi.Solar.Enums.ShadingSolverType 'DiGi\.Solar\.Enums\.ShadingSolverType'), date and time, plane, and polygonal faces\.

```csharp
public static DiGi.Solar.Interfaces.IShadingSolverResult? ShadingSolverResult(this DiGi.Solar.Enums.ShadingSolverType shadingSolverType, System.DateTime dateTime, DiGi.Geometry.Spatial.Classes.Plane? plane, System.Collections.Generic.IEnumerable<DiGi.Geometry.Planar.Interfaces.IPolygonalFace2D>? polygonalFace2Ds);
```
#### Parameters

<a name='DiGi.Solar.Create.ShadingSolverResult(thisDiGi.Solar.Enums.ShadingSolverType,System.DateTime,DiGi.Geometry.Spatial.Classes.Plane,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Interfaces.IPolygonalFace2D_).shadingSolverType'></a>

`shadingSolverType` [ShadingSolverType](DiGi.Solar.Enums.md#DiGi.Solar.Enums.ShadingSolverType 'DiGi\.Solar\.Enums\.ShadingSolverType')

The [ShadingSolverType](DiGi.Solar.Enums.md#DiGi.Solar.Enums.ShadingSolverType 'DiGi\.Solar\.Enums\.ShadingSolverType') that determines the type of shading solver result to be created\.

<a name='DiGi.Solar.Create.ShadingSolverResult(thisDiGi.Solar.Enums.ShadingSolverType,System.DateTime,DiGi.Geometry.Spatial.Classes.Plane,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Interfaces.IPolygonalFace2D_).dateTime'></a>

`dateTime` [System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime')

The [System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime') representing the time for which the shading is solved\.

<a name='DiGi.Solar.Create.ShadingSolverResult(thisDiGi.Solar.Enums.ShadingSolverType,System.DateTime,DiGi.Geometry.Spatial.Classes.Plane,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Interfaces.IPolygonalFace2D_).plane'></a>

`plane` [DiGi\.Geometry\.Spatial\.Classes\.Plane](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.plane 'DiGi\.Geometry\.Spatial\.Classes\.Plane')

The [DiGi\.Geometry\.Spatial\.Classes\.Plane](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.plane 'DiGi\.Geometry\.Spatial\.Classes\.Plane') used for geometrical shading calculations\. This can be null if a numerical solver is used\.

<a name='DiGi.Solar.Create.ShadingSolverResult(thisDiGi.Solar.Enums.ShadingSolverType,System.DateTime,DiGi.Geometry.Spatial.Classes.Plane,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Interfaces.IPolygonalFace2D_).polygonalFace2Ds'></a>

`polygonalFace2Ds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.Geometry\.Planar\.Interfaces\.IPolygonalFace2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.interfaces.ipolygonalface2d 'DiGi\.Geometry\.Planar\.Interfaces\.IPolygonalFace2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An [System\.Collections\.Generic\.IEnumerable&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1') representing the faces involved in shading calculations\.

#### Returns
[IShadingSolverResult](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSolverResult 'DiGi\.Solar\.Interfaces\.IShadingSolverResult')  
An implementation of [IShadingSolverResult](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSolverResult 'DiGi\.Solar\.Interfaces\.IShadingSolverResult') if a valid solver type is provided and necessary parameters are present; otherwise, null\.

<a name='DiGi.Solar.Create.SolarPowerResult(thisDiGi.Solar.Classes.IrradianceResult,double,double)'></a>

## Create\.SolarPowerResult\(this IrradianceResult, double, double\) Method

Computes the total solar power incident on a partially shaded surface, in W\.

The shadow blocks the direct beam component only, so the beam component is applied to the unshaded area while the sky diffuse and ground-reflected components are applied to the whole surface. A fully shaded surface therefore still receives ambient light.

Shading solvers in this workspace report the SHADED area rather than the unshaded one. Use [SolarPowerResult\_ByShadingFactor\(this IrradianceResult, double, double\)](DiGi.Solar.md#DiGi.Solar.Create.SolarPowerResult_ByShadingFactor(thisDiGi.Solar.Classes.IrradianceResult,double,double) 'DiGi\.Solar\.Create\.SolarPowerResult\_ByShadingFactor\(this DiGi\.Solar\.Classes\.IrradianceResult, double, double\)') to feed a shading factor straight from [TryGetShadingFactor\(IShadingElement, DateTime, double, bool\)](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingModel.TryGetShadingFactor(DiGi.Solar.Interfaces.IShadingElement,System.DateTime,double,bool) 'DiGi\.Solar\.Classes\.ShadingModel\.TryGetShadingFactor\(DiGi\.Solar\.Interfaces\.IShadingElement, System\.DateTime, double, bool\)') without inverting it by hand.

```csharp
public static DiGi.Solar.Classes.SolarPowerResult? SolarPowerResult(this DiGi.Solar.Classes.IrradianceResult? irradianceResult, double totalArea, double unshadedArea);
```
#### Parameters

<a name='DiGi.Solar.Create.SolarPowerResult(thisDiGi.Solar.Classes.IrradianceResult,double,double).irradianceResult'></a>

`irradianceResult` [IrradianceResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.IrradianceResult 'DiGi\.Solar\.Classes\.IrradianceResult')

The [IrradianceResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.IrradianceResult 'DiGi\.Solar\.Classes\.IrradianceResult') for the hour and surface orientation\.

<a name='DiGi.Solar.Create.SolarPowerResult(thisDiGi.Solar.Classes.IrradianceResult,double,double).totalArea'></a>

`totalArea` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The total area of the surface, in m2\.

<a name='DiGi.Solar.Create.SolarPowerResult(thisDiGi.Solar.Classes.IrradianceResult,double,double).unshadedArea'></a>

`unshadedArea` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The area of the surface in direct sunlight, in m2\. Must not exceed [totalArea](DiGi.Solar.md#DiGi.Solar.Create.SolarPowerResult(thisDiGi.Solar.Classes.IrradianceResult,double,double).totalArea 'DiGi\.Solar\.Create\.SolarPowerResult\(this DiGi\.Solar\.Classes\.IrradianceResult, double, double\)\.totalArea')\.

#### Returns
[SolarPowerResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.SolarPowerResult 'DiGi\.Solar\.Classes\.SolarPowerResult')  
A [SolarPowerResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.SolarPowerResult 'DiGi\.Solar\.Classes\.SolarPowerResult'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') if the irradiance result is null, either area is not a number or negative, or the unshaded area exceeds the total area\.

<a name='DiGi.Solar.Create.SolarPowerResult_ByShadingFactor(thisDiGi.Solar.Classes.IrradianceResult,double,double)'></a>

## Create\.SolarPowerResult\_ByShadingFactor\(this IrradianceResult, double, double\) Method

Computes the total solar power incident on a partially shaded surface, in W, from the SHADED fraction of that surface\.

This is the entry point for callers holding a shading solver result. [TryGetShadingFactor\(IShadingElement, DateTime, double, bool\)](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingModel.TryGetShadingFactor(DiGi.Solar.Interfaces.IShadingElement,System.DateTime,double,bool) 'DiGi\.Solar\.Classes\.ShadingModel\.TryGetShadingFactor\(DiGi\.Solar\.Interfaces\.IShadingElement, System\.DateTime, double, bool\)') and the [Area](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSolverResult.Area 'DiGi\.Solar\.Interfaces\.IShadingSolverResult\.Area') of a solver result both describe the area in SHADOW, so passing either into the unshaded-area overload directly would invert the answer.

```csharp
public static DiGi.Solar.Classes.SolarPowerResult? SolarPowerResult_ByShadingFactor(this DiGi.Solar.Classes.IrradianceResult? irradianceResult, double totalArea, double shadingFactor);
```
#### Parameters

<a name='DiGi.Solar.Create.SolarPowerResult_ByShadingFactor(thisDiGi.Solar.Classes.IrradianceResult,double,double).irradianceResult'></a>

`irradianceResult` [IrradianceResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.IrradianceResult 'DiGi\.Solar\.Classes\.IrradianceResult')

The [IrradianceResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.IrradianceResult 'DiGi\.Solar\.Classes\.IrradianceResult') for the hour and surface orientation\.

<a name='DiGi.Solar.Create.SolarPowerResult_ByShadingFactor(thisDiGi.Solar.Classes.IrradianceResult,double,double).totalArea'></a>

`totalArea` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The total area of the surface, in m2\.

<a name='DiGi.Solar.Create.SolarPowerResult_ByShadingFactor(thisDiGi.Solar.Classes.IrradianceResult,double,double).shadingFactor'></a>

`shadingFactor` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The shaded fraction of the surface, between 0 for fully lit and 1 for fully shaded\.

#### Returns
[SolarPowerResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.SolarPowerResult 'DiGi\.Solar\.Classes\.SolarPowerResult')  
A [SolarPowerResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.SolarPowerResult 'DiGi\.Solar\.Classes\.SolarPowerResult'), or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') if the irradiance result is null, the total area is not a number or negative, or the shading factor is not a number or lies outside the range 0 to 1\.

<a name='DiGi.Solar.Create.ViewFactorResults(thisDiGi.Solar.Classes.ShadingModel,System.Collections.Generic.IDictionary_string,DiGi.Geometry.Spatial.Classes.Vector3D_,double,int,int)'></a>

## Create\.ViewFactorResults\(this ShadingModel, IDictionary\<string,Vector3D\>, double, int, int\) Method

Calculates, for every receiver of a shading model, how much of its sky and of its ground it can see past the other elements \(receivers and shading\-only casters\)\.

Each hemisphere is split into [altitudeCount](DiGi.Solar.md#DiGi.Solar.Create.ViewFactorResults(thisDiGi.Solar.Classes.ShadingModel,System.Collections.Generic.IDictionary_string,DiGi.Geometry.Spatial.Classes.Vector3D_,double,int,int).altitudeCount 'DiGi\.Solar\.Create\.ViewFactorResults\(this DiGi\.Solar\.Classes\.ShadingModel, System\.Collections\.Generic\.IDictionary\<string,DiGi\.Geometry\.Spatial\.Classes\.Vector3D\>, double, int, int\)\.altitudeCount') equal altitude bands times [azimuthCount](DiGi.Solar.md#DiGi.Solar.Create.ViewFactorResults(thisDiGi.Solar.Classes.ShadingModel,System.Collections.Generic.IDictionary_string,DiGi.Geometry.Spatial.Classes.Vector3D_,double,int,int).azimuthCount 'DiGi\.Solar\.Create\.ViewFactorResults\(this DiGi\.Solar\.Classes\.ShadingModel, System\.Collections\.Generic\.IDictionary\<string,DiGi\.Geometry\.Spatial\.Classes\.Vector3D\>, double, int, int\)\.azimuthCount') azimuth sectors, and every patch is sampled at its centre direction `p`. A patch in front of the receiver (`cos θ = n · p > 0` for the outward normal `n`) is weighted by `cos θ · ΔΩ`, the weight an isotropic sky or ground gives it. Its blocked fraction is the shaded share of the receiver lit along `-p`: [ProjectedShadowFaces\(this Plane, BoundingBox2D, double\[\], int\[\], int, Vector3D, double\)](DiGi.Solar.md#DiGi.Solar.Query.ProjectedShadowFaces(thisDiGi.Geometry.Spatial.Classes.Plane,DiGi.Geometry.Planar.Classes.BoundingBox2D,double[],int[],int,DiGi.Geometry.Spatial.Classes.Vector3D,double) 'DiGi\.Solar\.Query\.ProjectedShadowFaces\(this DiGi\.Geometry\.Spatial\.Classes\.Plane, DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D, double\[\], int\[\], int, DiGi\.Geometry\.Spatial\.Classes\.Vector3D, double\)') merged and clipped by [ShadedFaces\(this PolygonalFace2D, IEnumerable&lt;PolygonalFace2D&gt;\)](DiGi.Solar.md#DiGi.Solar.Query.ShadedFaces(thisDiGi.Geometry.Planar.Classes.PolygonalFace2D,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Classes.PolygonalFace2D_) 'DiGi\.Solar\.Query\.ShadedFaces\(this DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D, System\.Collections\.Generic\.IEnumerable\<DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D\>\)'), the same projection the [ShadingSolver](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolver 'DiGi\.Solar\.Classes\.ShadingSolver') uses for the sun (EnergyPlus computes its isotropic diffuse shading ratio the same way).
            The visibility is the weighted unblocked share, `Σ w (1 - f) / Σ w`, and is exactly 1 when nothing blocks the view, so an open surface keeps the open-sky irradiance bit for bit. A hemisphere with no patch in front of the receiver (the ground of a flat roof) has visibility 1.

Ground patches use every caster, floor slabs included. A ground ray reaches a floor slab of a closed building only by passing its walls or roof, or by starting on its boundary: a wall touching a neighbour sees the neighbour's floor, not the ground. The ground itself is not modelled, so nothing below the lowest caster blocks.

A blocked patch contributes nothing: light reflected by the facades and roofs that block the view is ignored. That is exact for a wall touching a neighbour, but underestimates surfaces in narrow street canyons and courtyards (ZiolkowskiJakub/DiGi.Solar#15).

```csharp
public static System.Collections.Generic.List<DiGi.Solar.Classes.ViewFactorResult>? ViewFactorResults(this DiGi.Solar.Classes.ShadingModel? shadingModel, System.Collections.Generic.IDictionary<string,DiGi.Geometry.Spatial.Classes.Vector3D>? normals, double tolerance, int altitudeCount=6, int azimuthCount=24);
```
#### Parameters

<a name='DiGi.Solar.Create.ViewFactorResults(thisDiGi.Solar.Classes.ShadingModel,System.Collections.Generic.IDictionary_string,DiGi.Geometry.Spatial.Classes.Vector3D_,double,int,int).shadingModel'></a>

`shadingModel` [ShadingModel](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingModel 'DiGi\.Solar\.Classes\.ShadingModel')

The shading model\. This value can be null\.

<a name='DiGi.Solar.Create.ViewFactorResults(thisDiGi.Solar.Classes.ShadingModel,System.Collections.Generic.IDictionary_string,DiGi.Geometry.Spatial.Classes.Vector3D_,double,int,int).normals'></a>

`normals` [System\.Collections\.Generic\.IDictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2 'System\.Collections\.Generic\.IDictionary\`2')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2 'System\.Collections\.Generic\.IDictionary\`2')[DiGi\.Geometry\.Spatial\.Classes\.Vector3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.vector3d 'DiGi\.Geometry\.Spatial\.Classes\.Vector3D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2 'System\.Collections\.Generic\.IDictionary\`2')

The outward unit normal of each receiver, keyed by its reference\. A receiver missing from it, or null, uses its plane normal, which is the outward one only if the face is stored that way\.

<a name='DiGi.Solar.Create.ViewFactorResults(thisDiGi.Solar.Classes.ShadingModel,System.Collections.Generic.IDictionary_string,DiGi.Geometry.Spatial.Classes.Vector3D_,double,int,int).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance\.

<a name='DiGi.Solar.Create.ViewFactorResults(thisDiGi.Solar.Classes.ShadingModel,System.Collections.Generic.IDictionary_string,DiGi.Geometry.Spatial.Classes.Vector3D_,double,int,int).altitudeCount'></a>

`altitudeCount` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of altitude bands per hemisphere\.

<a name='DiGi.Solar.Create.ViewFactorResults(thisDiGi.Solar.Classes.ShadingModel,System.Collections.Generic.IDictionary_string,DiGi.Geometry.Spatial.Classes.Vector3D_,double,int,int).azimuthCount'></a>

`azimuthCount` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of azimuth sectors per altitude band\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[ViewFactorResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ViewFactorResult 'DiGi\.Solar\.Classes\.ViewFactorResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
One result per receiver that has a plane, a face with area and a triangulation, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when [shadingModel](DiGi.Solar.md#DiGi.Solar.Create.ViewFactorResults(thisDiGi.Solar.Classes.ShadingModel,System.Collections.Generic.IDictionary_string,DiGi.Geometry.Spatial.Classes.Vector3D_,double,int,int).shadingModel 'DiGi\.Solar\.Create\.ViewFactorResults\(this DiGi\.Solar\.Classes\.ShadingModel, System\.Collections\.Generic\.IDictionary\<string,DiGi\.Geometry\.Spatial\.Classes\.Vector3D\>, double, int, int\)\.shadingModel') is null or a patch count is below 1\.

<a name='DiGi.Solar.Query'></a>

## Query Class

```csharp
public static class Query
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Query
### Methods

<a name='DiGi.Solar.Query.Albedo(System.Nullable_double_,bool)'></a>

## Query\.Albedo\(Nullable\<double\>, bool\) Method

Resolves the ground reflectance \(albedo\) to be used for the ground\-reflected irradiance component\.

When the ground is snow covered, the supplied albedo is ignored and the result is [Snow](DiGi.Solar.Constants.md#DiGi.Solar.Constants.Albedo.Snow 'DiGi\.Solar\.Constants\.Albedo\.Snow').

An albedo that is null, not a number, zero or negative, or greater than one resolves to [Default](DiGi.Solar.Constants.md#DiGi.Solar.Constants.Albedo.Default 'DiGi\.Solar\.Constants\.Albedo\.Default'). The upper bound subsumes the weather-file missing marker 999. Zero is treated as missing because weather records that carry no albedo column report it as zero rather than as the missing marker, and taking that at face value would silently remove the whole ground-reflected component.

An EPW snow depth is not a reliable indicator of lying snow: POL_Warsaw.123750_IWEC.epw reports a constant 3.0 cm for every hour from April to November, a filler value that resolves to the snow reflectance for most of the year if read at face value (ZiolkowskiJakub/DiGi.Solar#2). The caller must decide [snowCovered](DiGi.Solar.md#DiGi.Solar.Query.Albedo(System.Nullable_double_,bool).snowCovered 'DiGi\.Solar\.Query\.Albedo\(System\.Nullable\<double\>, bool\)\.snowCovered') from a source it trusts rather than pass a raw snow depth.

```csharp
public static double Albedo(System.Nullable<double> albedo, bool snowCovered);
```
#### Parameters

<a name='DiGi.Solar.Query.Albedo(System.Nullable_double_,bool).albedo'></a>

`albedo` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The ground reflectance as supplied by the weather record, as a decimal fraction\. May be null, or a missing marker\.

<a name='DiGi.Solar.Query.Albedo(System.Nullable_double_,bool).snowCovered'></a>

`snowCovered` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

Whether snow is lying on the ground, decided by the caller\. See the summary for why the library does not accept a snow depth\.

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')  
The ground reflectance to use, as a decimal fraction between 0 and 1\.

<a name='DiGi.Solar.Query.GroupDirections(thisSystem.Collections.Generic.Dictionary_System.DateTime,DiGi.Geometry.Spatial.Classes.Vector3D_,double)'></a>

## Query\.GroupDirections\(this Dictionary\<DateTime,Vector3D\>, double\) Method

Groups directions from a dictionary of dates and vectors based on a specified angle tolerance\.

```csharp
public static System.Collections.Generic.List<System.Tuple<DiGi.Geometry.Spatial.Classes.Vector3D,System.Collections.Generic.List<System.DateTime>>>? GroupDirections(this System.Collections.Generic.Dictionary<System.DateTime,DiGi.Geometry.Spatial.Classes.Vector3D>? dictionary, double angleTolerance);
```
#### Parameters

<a name='DiGi.Solar.Query.GroupDirections(thisSystem.Collections.Generic.Dictionary_System.DateTime,DiGi.Geometry.Spatial.Classes.Vector3D_,double).dictionary'></a>

`dictionary` [System\.Collections\.Generic\.Dictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[DiGi\.Geometry\.Spatial\.Classes\.Vector3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.vector3d 'DiGi\.Geometry\.Spatial\.Classes\.Vector3D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')

The dictionary containing date\-time keys and their corresponding 3D direction vectors\.

<a name='DiGi.Solar.Query.GroupDirections(thisSystem.Collections.Generic.Dictionary_System.DateTime,DiGi.Geometry.Spatial.Classes.Vector3D_,double).angleTolerance'></a>

`angleTolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The maximum angle difference allowed to group two directions together\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.Tuple&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.tuple-2 'System\.Tuple\`2')[DiGi\.Geometry\.Spatial\.Classes\.Vector3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.vector3d 'DiGi\.Geometry\.Spatial\.Classes\.Vector3D')[,](https://learn.microsoft.com/en-us/dotnet/api/system.tuple-2 'System\.Tuple\`2')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.tuple-2 'System\.Tuple\`2')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A list of tuples, where each tuple contains a representative [DiGi\.Geometry\.Spatial\.Classes\.Vector3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.vector3d 'DiGi\.Geometry\.Spatial\.Classes\.Vector3D') and a list of [System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime') values associated with that direction; returns [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') if the input dictionary is null\.

<a name='DiGi.Solar.Query.ProjectedShadowFaces(thisDiGi.Geometry.Spatial.Classes.Plane,DiGi.Geometry.Planar.Classes.BoundingBox2D,double[],int[],int,DiGi.Geometry.Spatial.Classes.Vector3D,double)'></a>

## Query\.ProjectedShadowFaces\(this Plane, BoundingBox2D, double\[\], int\[\], int, Vector3D, double\) Method

Computes the unmerged shadows that caster triangles throw onto a receiver plane along one propagation direction\.

Each triangle is clipped to the part lying upstream of the receiver plane (between the light source and the plane), projected onto the plane along [direction](DiGi.Solar.md#DiGi.Solar.Query.ProjectedShadowFaces(thisDiGi.Geometry.Spatial.Classes.Plane,DiGi.Geometry.Planar.Classes.BoundingBox2D,double[],int[],int,DiGi.Geometry.Spatial.Classes.Vector3D,double).direction 'DiGi\.Solar\.Query\.ProjectedShadowFaces\(this DiGi\.Geometry\.Spatial\.Classes\.Plane, DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D, double\[\], int\[\], int, DiGi\.Geometry\.Spatial\.Classes\.Vector3D, double\)\.direction') and expressed in the plane's coordinates. Triangles whose projection misses the receiver's bounding box, and slivers with no area, are dropped.

This is the per-direction step of [Solve\(\)](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolver.Solve() 'DiGi\.Solar\.Classes\.ShadingSolver\.Solve\(\)'); merge and clip the result with [ShadedFaces\(this PolygonalFace2D, IEnumerable&lt;PolygonalFace2D&gt;\)](DiGi.Solar.md#DiGi.Solar.Query.ShadedFaces(thisDiGi.Geometry.Planar.Classes.PolygonalFace2D,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Classes.PolygonalFace2D_) 'DiGi\.Solar\.Query\.ShadedFaces\(this DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D, System\.Collections\.Generic\.IEnumerable\<DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D\>\)'). A direction grazing the plane (its dot product with the plane normal within [tolerance](DiGi.Solar.md#DiGi.Solar.Query.ProjectedShadowFaces(thisDiGi.Geometry.Spatial.Classes.Plane,DiGi.Geometry.Planar.Classes.BoundingBox2D,double[],int[],int,DiGi.Geometry.Spatial.Classes.Vector3D,double).tolerance 'DiGi\.Solar\.Query\.ProjectedShadowFaces\(this DiGi\.Geometry\.Spatial\.Classes\.Plane, DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D, double\[\], int\[\], int, DiGi\.Geometry\.Spatial\.Classes\.Vector3D, double\)\.tolerance')) casts no shadow.

```csharp
public static System.Collections.Generic.List<DiGi.Geometry.Planar.Classes.PolygonalFace2D> ProjectedShadowFaces(this DiGi.Geometry.Spatial.Classes.Plane? plane, DiGi.Geometry.Planar.Classes.BoundingBox2D? boundingBox2D, double[]? coordinates, int[]? indexes, int index, DiGi.Geometry.Spatial.Classes.Vector3D? direction, double tolerance);
```
#### Parameters

<a name='DiGi.Solar.Query.ProjectedShadowFaces(thisDiGi.Geometry.Spatial.Classes.Plane,DiGi.Geometry.Planar.Classes.BoundingBox2D,double[],int[],int,DiGi.Geometry.Spatial.Classes.Vector3D,double).plane'></a>

`plane` [DiGi\.Geometry\.Spatial\.Classes\.Plane](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.plane 'DiGi\.Geometry\.Spatial\.Classes\.Plane')

The receiver plane\. This value can be null\.

<a name='DiGi.Solar.Query.ProjectedShadowFaces(thisDiGi.Geometry.Spatial.Classes.Plane,DiGi.Geometry.Planar.Classes.BoundingBox2D,double[],int[],int,DiGi.Geometry.Spatial.Classes.Vector3D,double).boundingBox2D'></a>

`boundingBox2D` [DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D')

The bounding box of the receiver face in the plane's coordinates\. This value can be null\.

<a name='DiGi.Solar.Query.ProjectedShadowFaces(thisDiGi.Geometry.Spatial.Classes.Plane,DiGi.Geometry.Planar.Classes.BoundingBox2D,double[],int[],int,DiGi.Geometry.Spatial.Classes.Vector3D,double).coordinates'></a>

`coordinates` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The caster triangles, 9 coordinates \(3 points x, y, z\) per triangle\.

<a name='DiGi.Solar.Query.ProjectedShadowFaces(thisDiGi.Geometry.Spatial.Classes.Plane,DiGi.Geometry.Planar.Classes.BoundingBox2D,double[],int[],int,DiGi.Geometry.Spatial.Classes.Vector3D,double).indexes'></a>

`indexes` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

The index of the element each triangle belongs to, one per triangle\.

<a name='DiGi.Solar.Query.ProjectedShadowFaces(thisDiGi.Geometry.Spatial.Classes.Plane,DiGi.Geometry.Planar.Classes.BoundingBox2D,double[],int[],int,DiGi.Geometry.Spatial.Classes.Vector3D,double).index'></a>

`index` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The index of the receiver; its own triangles \(equal index\) are skipped, so a receiver never shades itself\. Use \-1 to skip none\.

<a name='DiGi.Solar.Query.ProjectedShadowFaces(thisDiGi.Geometry.Spatial.Classes.Plane,DiGi.Geometry.Planar.Classes.BoundingBox2D,double[],int[],int,DiGi.Geometry.Spatial.Classes.Vector3D,double).direction'></a>

`direction` [DiGi\.Geometry\.Spatial\.Classes\.Vector3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.vector3d 'DiGi\.Geometry\.Spatial\.Classes\.Vector3D')

The propagation direction of the light, pointing away from its source\. Need not be unit length\.

<a name='DiGi.Solar.Query.ProjectedShadowFaces(thisDiGi.Geometry.Spatial.Classes.Plane,DiGi.Geometry.Planar.Classes.BoundingBox2D,double[],int[],int,DiGi.Geometry.Spatial.Classes.Vector3D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.polygonalface2d 'DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
The shadow faces in the receiver plane's coordinates, empty when no shadow reaches the receiver or an input is null\.

<a name='DiGi.Solar.Query.ShadedFaces(thisDiGi.Geometry.Planar.Classes.PolygonalFace2D,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Classes.PolygonalFace2D_)'></a>

## Query\.ShadedFaces\(this PolygonalFace2D, IEnumerable\<PolygonalFace2D\>\) Method

Builds the shaded part of a receiver from its shadow faces: the shadows are merged into one hole\-preserving union and each union face is clipped to the receiver\.

This is the post-processing both shading solvers share, so the CPU [ShadingSolver](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolver 'DiGi\.Solar\.Classes\.ShadingSolver') and the ComputeSharp solver cannot drift apart after the shadows are computed.

If the merge fails (`DiGi.Geometry.Planar.Query.Union` returns `null` even after its snap-rounding retry), the unmerged shadows are clipped to the receiver and capped at its area instead ([ShadowFaces\(this PolygonalFace2D, IEnumerable&lt;PolygonalFace2D&gt;\)](DiGi.Solar.md#DiGi.Solar.Query.ShadowFaces(thisDiGi.Geometry.Planar.Classes.PolygonalFace2D,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Classes.PolygonalFace2D_) 'DiGi\.Solar\.Query\.ShadowFaces\(this DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D, System\.Collections\.Generic\.IEnumerable\<DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D\>\)')): a failed merge reads as shade, overstated at worst, and never as full sun.
            The same fallback applies when clipping a merged face to the receiver fails (`DiGi.Geometry.Planar.Query.Intersection` returns `null`), which a union face with zero-area sliver holes has caused (ZiolkowskiJakub/DiGi.Solar#16).

```csharp
public static System.Collections.Generic.List<DiGi.Geometry.Planar.Classes.PolygonalFace2D>? ShadedFaces(this DiGi.Geometry.Planar.Classes.PolygonalFace2D? polygonalFace2D_Receiver, System.Collections.Generic.IEnumerable<DiGi.Geometry.Planar.Classes.PolygonalFace2D>? polygonalFace2Ds_Shadow);
```
#### Parameters

<a name='DiGi.Solar.Query.ShadedFaces(thisDiGi.Geometry.Planar.Classes.PolygonalFace2D,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Classes.PolygonalFace2D_).polygonalFace2D_Receiver'></a>

`polygonalFace2D_Receiver` [DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.polygonalface2d 'DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D')

The receiver face, in its own plane coordinates, or null when the receiver has no face to clip against\.

<a name='DiGi.Solar.Query.ShadedFaces(thisDiGi.Geometry.Planar.Classes.PolygonalFace2D,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Classes.PolygonalFace2D_).polygonalFace2Ds_Shadow'></a>

`polygonalFace2Ds_Shadow` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.polygonalface2d 'DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The shadow faces, in the receiver's plane coordinates, as the solver produced them before merging; null or empty for a fully sunlit receiver\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.polygonalface2d 'DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
The shaded faces of the receiver, empty when no shadow reaches it; or null when [polygonalFace2D\_Receiver](DiGi.Solar.md#DiGi.Solar.Query.ShadedFaces(thisDiGi.Geometry.Planar.Classes.PolygonalFace2D,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Classes.PolygonalFace2D_).polygonalFace2D_Receiver 'DiGi\.Solar\.Query\.ShadedFaces\(this DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D, System\.Collections\.Generic\.IEnumerable\<DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D\>\)\.polygonalFace2D\_Receiver') is null\.

<a name='DiGi.Solar.Query.ShadowFaces(thisDiGi.Geometry.Planar.Classes.PolygonalFace2D,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Classes.PolygonalFace2D_)'></a>

## Query\.ShadowFaces\(this PolygonalFace2D, IEnumerable\<PolygonalFace2D\>\) Method

Builds the shadow of a receiver from unmerged shadow faces: each face is clipped to the receiver, and faces are kept in input order while their total area stays within the receiver face area\.

Use when the merged union of the shadows is unavailable (`DiGi.Geometry.Planar.Query.Union` returned `null` even after its snap-rounding retry). Overlapping shadows are then counted twice, up to the cap, which overstates the shaded area but never above the receiver: a failed merge reads as shade rather than as full sun.

For shadows that do not overlap each other the result is exactly the union, so the fallback is exact rather than merely bounded in the common case. The summed area stays within the receiver area unless the first clipped face is larger than the receiver on its own, which the clip itself cannot produce (it is inside the receiver) other than by numerical overshoot.

```csharp
public static System.Collections.Generic.List<DiGi.Geometry.Planar.Classes.PolygonalFace2D>? ShadowFaces(this DiGi.Geometry.Planar.Classes.PolygonalFace2D? polygonalFace2D_Receiver, System.Collections.Generic.IEnumerable<DiGi.Geometry.Planar.Classes.PolygonalFace2D> polygonalFace2Ds_Shadow);
```
#### Parameters

<a name='DiGi.Solar.Query.ShadowFaces(thisDiGi.Geometry.Planar.Classes.PolygonalFace2D,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Classes.PolygonalFace2D_).polygonalFace2D_Receiver'></a>

`polygonalFace2D_Receiver` [DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.polygonalface2d 'DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D')

The receiver face, in its own plane coordinates, or null when the receiver has no face to clip and cap against\.

<a name='DiGi.Solar.Query.ShadowFaces(thisDiGi.Geometry.Planar.Classes.PolygonalFace2D,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Classes.PolygonalFace2D_).polygonalFace2Ds_Shadow'></a>

`polygonalFace2Ds_Shadow` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.polygonalface2d 'DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The shadow faces, in the receiver's plane coordinates, as the solver produced them before merging\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.polygonalface2d 'DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A list of shadow faces clipped to the receiver, whose summed area never exceeds the receiver face area; or null when [polygonalFace2D\_Receiver](DiGi.Solar.md#DiGi.Solar.Query.ShadowFaces(thisDiGi.Geometry.Planar.Classes.PolygonalFace2D,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Classes.PolygonalFace2D_).polygonalFace2D_Receiver 'DiGi\.Solar\.Query\.ShadowFaces\(this DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D, System\.Collections\.Generic\.IEnumerable\<DiGi\.Geometry\.Planar\.Classes\.PolygonalFace2D\>\)\.polygonalFace2D\_Receiver') is null\.

<a name='DiGi.Solar.Query.SunDirection(thisDiGi.Core.Classes.Coordinates,DiGi.Core.Enums.UTC,System.DateTime,bool)'></a>

## Query\.SunDirection\(this Coordinates, UTC, DateTime, bool\) Method

Calculates the sun's direction vector for a specific geographic location and date time\.

```csharp
public static DiGi.Geometry.Spatial.Classes.Vector3D? SunDirection(this DiGi.Core.Classes.Coordinates? coordinates, DiGi.Core.Enums.UTC uTC, System.DateTime dateTime, bool includeNight=false);
```
#### Parameters

<a name='DiGi.Solar.Query.SunDirection(thisDiGi.Core.Classes.Coordinates,DiGi.Core.Enums.UTC,System.DateTime,bool).coordinates'></a>

`coordinates` [DiGi\.Core\.Classes\.Coordinates](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.coordinates 'DiGi\.Core\.Classes\.Coordinates')

The [DiGi\.Core\.Classes\.Coordinates](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.coordinates 'DiGi\.Core\.Classes\.Coordinates') of the geographic location\.

<a name='DiGi.Solar.Query.SunDirection(thisDiGi.Core.Classes.Coordinates,DiGi.Core.Enums.UTC,System.DateTime,bool).uTC'></a>

`uTC` [DiGi\.Core\.Enums\.UTC](https://learn.microsoft.com/en-us/dotnet/api/digi.core.enums.utc 'DiGi\.Core\.Enums\.UTC')

The [DiGi\.Core\.Enums\.UTC](https://learn.microsoft.com/en-us/dotnet/api/digi.core.enums.utc 'DiGi\.Core\.Enums\.UTC') timezone offset\.

<a name='DiGi.Solar.Query.SunDirection(thisDiGi.Core.Classes.Coordinates,DiGi.Core.Enums.UTC,System.DateTime,bool).dateTime'></a>

`dateTime` [System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime')

The date and time for which to calculate the sun's position\.

<a name='DiGi.Solar.Query.SunDirection(thisDiGi.Core.Classes.Coordinates,DiGi.Core.Enums.UTC,System.DateTime,bool).includeNight'></a>

`includeNight` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

If set to [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool'), returns [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') if the specified [dateTime](DiGi.Solar.md#DiGi.Solar.Query.SunDirection(thisDiGi.Core.Classes.Coordinates,DiGi.Core.Enums.UTC,System.DateTime,bool).dateTime 'DiGi\.Solar\.Query\.SunDirection\(this DiGi\.Core\.Classes\.Coordinates, DiGi\.Core\.Enums\.UTC, System\.DateTime, bool\)\.dateTime') is before sunrise or after sunset\.

#### Returns
[DiGi\.Geometry\.Spatial\.Classes\.Vector3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.vector3d 'DiGi\.Geometry\.Spatial\.Classes\.Vector3D')  
A [DiGi\.Geometry\.Spatial\.Classes\.Vector3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.vector3d 'DiGi\.Geometry\.Spatial\.Classes\.Vector3D') representing the sun's direction, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') if coordinates are null, date time is invalid, or it is nighttime and [includeNight](DiGi.Solar.md#DiGi.Solar.Query.SunDirection(thisDiGi.Core.Classes.Coordinates,DiGi.Core.Enums.UTC,System.DateTime,bool).includeNight 'DiGi\.Solar\.Query\.SunDirection\(this DiGi\.Core\.Classes\.Coordinates, DiGi\.Core\.Enums\.UTC, System\.DateTime, bool\)\.includeNight') is false\.

<a name='DiGi.Solar.Query.SunDirection(thisDiGi.Solar.Classes.ShadingModel,System.DateTime,bool)'></a>

## Query\.SunDirection\(this ShadingModel, DateTime, bool\) Method

Calculates the sun's direction vector based on a shading model's configuration and a specific date time\.

```csharp
public static DiGi.Geometry.Spatial.Classes.Vector3D? SunDirection(this DiGi.Solar.Classes.ShadingModel? shadingModel, System.DateTime dateTime, bool includeNight=false);
```
#### Parameters

<a name='DiGi.Solar.Query.SunDirection(thisDiGi.Solar.Classes.ShadingModel,System.DateTime,bool).shadingModel'></a>

`shadingModel` [ShadingModel](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingModel 'DiGi\.Solar\.Classes\.ShadingModel')

The [ShadingModel](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingModel 'DiGi\.Solar\.Classes\.ShadingModel') containing location and timezone information\.

<a name='DiGi.Solar.Query.SunDirection(thisDiGi.Solar.Classes.ShadingModel,System.DateTime,bool).dateTime'></a>

`dateTime` [System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime')

The date and time for which to calculate the sun's position\.

<a name='DiGi.Solar.Query.SunDirection(thisDiGi.Solar.Classes.ShadingModel,System.DateTime,bool).includeNight'></a>

`includeNight` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

If set to [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool'), returns [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') if the specified [dateTime](DiGi.Solar.md#DiGi.Solar.Query.SunDirection(thisDiGi.Solar.Classes.ShadingModel,System.DateTime,bool).dateTime 'DiGi\.Solar\.Query\.SunDirection\(this DiGi\.Solar\.Classes\.ShadingModel, System\.DateTime, bool\)\.dateTime') is before sunrise or after sunset\.

#### Returns
[DiGi\.Geometry\.Spatial\.Classes\.Vector3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.vector3d 'DiGi\.Geometry\.Spatial\.Classes\.Vector3D')  
A [DiGi\.Geometry\.Spatial\.Classes\.Vector3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.vector3d 'DiGi\.Geometry\.Spatial\.Classes\.Vector3D') representing the sun's direction, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') if the shading model is null or other calculation constraints are not met\.

<a name='DiGi.Solar.Query.SunDirection(thisSolarTimes)'></a>

## Query\.SunDirection\(this SolarTimes\) Method

Calculates the sun's direction vector based on the provided solar times\.

```csharp
public static DiGi.Geometry.Spatial.Classes.Vector3D? SunDirection(this SolarTimes? solarTimes);
```
#### Parameters

<a name='DiGi.Solar.Query.SunDirection(thisSolarTimes).solarTimes'></a>

`solarTimes` [Innovative\.SolarCalculator\.SolarTimes](https://learn.microsoft.com/en-us/dotnet/api/innovative.solarcalculator.solartimes 'Innovative\.SolarCalculator\.SolarTimes')

The [Innovative\.SolarCalculator\.SolarTimes](https://learn.microsoft.com/en-us/dotnet/api/innovative.solarcalculator.solartimes 'Innovative\.SolarCalculator\.SolarTimes') containing the solar elevation and azimuth\.

#### Returns
[DiGi\.Geometry\.Spatial\.Classes\.Vector3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.vector3d 'DiGi\.Geometry\.Spatial\.Classes\.Vector3D')  
A [DiGi\.Geometry\.Spatial\.Classes\.Vector3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.vector3d 'DiGi\.Geometry\.Spatial\.Classes\.Vector3D') representing the sun's direction, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') if the provided [Innovative\.SolarCalculator\.SolarTimes](https://learn.microsoft.com/en-us/dotnet/api/innovative.solarcalculator.solartimes 'Innovative\.SolarCalculator\.SolarTimes') is null or contains invalid data\.