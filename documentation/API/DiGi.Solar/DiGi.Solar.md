#### [DiGi\.Solar](index.md 'index')

## DiGi\.Solar Namespace
### Classes

<a name='DiGi.Solar.Query'></a>

## Query Class

```csharp
public static class Query
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Query
### Methods

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