#### [DiGi\.Solar](index.md 'index')

## DiGi\.Solar\.Classes Namespace
### Classes

<a name='DiGi.Solar.Classes.GeometricalShadingSolverResult'></a>

## GeometricalShadingSolverResult Class

Represents the result of a geometrical shading solver, containing spatial data such as the plane and polygonal faces used for shading calculations\.

```csharp
public class GeometricalShadingSolverResult : DiGi.Solar.Classes.ShadingSolverResult
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.Core\.Classes\.UniqueObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.uniqueobject 'DiGi\.Core\.Classes\.UniqueObject') → [DiGi\.Core\.Classes\.GuidObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.guidobject 'DiGi\.Core\.Classes\.GuidObject') → [DiGi\.Core\.Classes\.GuidResult](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.guidresult 'DiGi\.Core\.Classes\.GuidResult') → [DiGi\.Core\.Classes\.GuidResult&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.guidresult-1 'DiGi\.Core\.Classes\.GuidResult\`1')[IShadingElement](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingElement 'DiGi\.Solar\.Interfaces\.IShadingElement')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.guidresult-1 'DiGi\.Core\.Classes\.GuidResult\`1') → [ShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolverResult 'DiGi\.Solar\.Classes\.ShadingSolverResult') → GeometricalShadingSolverResult
### Constructors

<a name='DiGi.Solar.Classes.GeometricalShadingSolverResult.GeometricalShadingSolverResult(DiGi.Solar.Classes.GeometricalShadingSolverResult)'></a>

## GeometricalShadingSolverResult\(GeometricalShadingSolverResult\) Constructor

Initializes a new instance of the [GeometricalShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.GeometricalShadingSolverResult 'DiGi\.Solar\.Classes\.GeometricalShadingSolverResult') class by cloning an existing result\.

```csharp
public GeometricalShadingSolverResult(DiGi.Solar.Classes.GeometricalShadingSolverResult? geometricalShadingSolverResult);
```
#### Parameters

<a name='DiGi.Solar.Classes.GeometricalShadingSolverResult.GeometricalShadingSolverResult(DiGi.Solar.Classes.GeometricalShadingSolverResult).geometricalShadingSolverResult'></a>

`geometricalShadingSolverResult` [GeometricalShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.GeometricalShadingSolverResult 'DiGi\.Solar\.Classes\.GeometricalShadingSolverResult')

The source geometrical shading solver result to clone\.

<a name='DiGi.Solar.Classes.GeometricalShadingSolverResult.GeometricalShadingSolverResult(System.DateTime,DiGi.Geometry.Spatial.Classes.Plane,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Interfaces.IPolygonalFace2D_)'></a>

## GeometricalShadingSolverResult\(DateTime, Plane, IEnumerable\<IPolygonalFace2D\>\) Constructor

Initializes a new instance of the [GeometricalShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.GeometricalShadingSolverResult 'DiGi\.Solar\.Classes\.GeometricalShadingSolverResult') class\.

```csharp
public GeometricalShadingSolverResult(System.DateTime dateTime, DiGi.Geometry.Spatial.Classes.Plane? plane, System.Collections.Generic.IEnumerable<DiGi.Geometry.Planar.Interfaces.IPolygonalFace2D>? polygonalFace2Ds);
```
#### Parameters

<a name='DiGi.Solar.Classes.GeometricalShadingSolverResult.GeometricalShadingSolverResult(System.DateTime,DiGi.Geometry.Spatial.Classes.Plane,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Interfaces.IPolygonalFace2D_).dateTime'></a>

`dateTime` [System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime')

The date and time associated with the shading result\.

<a name='DiGi.Solar.Classes.GeometricalShadingSolverResult.GeometricalShadingSolverResult(System.DateTime,DiGi.Geometry.Spatial.Classes.Plane,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Interfaces.IPolygonalFace2D_).plane'></a>

`plane` [DiGi\.Geometry\.Spatial\.Classes\.Plane](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.plane 'DiGi\.Geometry\.Spatial\.Classes\.Plane')

The plane on which the geometrical calculations are performed\.

<a name='DiGi.Solar.Classes.GeometricalShadingSolverResult.GeometricalShadingSolverResult(System.DateTime,DiGi.Geometry.Spatial.Classes.Plane,System.Collections.Generic.IEnumerable_DiGi.Geometry.Planar.Interfaces.IPolygonalFace2D_).polygonalFace2Ds'></a>

`polygonalFace2Ds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.Geometry\.Planar\.Interfaces\.IPolygonalFace2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.interfaces.ipolygonalface2d 'DiGi\.Geometry\.Planar\.Interfaces\.IPolygonalFace2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The collection of 2D polygonal faces involved in the shading\.

<a name='DiGi.Solar.Classes.GeometricalShadingSolverResult.GeometricalShadingSolverResult(System.Text.Json.Nodes.JsonObject)'></a>

## GeometricalShadingSolverResult\(JsonObject\) Constructor

Initializes a new instance of the [GeometricalShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.GeometricalShadingSolverResult 'DiGi\.Solar\.Classes\.GeometricalShadingSolverResult') class from a JSON object\.

```csharp
public GeometricalShadingSolverResult(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.Solar.Classes.GeometricalShadingSolverResult.GeometricalShadingSolverResult(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JSON object containing the result data\.
### Properties

<a name='DiGi.Solar.Classes.GeometricalShadingSolverResult.Area'></a>

## GeometricalShadingSolverResult\.Area Property

Gets the total area of all associated 2D polygonal faces\.

```csharp
public override double Area { get; }
```

Implements [Area](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSolverResult.Area 'DiGi\.Solar\.Interfaces\.IShadingSolverResult\.Area')

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.Solar.Classes.GeometricalShadingSolverResult.Plane'></a>

## GeometricalShadingSolverResult\.Plane Property

Gets the plane associated with this shading solver result\.

```csharp
public DiGi.Geometry.Spatial.Classes.Plane? Plane { get; }
```

#### Property Value
[DiGi\.Geometry\.Spatial\.Classes\.Plane](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.plane 'DiGi\.Geometry\.Spatial\.Classes\.Plane')

<a name='DiGi.Solar.Classes.GeometricalShadingSolverResult.PolygonalFace2Ds'></a>

## GeometricalShadingSolverResult\.PolygonalFace2Ds Property

Gets the list of 2D polygonal faces associated with this shading solver result\.

```csharp
public System.Collections.Generic.List<DiGi.Geometry.Planar.Interfaces.IPolygonalFace2D>? PolygonalFace2Ds { get; }
```

#### Property Value
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.Geometry\.Planar\.Interfaces\.IPolygonalFace2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.interfaces.ipolygonalface2d 'DiGi\.Geometry\.Planar\.Interfaces\.IPolygonalFace2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')
### Methods

<a name='DiGi.Solar.Classes.GeometricalShadingSolverResult.GetPolygonalFace3Ds()'></a>

## GeometricalShadingSolverResult\.GetPolygonalFace3Ds\(\) Method

Converts the 2D polygonal faces to their 3D representations based on the associated plane\.

```csharp
public System.Collections.Generic.List<DiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D>? GetPolygonalFace3Ds();
```

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.Geometry\.Spatial\.Interfaces\.IPolygonalFace3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.interfaces.ipolygonalface3d 'DiGi\.Geometry\.Spatial\.Interfaces\.IPolygonalFace3D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A list of 3D polygonal faces, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') if the plane or 2D faces are not defined\.

<a name='DiGi.Solar.Classes.NumericalShadingSolverResult'></a>

## NumericalShadingSolverResult Class

Represents the result of a numerical shading solver calculation, providing details such as the calculated area\.

```csharp
public class NumericalShadingSolverResult : DiGi.Solar.Classes.ShadingSolverResult
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.Core\.Classes\.UniqueObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.uniqueobject 'DiGi\.Core\.Classes\.UniqueObject') → [DiGi\.Core\.Classes\.GuidObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.guidobject 'DiGi\.Core\.Classes\.GuidObject') → [DiGi\.Core\.Classes\.GuidResult](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.guidresult 'DiGi\.Core\.Classes\.GuidResult') → [DiGi\.Core\.Classes\.GuidResult&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.guidresult-1 'DiGi\.Core\.Classes\.GuidResult\`1')[IShadingElement](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingElement 'DiGi\.Solar\.Interfaces\.IShadingElement')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.guidresult-1 'DiGi\.Core\.Classes\.GuidResult\`1') → [ShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolverResult 'DiGi\.Solar\.Classes\.ShadingSolverResult') → NumericalShadingSolverResult
### Constructors

<a name='DiGi.Solar.Classes.NumericalShadingSolverResult.NumericalShadingSolverResult(DiGi.Solar.Classes.NumericalShadingSolverResult)'></a>

## NumericalShadingSolverResult\(NumericalShadingSolverResult\) Constructor

Initializes a new instance of the [NumericalShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.NumericalShadingSolverResult 'DiGi\.Solar\.Classes\.NumericalShadingSolverResult') class by copying an existing result instance\.

```csharp
public NumericalShadingSolverResult(DiGi.Solar.Classes.NumericalShadingSolverResult numericalShadingSolverResult);
```
#### Parameters

<a name='DiGi.Solar.Classes.NumericalShadingSolverResult.NumericalShadingSolverResult(DiGi.Solar.Classes.NumericalShadingSolverResult).numericalShadingSolverResult'></a>

`numericalShadingSolverResult` [NumericalShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.NumericalShadingSolverResult 'DiGi\.Solar\.Classes\.NumericalShadingSolverResult')

The source [NumericalShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.NumericalShadingSolverResult 'DiGi\.Solar\.Classes\.NumericalShadingSolverResult') instance to copy from\.

<a name='DiGi.Solar.Classes.NumericalShadingSolverResult.NumericalShadingSolverResult(System.DateTime,double)'></a>

## NumericalShadingSolverResult\(DateTime, double\) Constructor

Initializes a new instance of the [NumericalShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.NumericalShadingSolverResult 'DiGi\.Solar\.Classes\.NumericalShadingSolverResult') class with a specified date time and area\.

```csharp
public NumericalShadingSolverResult(System.DateTime dateTime, double area);
```
#### Parameters

<a name='DiGi.Solar.Classes.NumericalShadingSolverResult.NumericalShadingSolverResult(System.DateTime,double).dateTime'></a>

`dateTime` [System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime')

The date and time associated with the shading calculation result\.

<a name='DiGi.Solar.Classes.NumericalShadingSolverResult.NumericalShadingSolverResult(System.DateTime,double).area'></a>

`area` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The numerical value of the calculated shading area\.

<a name='DiGi.Solar.Classes.NumericalShadingSolverResult.NumericalShadingSolverResult(System.Text.Json.Nodes.JsonObject)'></a>

## NumericalShadingSolverResult\(JsonObject\) Constructor

Initializes a new instance of the [NumericalShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.NumericalShadingSolverResult 'DiGi\.Solar\.Classes\.NumericalShadingSolverResult') class from a JSON object\.

```csharp
public NumericalShadingSolverResult(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.Solar.Classes.NumericalShadingSolverResult.NumericalShadingSolverResult(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject') containing the result data\.
### Properties

<a name='DiGi.Solar.Classes.NumericalShadingSolverResult.Area'></a>

## NumericalShadingSolverResult\.Area Property

Gets the numerical shading area\.

```csharp
public override double Area { get; }
```

Implements [Area](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSolverResult.Area 'DiGi\.Solar\.Interfaces\.IShadingSolverResult\.Area')

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.Solar.Classes.ShadingElement'></a>

## ShadingElement Class

Represents an element that provides shading in a solar analysis context\.

```csharp
public class ShadingElement : DiGi.Core.Classes.GuidObject, DiGi.Solar.Interfaces.IShadingElement, DiGi.Solar.Interfaces.IShadingUniqueObject, DiGi.Solar.Interfaces.IShadingSerializableObject, DiGi.Solar.Interfaces.IShadingObject, DiGi.Solar.Interfaces.ISolarObject, DiGi.Core.Interfaces.IObject, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject, DiGi.Core.Interfaces.IUniqueObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.Core\.Classes\.UniqueObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.uniqueobject 'DiGi\.Core\.Classes\.UniqueObject') → [DiGi\.Core\.Classes\.GuidObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.guidobject 'DiGi\.Core\.Classes\.GuidObject') → ShadingElement

Implements [IShadingElement](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingElement 'DiGi\.Solar\.Interfaces\.IShadingElement'), [IShadingUniqueObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingUniqueObject 'DiGi\.Solar\.Interfaces\.IShadingUniqueObject'), [IShadingSerializableObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSerializableObject 'DiGi\.Solar\.Interfaces\.IShadingSerializableObject'), [IShadingObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingObject 'DiGi\.Solar\.Interfaces\.IShadingObject'), [ISolarObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.ISolarObject 'DiGi\.Solar\.Interfaces\.ISolarObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject'), [DiGi\.Core\.Interfaces\.IUniqueObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iuniqueobject 'DiGi\.Core\.Interfaces\.IUniqueObject')
### Constructors

<a name='DiGi.Solar.Classes.ShadingElement.ShadingElement(DiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D,bool)'></a>

## ShadingElement\(IPolygonalFace3D, bool\) Constructor

Initializes a new instance of the [ShadingElement](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingElement 'DiGi\.Solar\.Classes\.ShadingElement') class with specified geometry and properties, and a generated unique identifier\.

```csharp
public ShadingElement(DiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D? polygonalFace3D, bool shadingOnly);
```
#### Parameters

<a name='DiGi.Solar.Classes.ShadingElement.ShadingElement(DiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D,bool).polygonalFace3D'></a>

`polygonalFace3D` [DiGi\.Geometry\.Spatial\.Interfaces\.IPolygonalFace3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.interfaces.ipolygonalface3d 'DiGi\.Geometry\.Spatial\.Interfaces\.IPolygonalFace3D')

The 3D polygonal face representing the geometry of the shading element\.

<a name='DiGi.Solar.Classes.ShadingElement.ShadingElement(DiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D,bool).shadingOnly'></a>

`shadingOnly` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A flag indicating if the element is used exclusively for shading calculations\.

<a name='DiGi.Solar.Classes.ShadingElement.ShadingElement(DiGi.Solar.Classes.ShadingElement)'></a>

## ShadingElement\(ShadingElement\) Constructor

Initializes a new instance of the [ShadingElement](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingElement 'DiGi\.Solar\.Classes\.ShadingElement') class by cloning an existing shading element\.

```csharp
public ShadingElement(DiGi.Solar.Classes.ShadingElement? shadingElement);
```
#### Parameters

<a name='DiGi.Solar.Classes.ShadingElement.ShadingElement(DiGi.Solar.Classes.ShadingElement).shadingElement'></a>

`shadingElement` [ShadingElement](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingElement 'DiGi\.Solar\.Classes\.ShadingElement')

The source [ShadingElement](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingElement 'DiGi\.Solar\.Classes\.ShadingElement') to clone\.

<a name='DiGi.Solar.Classes.ShadingElement.ShadingElement(string,DiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D,bool)'></a>

## ShadingElement\(string, IPolygonalFace3D, bool\) Constructor

Initializes a new instance of the [ShadingElement](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingElement 'DiGi\.Solar\.Classes\.ShadingElement') class with specified properties and a generated unique identifier\.

```csharp
public ShadingElement(string? reference, DiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D? polygonalFace3D, bool shadingOnly);
```
#### Parameters

<a name='DiGi.Solar.Classes.ShadingElement.ShadingElement(string,DiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D,bool).reference'></a>

`reference` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The reference string identifying the element\.

<a name='DiGi.Solar.Classes.ShadingElement.ShadingElement(string,DiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D,bool).polygonalFace3D'></a>

`polygonalFace3D` [DiGi\.Geometry\.Spatial\.Interfaces\.IPolygonalFace3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.interfaces.ipolygonalface3d 'DiGi\.Geometry\.Spatial\.Interfaces\.IPolygonalFace3D')

The 3D polygonal face representing the geometry of the shading element\.

<a name='DiGi.Solar.Classes.ShadingElement.ShadingElement(string,DiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D,bool).shadingOnly'></a>

`shadingOnly` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A flag indicating if the element is used exclusively for shading calculations\.

<a name='DiGi.Solar.Classes.ShadingElement.ShadingElement(System.Guid,string,DiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D,bool)'></a>

## ShadingElement\(Guid, string, IPolygonalFace3D, bool\) Constructor

Initializes a new instance of the [ShadingElement](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingElement 'DiGi\.Solar\.Classes\.ShadingElement') class with a specified unique identifier and properties\.

```csharp
public ShadingElement(System.Guid guid, string? reference, DiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D? polygonalFace3D, bool shadingOnly);
```
#### Parameters

<a name='DiGi.Solar.Classes.ShadingElement.ShadingElement(System.Guid,string,DiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D,bool).guid'></a>

`guid` [System\.Guid](https://learn.microsoft.com/en-us/dotnet/api/system.guid 'System\.Guid')

The unique identifier for the object\.

<a name='DiGi.Solar.Classes.ShadingElement.ShadingElement(System.Guid,string,DiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D,bool).reference'></a>

`reference` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The reference string identifying the element\.

<a name='DiGi.Solar.Classes.ShadingElement.ShadingElement(System.Guid,string,DiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D,bool).polygonalFace3D'></a>

`polygonalFace3D` [DiGi\.Geometry\.Spatial\.Interfaces\.IPolygonalFace3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.interfaces.ipolygonalface3d 'DiGi\.Geometry\.Spatial\.Interfaces\.IPolygonalFace3D')

The 3D polygonal face representing the geometry of the shading element\.

<a name='DiGi.Solar.Classes.ShadingElement.ShadingElement(System.Guid,string,DiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D,bool).shadingOnly'></a>

`shadingOnly` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A flag indicating if the element is used exclusively for shading calculations\.

<a name='DiGi.Solar.Classes.ShadingElement.ShadingElement(System.Text.Json.Nodes.JsonObject)'></a>

## ShadingElement\(JsonObject\) Constructor

Initializes a new instance of the [ShadingElement](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingElement 'DiGi\.Solar\.Classes\.ShadingElement') class from a JSON object\.

```csharp
public ShadingElement(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.Solar.Classes.ShadingElement.ShadingElement(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject') containing the element data\.
### Properties

<a name='DiGi.Solar.Classes.ShadingElement.PolygonalFace3D'></a>

## ShadingElement\.PolygonalFace3D Property

Gets the 3D polygonal face associated with this shading element\.

```csharp
public DiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D? PolygonalFace3D { get; }
```

Implements [PolygonalFace3D](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingElement.PolygonalFace3D 'DiGi\.Solar\.Interfaces\.IShadingElement\.PolygonalFace3D')

#### Property Value
[DiGi\.Geometry\.Spatial\.Interfaces\.IPolygonalFace3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.interfaces.ipolygonalface3d 'DiGi\.Geometry\.Spatial\.Interfaces\.IPolygonalFace3D')

<a name='DiGi.Solar.Classes.ShadingElement.Reference'></a>

## ShadingElement\.Reference Property

Gets the reference identifier of the shading element\.

```csharp
public string? Reference { get; }
```

Implements [Reference](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingElement.Reference 'DiGi\.Solar\.Interfaces\.IShadingElement\.Reference')

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.Solar.Classes.ShadingElement.ShadingOnly'></a>

## ShadingElement\.ShadingOnly Property

Gets a value indicating whether the element is used exclusively for shading calculations\.

```csharp
public bool ShadingOnly { get; }
```

Implements [ShadingOnly](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingElement.ShadingOnly 'DiGi\.Solar\.Interfaces\.IShadingElement\.ShadingOnly')

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.Solar.Classes.ShadingModel'></a>

## ShadingModel Class

Represents a shading model that manages the relationship between shading elements and their corresponding solver results\.

```csharp
public class ShadingModel : DiGi.Core.Classes.GuidObject, DiGi.Solar.Interfaces.ISolarObject, DiGi.Core.Interfaces.IObject, DiGi.Core.Interfaces.IGuidModel, DiGi.Core.Interfaces.ISerializableModel, DiGi.Core.Interfaces.IModel, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject, DiGi.Core.Interfaces.IGuidObject, DiGi.Core.Interfaces.IUniqueObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.Core\.Classes\.UniqueObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.uniqueobject 'DiGi\.Core\.Classes\.UniqueObject') → [DiGi\.Core\.Classes\.GuidObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.guidobject 'DiGi\.Core\.Classes\.GuidObject') → ShadingModel

Implements [ISolarObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.ISolarObject 'DiGi\.Solar\.Interfaces\.ISolarObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Core\.Interfaces\.IGuidModel](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iguidmodel 'DiGi\.Core\.Interfaces\.IGuidModel'), [DiGi\.Core\.Interfaces\.ISerializableModel](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializablemodel 'DiGi\.Core\.Interfaces\.ISerializableModel'), [DiGi\.Core\.Interfaces\.IModel](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.imodel 'DiGi\.Core\.Interfaces\.IModel'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject'), [DiGi\.Core\.Interfaces\.IGuidObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iguidobject 'DiGi\.Core\.Interfaces\.IGuidObject'), [DiGi\.Core\.Interfaces\.IUniqueObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iuniqueobject 'DiGi\.Core\.Interfaces\.IUniqueObject')
### Constructors

<a name='DiGi.Solar.Classes.ShadingModel.ShadingModel(DiGi.Core.Enums.UTC,DiGi.Core.Classes.Coordinates)'></a>

## ShadingModel\(UTC, Coordinates\) Constructor

Initializes a new instance of the [ShadingModel](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingModel 'DiGi\.Solar\.Classes\.ShadingModel') class with the specified UTC offset and coordinates\.

```csharp
public ShadingModel(DiGi.Core.Enums.UTC uTC, DiGi.Core.Classes.Coordinates? coordinates);
```
#### Parameters

<a name='DiGi.Solar.Classes.ShadingModel.ShadingModel(DiGi.Core.Enums.UTC,DiGi.Core.Classes.Coordinates).uTC'></a>

`uTC` [DiGi\.Core\.Enums\.UTC](https://learn.microsoft.com/en-us/dotnet/api/digi.core.enums.utc 'DiGi\.Core\.Enums\.UTC')

The UTC time zone offset\.

<a name='DiGi.Solar.Classes.ShadingModel.ShadingModel(DiGi.Core.Enums.UTC,DiGi.Core.Classes.Coordinates).coordinates'></a>

`coordinates` [DiGi\.Core\.Classes\.Coordinates](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.coordinates 'DiGi\.Core\.Classes\.Coordinates')

The geographic coordinates associated with the model\.

<a name='DiGi.Solar.Classes.ShadingModel.ShadingModel(DiGi.Solar.Classes.ShadingModel)'></a>

## ShadingModel\(ShadingModel\) Constructor

Initializes a new instance of the [ShadingModel](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingModel 'DiGi\.Solar\.Classes\.ShadingModel') class by cloning an existing shading model\.

```csharp
public ShadingModel(DiGi.Solar.Classes.ShadingModel shadingModel);
```
#### Parameters

<a name='DiGi.Solar.Classes.ShadingModel.ShadingModel(DiGi.Solar.Classes.ShadingModel).shadingModel'></a>

`shadingModel` [ShadingModel](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingModel 'DiGi\.Solar\.Classes\.ShadingModel')

The source shading model to clone\.

<a name='DiGi.Solar.Classes.ShadingModel.ShadingModel(System.Text.Json.Nodes.JsonObject)'></a>

## ShadingModel\(JsonObject\) Constructor

Initializes a new instance of the [ShadingModel](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingModel 'DiGi\.Solar\.Classes\.ShadingModel') class from a JSON object\.

```csharp
public ShadingModel(System.Text.Json.Nodes.JsonObject jsonObject);
```
#### Parameters

<a name='DiGi.Solar.Classes.ShadingModel.ShadingModel(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JSON object containing the model data\.
### Properties

<a name='DiGi.Solar.Classes.ShadingModel.Coordinates'></a>

## ShadingModel\.Coordinates Property

Gets the geographic coordinates associated with the shading model\.

```csharp
public DiGi.Core.Classes.Coordinates? Coordinates { get; }
```

#### Property Value
[DiGi\.Core\.Classes\.Coordinates](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.coordinates 'DiGi\.Core\.Classes\.Coordinates')

<a name='DiGi.Solar.Classes.ShadingModel.UTC'></a>

## ShadingModel\.UTC Property

Gets the UTC time zone offset for the shading model\.

```csharp
public DiGi.Core.Enums.UTC UTC { get; }
```

#### Property Value
[DiGi\.Core\.Enums\.UTC](https://learn.microsoft.com/en-us/dotnet/api/digi.core.enums.utc 'DiGi\.Core\.Enums\.UTC')
### Methods

<a name='DiGi.Solar.Classes.ShadingModel.Assign(DiGi.Solar.Interfaces.IShadingElement,System.Collections.Generic.IEnumerable_DiGi.Solar.Interfaces.IShadingSolverResult_)'></a>

## ShadingModel\.Assign\(IShadingElement, IEnumerable\<IShadingSolverResult\>\) Method

Assigns a shading element and its corresponding solver results to the model's relation cluster\.

```csharp
public bool Assign(DiGi.Solar.Interfaces.IShadingElement? shadingElement, System.Collections.Generic.IEnumerable<DiGi.Solar.Interfaces.IShadingSolverResult>? shadingSolverResults);
```
#### Parameters

<a name='DiGi.Solar.Classes.ShadingModel.Assign(DiGi.Solar.Interfaces.IShadingElement,System.Collections.Generic.IEnumerable_DiGi.Solar.Interfaces.IShadingSolverResult_).shadingElement'></a>

`shadingElement` [IShadingElement](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingElement 'DiGi\.Solar\.Interfaces\.IShadingElement')

The shading element to assign\.

<a name='DiGi.Solar.Classes.ShadingModel.Assign(DiGi.Solar.Interfaces.IShadingElement,System.Collections.Generic.IEnumerable_DiGi.Solar.Interfaces.IShadingSolverResult_).shadingSolverResults'></a>

`shadingSolverResults` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[IShadingSolverResult](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSolverResult 'DiGi\.Solar\.Interfaces\.IShadingSolverResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of solver results associated with the element\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True if the assignment was successful; otherwise, false\.

<a name='DiGi.Solar.Classes.ShadingModel.GetShadingElements_TShadingElement_(System.Nullable_bool_)'></a>

## ShadingModel\.GetShadingElements\<TShadingElement\>\(Nullable\<bool\>\) Method

Retrieves a list of shading elements of the specified type, optionally filtered by their shading\-only status\.

```csharp
public System.Collections.Generic.List<TShadingElement>? GetShadingElements<TShadingElement>(System.Nullable<bool> shadingOnly=null)
    where TShadingElement : DiGi.Solar.Interfaces.IShadingElement;
```
#### Type parameters

<a name='DiGi.Solar.Classes.ShadingModel.GetShadingElements_TShadingElement_(System.Nullable_bool_).TShadingElement'></a>

`TShadingElement`

The type of shading element to retrieve\.
#### Parameters

<a name='DiGi.Solar.Classes.ShadingModel.GetShadingElements_TShadingElement_(System.Nullable_bool_).shadingOnly'></a>

`shadingOnly` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

Optional filter to include only elements that are marked as shading only\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[TShadingElement](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingModel.GetShadingElements_TShadingElement_(System.Nullable_bool_).TShadingElement 'DiGi\.Solar\.Classes\.ShadingModel\.GetShadingElements\<TShadingElement\>\(System\.Nullable\<bool\>\)\.TShadingElement')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A list of matching shading elements, or null if none were found\.

<a name='DiGi.Solar.Classes.ShadingModel.GetShadingSolverResults_TShadingSolverResult_()'></a>

## ShadingModel\.GetShadingSolverResults\<TShadingSolverResult\>\(\) Method

Retrieves all shading solver results of the specified type\.

```csharp
public System.Collections.Generic.List<TShadingSolverResult>? GetShadingSolverResults<TShadingSolverResult>()
    where TShadingSolverResult : DiGi.Solar.Interfaces.IShadingSolverResult;
```
#### Type parameters

<a name='DiGi.Solar.Classes.ShadingModel.GetShadingSolverResults_TShadingSolverResult_().TShadingSolverResult'></a>

`TShadingSolverResult`

The type of solver result to retrieve\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[TShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingModel.GetShadingSolverResults_TShadingSolverResult_().TShadingSolverResult 'DiGi\.Solar\.Classes\.ShadingModel\.GetShadingSolverResults\<TShadingSolverResult\>\(\)\.TShadingSolverResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A list of matching solver results, or null if none were found\.

<a name='DiGi.Solar.Classes.ShadingModel.GetShadingSolverResults_TShadingSolverResult_(DiGi.Solar.Interfaces.IShadingElement)'></a>

## ShadingModel\.GetShadingSolverResults\<TShadingSolverResult\>\(IShadingElement\) Method

Retrieves shading solver results of the specified type for a specific shading element\.

```csharp
public System.Collections.Generic.List<TShadingSolverResult>? GetShadingSolverResults<TShadingSolverResult>(DiGi.Solar.Interfaces.IShadingElement? shadingElement)
    where TShadingSolverResult : DiGi.Solar.Interfaces.IShadingSolverResult;
```
#### Type parameters

<a name='DiGi.Solar.Classes.ShadingModel.GetShadingSolverResults_TShadingSolverResult_(DiGi.Solar.Interfaces.IShadingElement).TShadingSolverResult'></a>

`TShadingSolverResult`

The type of solver result to retrieve\.
#### Parameters

<a name='DiGi.Solar.Classes.ShadingModel.GetShadingSolverResults_TShadingSolverResult_(DiGi.Solar.Interfaces.IShadingElement).shadingElement'></a>

`shadingElement` [IShadingElement](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingElement 'DiGi\.Solar\.Interfaces\.IShadingElement')

The shading element associated with the results\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[TShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingModel.GetShadingSolverResults_TShadingSolverResult_(DiGi.Solar.Interfaces.IShadingElement).TShadingSolverResult 'DiGi\.Solar\.Classes\.ShadingModel\.GetShadingSolverResults\<TShadingSolverResult\>\(DiGi\.Solar\.Interfaces\.IShadingElement\)\.TShadingSolverResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A list of matching solver results, or null if the element is null or no results were found\.

<a name='DiGi.Solar.Classes.ShadingModel.TryGetShadingFactor(DiGi.Solar.Interfaces.IShadingElement,System.DateTime,double,bool)'></a>

## ShadingModel\.TryGetShadingFactor\(IShadingElement, DateTime, double, bool\) Method

Attempts to calculate the shading factor for a specific element at a given date and time\.

```csharp
public bool TryGetShadingFactor(DiGi.Solar.Interfaces.IShadingElement shadingElement, System.DateTime dateTime, out double factor, bool interpolation=true);
```
#### Parameters

<a name='DiGi.Solar.Classes.ShadingModel.TryGetShadingFactor(DiGi.Solar.Interfaces.IShadingElement,System.DateTime,double,bool).shadingElement'></a>

`shadingElement` [IShadingElement](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingElement 'DiGi\.Solar\.Interfaces\.IShadingElement')

The shading element to evaluate\.

<a name='DiGi.Solar.Classes.ShadingModel.TryGetShadingFactor(DiGi.Solar.Interfaces.IShadingElement,System.DateTime,double,bool).dateTime'></a>

`dateTime` [System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime')

The date and time of evaluation\.

<a name='DiGi.Solar.Classes.ShadingModel.TryGetShadingFactor(DiGi.Solar.Interfaces.IShadingElement,System.DateTime,double,bool).factor'></a>

`factor` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

When this method returns, contains the calculated shading factor if successful; otherwise, [System\.Double\.NaN](https://learn.microsoft.com/en-us/dotnet/api/system.double.nan 'System\.Double\.NaN')\.

<a name='DiGi.Solar.Classes.ShadingModel.TryGetShadingFactor(DiGi.Solar.Interfaces.IShadingElement,System.DateTime,double,bool).interpolation'></a>

`interpolation` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

Whether to interpolate between known results if an exact match for the date and time is not found\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True if a shading factor was successfully determined; otherwise, false\.

<a name='DiGi.Solar.Classes.ShadingModel.Update(DiGi.Solar.Interfaces.IShadingElement)'></a>

## ShadingModel\.Update\(IShadingElement\) Method

Updates or adds a shading element to the model's relation cluster\.

```csharp
public bool Update(DiGi.Solar.Interfaces.IShadingElement shadingElement);
```
#### Parameters

<a name='DiGi.Solar.Classes.ShadingModel.Update(DiGi.Solar.Interfaces.IShadingElement).shadingElement'></a>

`shadingElement` [IShadingElement](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingElement 'DiGi\.Solar\.Interfaces\.IShadingElement')

The shading element to update\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True if the element was successfully added or updated; otherwise, false\.

<a name='DiGi.Solar.Classes.ShadingModel.Update(DiGi.Solar.Interfaces.IShadingSolverResult)'></a>

## ShadingModel\.Update\(IShadingSolverResult\) Method

Updates or adds a shading solver result to the model's relation cluster\.

```csharp
public bool Update(DiGi.Solar.Interfaces.IShadingSolverResult shadingSolverResult);
```
#### Parameters

<a name='DiGi.Solar.Classes.ShadingModel.Update(DiGi.Solar.Interfaces.IShadingSolverResult).shadingSolverResult'></a>

`shadingSolverResult` [IShadingSolverResult](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSolverResult 'DiGi\.Solar\.Interfaces\.IShadingSolverResult')

The shading solver result to update\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True if the solver result was successfully added or updated; otherwise, false\.

<a name='DiGi.Solar.Classes.ShadingRelationCluster'></a>

## ShadingRelationCluster Class

Represents a cluster of shading relations, associating shading unique objects with their corresponding shading relations\.

```csharp
public class ShadingRelationCluster : DiGi.Core.Relation.Classes.UniqueObjectRelationCluster<DiGi.Solar.Interfaces.IShadingUniqueObject, DiGi.Solar.Interfaces.IShadingRelation>, DiGi.Solar.Interfaces.ISolarObject, DiGi.Core.Interfaces.IObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Cluster&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.cluster-3 'DiGi\.Core\.Classes\.Cluster\`3')[DiGi\.Core\.Classes\.TypeReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.typereference 'DiGi\.Core\.Classes\.TypeReference')[,](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.cluster-3 'DiGi\.Core\.Classes\.Cluster\`3')[DiGi\.Core\.Interfaces\.IUniqueReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iuniquereference 'DiGi\.Core\.Interfaces\.IUniqueReference')[,](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.cluster-3 'DiGi\.Core\.Classes\.Cluster\`3')[IShadingUniqueObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingUniqueObject 'DiGi\.Solar\.Interfaces\.IShadingUniqueObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.cluster-3 'DiGi\.Core\.Classes\.Cluster\`3') → [DiGi\.Core\.Classes\.ValueCluster&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.valuecluster-3 'DiGi\.Core\.Classes\.ValueCluster\`3')[DiGi\.Core\.Classes\.TypeReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.typereference 'DiGi\.Core\.Classes\.TypeReference')[,](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.valuecluster-3 'DiGi\.Core\.Classes\.ValueCluster\`3')[DiGi\.Core\.Interfaces\.IUniqueReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iuniquereference 'DiGi\.Core\.Interfaces\.IUniqueReference')[,](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.valuecluster-3 'DiGi\.Core\.Classes\.ValueCluster\`3')[IShadingUniqueObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingUniqueObject 'DiGi\.Solar\.Interfaces\.IShadingUniqueObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.valuecluster-3 'DiGi\.Core\.Classes\.ValueCluster\`3') → [DiGi\.Core\.Classes\.SerializableObjectValueCluster&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobjectvaluecluster-3 'DiGi\.Core\.Classes\.SerializableObjectValueCluster\`3')[DiGi\.Core\.Classes\.TypeReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.typereference 'DiGi\.Core\.Classes\.TypeReference')[,](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobjectvaluecluster-3 'DiGi\.Core\.Classes\.SerializableObjectValueCluster\`3')[DiGi\.Core\.Interfaces\.IUniqueReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iuniquereference 'DiGi\.Core\.Interfaces\.IUniqueReference')[,](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobjectvaluecluster-3 'DiGi\.Core\.Classes\.SerializableObjectValueCluster\`3')[IShadingUniqueObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingUniqueObject 'DiGi\.Solar\.Interfaces\.IShadingUniqueObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobjectvaluecluster-3 'DiGi\.Core\.Classes\.SerializableObjectValueCluster\`3') → [DiGi\.Core\.Classes\.UniqueObjectValueCluster&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.uniqueobjectvaluecluster-1 'DiGi\.Core\.Classes\.UniqueObjectValueCluster\`1')[IShadingUniqueObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingUniqueObject 'DiGi\.Solar\.Interfaces\.IShadingUniqueObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.uniqueobjectvaluecluster-1 'DiGi\.Core\.Classes\.UniqueObjectValueCluster\`1') → [DiGi\.Core\.Relation\.Classes\.UniqueObjectRelationCluster&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.relation.classes.uniqueobjectrelationcluster-2 'DiGi\.Core\.Relation\.Classes\.UniqueObjectRelationCluster\`2')[IShadingUniqueObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingUniqueObject 'DiGi\.Solar\.Interfaces\.IShadingUniqueObject')[,](https://learn.microsoft.com/en-us/dotnet/api/digi.core.relation.classes.uniqueobjectrelationcluster-2 'DiGi\.Core\.Relation\.Classes\.UniqueObjectRelationCluster\`2')[IShadingRelation](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingRelation 'DiGi\.Solar\.Interfaces\.IShadingRelation')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.relation.classes.uniqueobjectrelationcluster-2 'DiGi\.Core\.Relation\.Classes\.UniqueObjectRelationCluster\`2') → ShadingRelationCluster

Implements [ISolarObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.ISolarObject 'DiGi\.Solar\.Interfaces\.ISolarObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')
### Constructors

<a name='DiGi.Solar.Classes.ShadingRelationCluster.ShadingRelationCluster()'></a>

## ShadingRelationCluster\(\) Constructor

Initializes a new instance of the [ShadingRelationCluster](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingRelationCluster 'DiGi\.Solar\.Classes\.ShadingRelationCluster') class\.

```csharp
public ShadingRelationCluster();
```

<a name='DiGi.Solar.Classes.ShadingRelationCluster.ShadingRelationCluster(DiGi.Solar.Classes.ShadingRelationCluster)'></a>

## ShadingRelationCluster\(ShadingRelationCluster\) Constructor

Initializes a new instance of the [ShadingRelationCluster](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingRelationCluster 'DiGi\.Solar\.Classes\.ShadingRelationCluster') class by copying another shading relation cluster\.

```csharp
public ShadingRelationCluster(DiGi.Solar.Classes.ShadingRelationCluster? shadingRelationCluster);
```
#### Parameters

<a name='DiGi.Solar.Classes.ShadingRelationCluster.ShadingRelationCluster(DiGi.Solar.Classes.ShadingRelationCluster).shadingRelationCluster'></a>

`shadingRelationCluster` [ShadingRelationCluster](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingRelationCluster 'DiGi\.Solar\.Classes\.ShadingRelationCluster')

The source shading relation cluster to copy from\.

<a name='DiGi.Solar.Classes.ShadingRelationCluster.ShadingRelationCluster(System.Text.Json.Nodes.JsonObject)'></a>

## ShadingRelationCluster\(JsonObject\) Constructor

Initializes a new instance of the [ShadingRelationCluster](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingRelationCluster 'DiGi\.Solar\.Classes\.ShadingRelationCluster') class from the specified JSON object\.

```csharp
public ShadingRelationCluster(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.Solar.Classes.ShadingRelationCluster.ShadingRelationCluster(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JSON object containing the cluster data\.
### Methods

<a name='DiGi.Solar.Classes.ShadingRelationCluster.AddRelation(DiGi.Solar.Interfaces.IShadingElement,System.Collections.Generic.IEnumerable_DiGi.Solar.Interfaces.IShadingSolverResult_)'></a>

## ShadingRelationCluster\.AddRelation\(IShadingElement, IEnumerable\<IShadingSolverResult\>\) Method

Adds a shading solver result relation for the specified shading element and results\.

```csharp
public DiGi.Solar.Classes.ShadingSolverResultRelation? AddRelation(DiGi.Solar.Interfaces.IShadingElement? shadingElement, System.Collections.Generic.IEnumerable<DiGi.Solar.Interfaces.IShadingSolverResult>? shadingSolverResults);
```
#### Parameters

<a name='DiGi.Solar.Classes.ShadingRelationCluster.AddRelation(DiGi.Solar.Interfaces.IShadingElement,System.Collections.Generic.IEnumerable_DiGi.Solar.Interfaces.IShadingSolverResult_).shadingElement'></a>

`shadingElement` [IShadingElement](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingElement 'DiGi\.Solar\.Interfaces\.IShadingElement')

The shading element associated with the relation\.

<a name='DiGi.Solar.Classes.ShadingRelationCluster.AddRelation(DiGi.Solar.Interfaces.IShadingElement,System.Collections.Generic.IEnumerable_DiGi.Solar.Interfaces.IShadingSolverResult_).shadingSolverResults'></a>

`shadingSolverResults` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[IShadingSolverResult](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSolverResult 'DiGi\.Solar\.Interfaces\.IShadingSolverResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The collection of shading solver results to associate\.

#### Returns
[ShadingSolverResultRelation](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolverResultRelation 'DiGi\.Solar\.Classes\.ShadingSolverResultRelation')  
The newly created [ShadingSolverResultRelation](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolverResultRelation 'DiGi\.Solar\.Classes\.ShadingSolverResultRelation'), or null if any input is null\.

<a name='DiGi.Solar.Classes.ShadingRelationCluster.GetShadingSolverResults_TShadingSolverResult_(DiGi.Solar.Classes.ShadingSolverResultRelation)'></a>

## ShadingRelationCluster\.GetShadingSolverResults\<TShadingSolverResult\>\(ShadingSolverResultRelation\) Method

Retrieves a list of shading solver results of the specified type from the given relation\.

```csharp
public System.Collections.Generic.List<TShadingSolverResult>? GetShadingSolverResults<TShadingSolverResult>(DiGi.Solar.Classes.ShadingSolverResultRelation? shadingSolverResultRelation)
    where TShadingSolverResult : DiGi.Solar.Interfaces.IShadingSolverResult;
```
#### Type parameters

<a name='DiGi.Solar.Classes.ShadingRelationCluster.GetShadingSolverResults_TShadingSolverResult_(DiGi.Solar.Classes.ShadingSolverResultRelation).TShadingSolverResult'></a>

`TShadingSolverResult`

The specific type of shading solver result to retrieve\.
#### Parameters

<a name='DiGi.Solar.Classes.ShadingRelationCluster.GetShadingSolverResults_TShadingSolverResult_(DiGi.Solar.Classes.ShadingSolverResultRelation).shadingSolverResultRelation'></a>

`shadingSolverResultRelation` [ShadingSolverResultRelation](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolverResultRelation 'DiGi\.Solar\.Classes\.ShadingSolverResultRelation')

The relation containing the references to the solver results\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[TShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingRelationCluster.GetShadingSolverResults_TShadingSolverResult_(DiGi.Solar.Classes.ShadingSolverResultRelation).TShadingSolverResult 'DiGi\.Solar\.Classes\.ShadingRelationCluster\.GetShadingSolverResults\<TShadingSolverResult\>\(DiGi\.Solar\.Classes\.ShadingSolverResultRelation\)\.TShadingSolverResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A list of [TShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingRelationCluster.GetShadingSolverResults_TShadingSolverResult_(DiGi.Solar.Classes.ShadingSolverResultRelation).TShadingSolverResult 'DiGi\.Solar\.Classes\.ShadingRelationCluster\.GetShadingSolverResults\<TShadingSolverResult\>\(DiGi\.Solar\.Classes\.ShadingSolverResultRelation\)\.TShadingSolverResult') instances, or null if no results are found\.

<a name='DiGi.Solar.Classes.ShadingSolverResult'></a>

## ShadingSolverResult Class

Represents the result of a shading solver calculation\.

```csharp
public abstract class ShadingSolverResult : DiGi.Core.Classes.GuidResult<DiGi.Solar.Interfaces.IShadingElement>, DiGi.Solar.Interfaces.IShadingSolverResult, DiGi.Solar.Interfaces.IShadingUniqueObject, DiGi.Solar.Interfaces.IShadingSerializableObject, DiGi.Solar.Interfaces.IShadingObject, DiGi.Solar.Interfaces.ISolarObject, DiGi.Core.Interfaces.IObject, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject, DiGi.Core.Interfaces.IUniqueObject, DiGi.Core.Interfaces.IResult<DiGi.Solar.Interfaces.IShadingElement>, DiGi.Core.Interfaces.IResult
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.Core\.Classes\.UniqueObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.uniqueobject 'DiGi\.Core\.Classes\.UniqueObject') → [DiGi\.Core\.Classes\.GuidObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.guidobject 'DiGi\.Core\.Classes\.GuidObject') → [DiGi\.Core\.Classes\.GuidResult](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.guidresult 'DiGi\.Core\.Classes\.GuidResult') → [DiGi\.Core\.Classes\.GuidResult&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.guidresult-1 'DiGi\.Core\.Classes\.GuidResult\`1')[IShadingElement](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingElement 'DiGi\.Solar\.Interfaces\.IShadingElement')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.guidresult-1 'DiGi\.Core\.Classes\.GuidResult\`1') → ShadingSolverResult

Derived  
↳ [GeometricalShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.GeometricalShadingSolverResult 'DiGi\.Solar\.Classes\.GeometricalShadingSolverResult')  
↳ [NumericalShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.NumericalShadingSolverResult 'DiGi\.Solar\.Classes\.NumericalShadingSolverResult')

Implements [IShadingSolverResult](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSolverResult 'DiGi\.Solar\.Interfaces\.IShadingSolverResult'), [IShadingUniqueObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingUniqueObject 'DiGi\.Solar\.Interfaces\.IShadingUniqueObject'), [IShadingSerializableObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSerializableObject 'DiGi\.Solar\.Interfaces\.IShadingSerializableObject'), [IShadingObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingObject 'DiGi\.Solar\.Interfaces\.IShadingObject'), [ISolarObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.ISolarObject 'DiGi\.Solar\.Interfaces\.ISolarObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject'), [DiGi\.Core\.Interfaces\.IUniqueObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iuniqueobject 'DiGi\.Core\.Interfaces\.IUniqueObject'), [DiGi\.Core\.Interfaces\.IResult&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iresult-1 'DiGi\.Core\.Interfaces\.IResult\`1')[IShadingElement](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingElement 'DiGi\.Solar\.Interfaces\.IShadingElement')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iresult-1 'DiGi\.Core\.Interfaces\.IResult\`1'), [DiGi\.Core\.Interfaces\.IResult](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iresult 'DiGi\.Core\.Interfaces\.IResult')
### Constructors

<a name='DiGi.Solar.Classes.ShadingSolverResult.ShadingSolverResult(DiGi.Solar.Classes.ShadingSolverResult)'></a>

## ShadingSolverResult\(ShadingSolverResult\) Constructor

Initializes a new instance of the [ShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolverResult 'DiGi\.Solar\.Classes\.ShadingSolverResult') class by copying an existing shading solver result\.

```csharp
public ShadingSolverResult(DiGi.Solar.Classes.ShadingSolverResult? shadingSolverResult);
```
#### Parameters

<a name='DiGi.Solar.Classes.ShadingSolverResult.ShadingSolverResult(DiGi.Solar.Classes.ShadingSolverResult).shadingSolverResult'></a>

`shadingSolverResult` [ShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolverResult 'DiGi\.Solar\.Classes\.ShadingSolverResult')

The source shading solver result to copy from\.

<a name='DiGi.Solar.Classes.ShadingSolverResult.ShadingSolverResult(System.DateTime)'></a>

## ShadingSolverResult\(DateTime\) Constructor

Initializes a new instance of the [ShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolverResult 'DiGi\.Solar\.Classes\.ShadingSolverResult') class with a specified date and time\.

```csharp
public ShadingSolverResult(System.DateTime dateTime);
```
#### Parameters

<a name='DiGi.Solar.Classes.ShadingSolverResult.ShadingSolverResult(System.DateTime).dateTime'></a>

`dateTime` [System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime')

The date and time associated with the shading solver result\.

<a name='DiGi.Solar.Classes.ShadingSolverResult.ShadingSolverResult(System.Text.Json.Nodes.JsonObject)'></a>

## ShadingSolverResult\(JsonObject\) Constructor

Initializes a new instance of the [ShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolverResult 'DiGi\.Solar\.Classes\.ShadingSolverResult') class from a JSON object\.

```csharp
public ShadingSolverResult(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.Solar.Classes.ShadingSolverResult.ShadingSolverResult(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JSON object containing the result data\.
### Properties

<a name='DiGi.Solar.Classes.ShadingSolverResult.Area'></a>

## ShadingSolverResult\.Area Property

Gets the area associated with the shading solver result\.

```csharp
public abstract double Area { get; }
```

Implements [Area](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSolverResult.Area 'DiGi\.Solar\.Interfaces\.IShadingSolverResult\.Area')

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.Solar.Classes.ShadingSolverResult.DateTime'></a>

## ShadingSolverResult\.DateTime Property

Gets the date and time associated with the shading solver result\.

```csharp
public System.DateTime DateTime { get; }
```

Implements [DateTime](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSolverResult.DateTime 'DiGi\.Solar\.Interfaces\.IShadingSolverResult\.DateTime')

#### Property Value
[System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime')

<a name='DiGi.Solar.Classes.ShadingSolverResultRelation'></a>

## ShadingSolverResultRelation Class

Represents a one\-to\-many directional relationship between a shading element and its corresponding shading solver results\.

```csharp
public class ShadingSolverResultRelation : DiGi.Core.Relation.Classes.OneToManyDirectionalRelation<DiGi.Solar.Interfaces.IShadingElement, DiGi.Solar.Interfaces.IShadingSolverResult>, DiGi.Solar.Interfaces.IShadingRelation, DiGi.Solar.Interfaces.IShadingObject, DiGi.Solar.Interfaces.ISolarObject, DiGi.Core.Interfaces.IObject, DiGi.Core.Relation.Interfaces.IRelation, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.Core\.Relation\.Classes\.Relation](https://learn.microsoft.com/en-us/dotnet/api/digi.core.relation.classes.relation 'DiGi\.Core\.Relation\.Classes\.Relation') → [DiGi\.Core\.Relation\.Classes\.Relation&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.relation.classes.relation-2 'DiGi\.Core\.Relation\.Classes\.Relation\`2')[IShadingElement](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingElement 'DiGi\.Solar\.Interfaces\.IShadingElement')[,](https://learn.microsoft.com/en-us/dotnet/api/digi.core.relation.classes.relation-2 'DiGi\.Core\.Relation\.Classes\.Relation\`2')[IShadingSolverResult](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSolverResult 'DiGi\.Solar\.Interfaces\.IShadingSolverResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.relation.classes.relation-2 'DiGi\.Core\.Relation\.Classes\.Relation\`2') → [DiGi\.Core\.Relation\.Classes\.OneToManyRelation&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.relation.classes.onetomanyrelation-2 'DiGi\.Core\.Relation\.Classes\.OneToManyRelation\`2')[IShadingElement](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingElement 'DiGi\.Solar\.Interfaces\.IShadingElement')[,](https://learn.microsoft.com/en-us/dotnet/api/digi.core.relation.classes.onetomanyrelation-2 'DiGi\.Core\.Relation\.Classes\.OneToManyRelation\`2')[IShadingSolverResult](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSolverResult 'DiGi\.Solar\.Interfaces\.IShadingSolverResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.relation.classes.onetomanyrelation-2 'DiGi\.Core\.Relation\.Classes\.OneToManyRelation\`2') → [DiGi\.Core\.Relation\.Classes\.OneToManyDirectionalRelation&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.relation.classes.onetomanydirectionalrelation-2 'DiGi\.Core\.Relation\.Classes\.OneToManyDirectionalRelation\`2')[IShadingElement](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingElement 'DiGi\.Solar\.Interfaces\.IShadingElement')[,](https://learn.microsoft.com/en-us/dotnet/api/digi.core.relation.classes.onetomanydirectionalrelation-2 'DiGi\.Core\.Relation\.Classes\.OneToManyDirectionalRelation\`2')[IShadingSolverResult](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSolverResult 'DiGi\.Solar\.Interfaces\.IShadingSolverResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.relation.classes.onetomanydirectionalrelation-2 'DiGi\.Core\.Relation\.Classes\.OneToManyDirectionalRelation\`2') → ShadingSolverResultRelation

Implements [IShadingRelation](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingRelation 'DiGi\.Solar\.Interfaces\.IShadingRelation'), [IShadingObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingObject 'DiGi\.Solar\.Interfaces\.IShadingObject'), [ISolarObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.ISolarObject 'DiGi\.Solar\.Interfaces\.ISolarObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Core\.Relation\.Interfaces\.IRelation](https://learn.microsoft.com/en-us/dotnet/api/digi.core.relation.interfaces.irelation 'DiGi\.Core\.Relation\.Interfaces\.IRelation'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject')
### Constructors

<a name='DiGi.Solar.Classes.ShadingSolverResultRelation.ShadingSolverResultRelation(DiGi.Solar.Classes.ShadingSolverResultRelation)'></a>

## ShadingSolverResultRelation\(ShadingSolverResultRelation\) Constructor

Initializes a new instance of the [ShadingSolverResultRelation](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolverResultRelation 'DiGi\.Solar\.Classes\.ShadingSolverResultRelation') class by copying an existing relation\.

```csharp
public ShadingSolverResultRelation(DiGi.Solar.Classes.ShadingSolverResultRelation shadingSolverResultRelation);
```
#### Parameters

<a name='DiGi.Solar.Classes.ShadingSolverResultRelation.ShadingSolverResultRelation(DiGi.Solar.Classes.ShadingSolverResultRelation).shadingSolverResultRelation'></a>

`shadingSolverResultRelation` [ShadingSolverResultRelation](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolverResultRelation 'DiGi\.Solar\.Classes\.ShadingSolverResultRelation')

The source relation to copy\.

<a name='DiGi.Solar.Classes.ShadingSolverResultRelation.ShadingSolverResultRelation(DiGi.Solar.Interfaces.IShadingElement,DiGi.Solar.Interfaces.IShadingSolverResult)'></a>

## ShadingSolverResultRelation\(IShadingElement, IShadingSolverResult\) Constructor

Initializes a new instance of the [ShadingSolverResultRelation](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolverResultRelation 'DiGi\.Solar\.Classes\.ShadingSolverResultRelation') class with a specific shading element and a single solver result\.

```csharp
public ShadingSolverResultRelation(DiGi.Solar.Interfaces.IShadingElement shadingElement, DiGi.Solar.Interfaces.IShadingSolverResult shadingSolverResult);
```
#### Parameters

<a name='DiGi.Solar.Classes.ShadingSolverResultRelation.ShadingSolverResultRelation(DiGi.Solar.Interfaces.IShadingElement,DiGi.Solar.Interfaces.IShadingSolverResult).shadingElement'></a>

`shadingElement` [IShadingElement](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingElement 'DiGi\.Solar\.Interfaces\.IShadingElement')

The parent shading element\.

<a name='DiGi.Solar.Classes.ShadingSolverResultRelation.ShadingSolverResultRelation(DiGi.Solar.Interfaces.IShadingElement,DiGi.Solar.Interfaces.IShadingSolverResult).shadingSolverResult'></a>

`shadingSolverResult` [IShadingSolverResult](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSolverResult 'DiGi\.Solar\.Interfaces\.IShadingSolverResult')

The related shading solver result\.

<a name='DiGi.Solar.Classes.ShadingSolverResultRelation.ShadingSolverResultRelation(DiGi.Solar.Interfaces.IShadingElement,System.Collections.Generic.IEnumerable_DiGi.Solar.Interfaces.IShadingSolverResult_)'></a>

## ShadingSolverResultRelation\(IShadingElement, IEnumerable\<IShadingSolverResult\>\) Constructor

Initializes a new instance of the [ShadingSolverResultRelation](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolverResultRelation 'DiGi\.Solar\.Classes\.ShadingSolverResultRelation') class with a specific shading element and a collection of solver results\.

```csharp
public ShadingSolverResultRelation(DiGi.Solar.Interfaces.IShadingElement shadingElement, System.Collections.Generic.IEnumerable<DiGi.Solar.Interfaces.IShadingSolverResult> shadingSolverResults);
```
#### Parameters

<a name='DiGi.Solar.Classes.ShadingSolverResultRelation.ShadingSolverResultRelation(DiGi.Solar.Interfaces.IShadingElement,System.Collections.Generic.IEnumerable_DiGi.Solar.Interfaces.IShadingSolverResult_).shadingElement'></a>

`shadingElement` [IShadingElement](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingElement 'DiGi\.Solar\.Interfaces\.IShadingElement')

The parent shading element\.

<a name='DiGi.Solar.Classes.ShadingSolverResultRelation.ShadingSolverResultRelation(DiGi.Solar.Interfaces.IShadingElement,System.Collections.Generic.IEnumerable_DiGi.Solar.Interfaces.IShadingSolverResult_).shadingSolverResults'></a>

`shadingSolverResults` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[IShadingSolverResult](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSolverResult 'DiGi\.Solar\.Interfaces\.IShadingSolverResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The collection of related shading solver results\.

<a name='DiGi.Solar.Classes.ShadingSolverResultRelation.ShadingSolverResultRelation(System.Text.Json.Nodes.JsonObject)'></a>

## ShadingSolverResultRelation\(JsonObject\) Constructor

Initializes a new instance of the [ShadingSolverResultRelation](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolverResultRelation 'DiGi\.Solar\.Classes\.ShadingSolverResultRelation') class from a JSON object\.

```csharp
public ShadingSolverResultRelation(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.Solar.Classes.ShadingSolverResultRelation.ShadingSolverResultRelation(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JSON object containing relation data, or null\.