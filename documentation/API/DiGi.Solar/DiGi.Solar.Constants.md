#### [DiGi\.Solar](DiGi.Solar.Overview.md 'DiGi\.Solar\.Overview')

## DiGi\.Solar\.Constants Namespace
### Classes

<a name='DiGi.Solar.Constants.Albedo'></a>

## Albedo Class

Provides ground reflectance \(albedo\) constants used by the irradiance calculation\.

```csharp
public static class Albedo
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Albedo
### Fields

<a name='DiGi.Solar.Constants.Albedo.Default'></a>

## Albedo\.Default Field

Default ground reflectance applied when no usable albedo value is available\.

```csharp
public const double Default = 0.2;
```

#### Field Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.Solar.Constants.Albedo.Snow'></a>

## Albedo\.Snow Field

Ground reflectance applied when snow is lying on the ground\.

```csharp
public const double Snow = 0.7;
```

#### Field Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')