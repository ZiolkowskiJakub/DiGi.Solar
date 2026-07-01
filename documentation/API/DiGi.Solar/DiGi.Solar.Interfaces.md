#### [DiGi\.Solar](index.md 'index')

## DiGi\.Solar\.Interfaces Namespace
### Interfaces

<a name='DiGi.Solar.Interfaces.IShadingElement'></a>

## IShadingElement Interface

Defines the properties of an element that provides shading\.

```csharp
public interface IShadingElement : DiGi.Solar.Interfaces.IShadingUniqueObject, DiGi.Solar.Interfaces.IShadingSerializableObject, DiGi.Solar.Interfaces.IShadingObject, DiGi.Solar.Interfaces.ISolarObject, DiGi.Core.Interfaces.IObject, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject, DiGi.Core.Interfaces.IUniqueObject
```

Derived  
↳ [ShadingElement](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingElement 'DiGi\.Solar\.Classes\.ShadingElement')

Implements [IShadingUniqueObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingUniqueObject 'DiGi\.Solar\.Interfaces\.IShadingUniqueObject'), [IShadingSerializableObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSerializableObject 'DiGi\.Solar\.Interfaces\.IShadingSerializableObject'), [IShadingObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingObject 'DiGi\.Solar\.Interfaces\.IShadingObject'), [ISolarObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.ISolarObject 'DiGi\.Solar\.Interfaces\.ISolarObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject'), [DiGi\.Core\.Interfaces\.IUniqueObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iuniqueobject 'DiGi\.Core\.Interfaces\.IUniqueObject')
### Properties

<a name='DiGi.Solar.Interfaces.IShadingElement.PolygonalFace3D'></a>

## IShadingElement\.PolygonalFace3D Property

Gets the 3D polygonal face associated with the shading element\.

```csharp
DiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D? PolygonalFace3D { get; }
```

#### Property Value
[DiGi\.Geometry\.Spatial\.Interfaces\.IPolygonalFace3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.interfaces.ipolygonalface3d 'DiGi\.Geometry\.Spatial\.Interfaces\.IPolygonalFace3D')

<a name='DiGi.Solar.Interfaces.IShadingElement.Reference'></a>

## IShadingElement\.Reference Property

Gets the reference identifier of the shading element\.

```csharp
string? Reference { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.Solar.Interfaces.IShadingElement.ShadingOnly'></a>

## IShadingElement\.ShadingOnly Property

Gets a value indicating whether the element is used exclusively for shading purposes\.

```csharp
bool ShadingOnly { get; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.Solar.Interfaces.IShadingObject'></a>

## IShadingObject Interface

Defines the contract for an object that causes shading within a solar installation\.

```csharp
public interface IShadingObject : DiGi.Solar.Interfaces.ISolarObject, DiGi.Core.Interfaces.IObject
```

Derived  
↳ [ShadingElement](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingElement 'DiGi\.Solar\.Classes\.ShadingElement')  
↳ [ShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolverResult 'DiGi\.Solar\.Classes\.ShadingSolverResult')  
↳ [ShadingSolverResultRelation](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolverResultRelation 'DiGi\.Solar\.Classes\.ShadingSolverResultRelation')  
↳ [IShadingElement](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingElement 'DiGi\.Solar\.Interfaces\.IShadingElement')  
↳ [IShadingRelation](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingRelation 'DiGi\.Solar\.Interfaces\.IShadingRelation')  
↳ [IShadingSerializableObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSerializableObject 'DiGi\.Solar\.Interfaces\.IShadingSerializableObject')  
↳ [IShadingSolverResult](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSolverResult 'DiGi\.Solar\.Interfaces\.IShadingSolverResult')  
↳ [IShadingUniqueObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingUniqueObject 'DiGi\.Solar\.Interfaces\.IShadingUniqueObject')

Implements [ISolarObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.ISolarObject 'DiGi\.Solar\.Interfaces\.ISolarObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')

<a name='DiGi.Solar.Interfaces.IShadingRelation'></a>

## IShadingRelation Interface

Defines the contract for a shading relation, combining the characteristics of a shading object and a relationship between unique objects\.

```csharp
public interface IShadingRelation : DiGi.Solar.Interfaces.IShadingObject, DiGi.Solar.Interfaces.ISolarObject, DiGi.Core.Interfaces.IObject, DiGi.Core.Relation.Interfaces.IRelation, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject
```

Derived  
↳ [ShadingSolverResultRelation](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolverResultRelation 'DiGi\.Solar\.Classes\.ShadingSolverResultRelation')

Implements [IShadingObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingObject 'DiGi\.Solar\.Interfaces\.IShadingObject'), [ISolarObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.ISolarObject 'DiGi\.Solar\.Interfaces\.ISolarObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Core\.Relation\.Interfaces\.IRelation](https://learn.microsoft.com/en-us/dotnet/api/digi.core.relation.interfaces.irelation 'DiGi\.Core\.Relation\.Interfaces\.IRelation'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject')

<a name='DiGi.Solar.Interfaces.IShadingSerializableObject'></a>

## IShadingSerializableObject Interface

Defines a contract for shading objects that can be serialized to and from JSON\.

```csharp
public interface IShadingSerializableObject : DiGi.Solar.Interfaces.IShadingObject, DiGi.Solar.Interfaces.ISolarObject, DiGi.Core.Interfaces.IObject, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject
```

Derived  
↳ [ShadingElement](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingElement 'DiGi\.Solar\.Classes\.ShadingElement')  
↳ [ShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolverResult 'DiGi\.Solar\.Classes\.ShadingSolverResult')  
↳ [IShadingElement](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingElement 'DiGi\.Solar\.Interfaces\.IShadingElement')  
↳ [IShadingSolverResult](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSolverResult 'DiGi\.Solar\.Interfaces\.IShadingSolverResult')  
↳ [IShadingUniqueObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingUniqueObject 'DiGi\.Solar\.Interfaces\.IShadingUniqueObject')

Implements [IShadingObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingObject 'DiGi\.Solar\.Interfaces\.IShadingObject'), [ISolarObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.ISolarObject 'DiGi\.Solar\.Interfaces\.ISolarObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject')

<a name='DiGi.Solar.Interfaces.IShadingSolverResult'></a>

## IShadingSolverResult Interface

Defines a contract for an object that represents the result of a shading solver operation\.

```csharp
public interface IShadingSolverResult : DiGi.Solar.Interfaces.IShadingUniqueObject, DiGi.Solar.Interfaces.IShadingSerializableObject, DiGi.Solar.Interfaces.IShadingObject, DiGi.Solar.Interfaces.ISolarObject, DiGi.Core.Interfaces.IObject, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject, DiGi.Core.Interfaces.IUniqueObject, DiGi.Core.Interfaces.IResult<DiGi.Solar.Interfaces.IShadingElement>, DiGi.Core.Interfaces.IResult
```

Derived  
↳ [ShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolverResult 'DiGi\.Solar\.Classes\.ShadingSolverResult')

Implements [IShadingUniqueObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingUniqueObject 'DiGi\.Solar\.Interfaces\.IShadingUniqueObject'), [IShadingSerializableObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSerializableObject 'DiGi\.Solar\.Interfaces\.IShadingSerializableObject'), [IShadingObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingObject 'DiGi\.Solar\.Interfaces\.IShadingObject'), [ISolarObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.ISolarObject 'DiGi\.Solar\.Interfaces\.ISolarObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject'), [DiGi\.Core\.Interfaces\.IUniqueObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iuniqueobject 'DiGi\.Core\.Interfaces\.IUniqueObject'), [DiGi\.Core\.Interfaces\.IResult&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iresult-1 'DiGi\.Core\.Interfaces\.IResult\`1')[IShadingElement](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingElement 'DiGi\.Solar\.Interfaces\.IShadingElement')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iresult-1 'DiGi\.Core\.Interfaces\.IResult\`1'), [DiGi\.Core\.Interfaces\.IResult](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iresult 'DiGi\.Core\.Interfaces\.IResult')
### Properties

<a name='DiGi.Solar.Interfaces.IShadingSolverResult.Area'></a>

## IShadingSolverResult\.Area Property

Gets the calculated area of the shading result\.

```csharp
double Area { get; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.Solar.Interfaces.IShadingSolverResult.DateTime'></a>

## IShadingSolverResult\.DateTime Property

Gets the date and time associated with the shading calculation\.

```csharp
System.DateTime DateTime { get; }
```

#### Property Value
[System\.DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime 'System\.DateTime')

<a name='DiGi.Solar.Interfaces.IShadingUniqueObject'></a>

## IShadingUniqueObject Interface

Defines a contract for a shading object that is both serializable and possesses a unique identifier\.

```csharp
public interface IShadingUniqueObject : DiGi.Solar.Interfaces.IShadingSerializableObject, DiGi.Solar.Interfaces.IShadingObject, DiGi.Solar.Interfaces.ISolarObject, DiGi.Core.Interfaces.IObject, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject, DiGi.Core.Interfaces.IUniqueObject
```

Derived  
↳ [ShadingElement](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingElement 'DiGi\.Solar\.Classes\.ShadingElement')  
↳ [ShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolverResult 'DiGi\.Solar\.Classes\.ShadingSolverResult')  
↳ [IShadingElement](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingElement 'DiGi\.Solar\.Interfaces\.IShadingElement')  
↳ [IShadingSolverResult](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSolverResult 'DiGi\.Solar\.Interfaces\.IShadingSolverResult')

Implements [IShadingSerializableObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSerializableObject 'DiGi\.Solar\.Interfaces\.IShadingSerializableObject'), [IShadingObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingObject 'DiGi\.Solar\.Interfaces\.IShadingObject'), [ISolarObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.ISolarObject 'DiGi\.Solar\.Interfaces\.ISolarObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject'), [DiGi\.Core\.Interfaces\.IUniqueObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iuniqueobject 'DiGi\.Core\.Interfaces\.IUniqueObject')

<a name='DiGi.Solar.Interfaces.ISolarObject'></a>

## ISolarObject Interface

Defines the contract for an object specifically associated with solar energy components and systems\.

```csharp
public interface ISolarObject : DiGi.Core.Interfaces.IObject
```

Derived  
↳ [ShadingElement](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingElement 'DiGi\.Solar\.Classes\.ShadingElement')  
↳ [ShadingModel](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingModel 'DiGi\.Solar\.Classes\.ShadingModel')  
↳ [ShadingRelationCluster](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingRelationCluster 'DiGi\.Solar\.Classes\.ShadingRelationCluster')  
↳ [ShadingSolverResult](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolverResult 'DiGi\.Solar\.Classes\.ShadingSolverResult')  
↳ [ShadingSolverResultRelation](DiGi.Solar.Classes.md#DiGi.Solar.Classes.ShadingSolverResultRelation 'DiGi\.Solar\.Classes\.ShadingSolverResultRelation')  
↳ [IShadingElement](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingElement 'DiGi\.Solar\.Interfaces\.IShadingElement')  
↳ [IShadingObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingObject 'DiGi\.Solar\.Interfaces\.IShadingObject')  
↳ [IShadingRelation](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingRelation 'DiGi\.Solar\.Interfaces\.IShadingRelation')  
↳ [IShadingSerializableObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSerializableObject 'DiGi\.Solar\.Interfaces\.IShadingSerializableObject')  
↳ [IShadingSolverResult](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingSolverResult 'DiGi\.Solar\.Interfaces\.IShadingSolverResult')  
↳ [IShadingUniqueObject](DiGi.Solar.Interfaces.md#DiGi.Solar.Interfaces.IShadingUniqueObject 'DiGi\.Solar\.Interfaces\.IShadingUniqueObject')

Implements [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')