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

<a name='DiGi.Solar.Constants.Albedo.Missing'></a>

## Albedo\.Missing Field

The marker weather files use for a missing numeric field, which both albedo and snow depth encode as 999\.

The albedo branch of [Albedo\(Nullable&lt;double&gt;, Nullable&lt;double&gt;\)](DiGi.Solar.md#DiGi.Solar.Query.Albedo(System.Nullable_double_,System.Nullable_double_) 'DiGi\.Solar\.Query\.Albedo\(System\.Nullable\<double\>, System\.Nullable\<double\>\)') does not read this constant, because rejecting anything greater than 1 already subsumes it. It is the snow depth check that needs it, so that a missing snow depth is not read as 999 cm of lying snow.

```csharp
public const double Missing = 999;
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