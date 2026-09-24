#### [DiGi\.Solar\.ComputeSharp](DiGi.Solar.ComputeSharp.Overview.md 'DiGi\.Solar\.ComputeSharp\.Overview')

## DiGi\.Solar\.ComputeSharp Namespace
### Classes

<a name='DiGi.Solar.ComputeSharp.Create'></a>

## Create Class

```csharp
public static class Create
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Create
### Methods

<a name='DiGi.Solar.ComputeSharp.Create.GraphicsDevice(thisDiGi.Solar.ComputeSharp.Enums.ComputeDeviceType)'></a>

## Create\.GraphicsDevice\(this ComputeDeviceType\) Method

Gets the ComputeSharp graphics device matching the specified [ComputeDeviceType](DiGi.Solar.ComputeSharp.Enums.md#DiGi.Solar.ComputeSharp.Enums.ComputeDeviceType 'DiGi\.Solar\.ComputeSharp\.Enums\.ComputeDeviceType')\.

Only a device passing [IsSupported\(this GraphicsDevice\)](DiGi.Solar.ComputeSharp.md#DiGi.Solar.ComputeSharp.Query.IsSupported(thisComputeSharp.GraphicsDevice) 'DiGi\.Solar\.ComputeSharp\.Query\.IsSupported\(this ComputeSharp\.GraphicsDevice\)') is returned: hardware-accelerated and supporting double precision, which the shading shaders require.
            The WARP software device is never returned (ZiolkowskiJakub/DiGi.Solar#10), neither for the obsolete `ComputeDeviceType.Software` nor as the ComputeSharp default device on a machine without a hardware adapter.

The returned device is shared by ComputeSharp and must not be disposed by the caller.

```csharp
public static ComputeSharp.GraphicsDevice? GraphicsDevice(this DiGi.Solar.ComputeSharp.Enums.ComputeDeviceType computeDeviceType);
```
#### Parameters

<a name='DiGi.Solar.ComputeSharp.Create.GraphicsDevice(thisDiGi.Solar.ComputeSharp.Enums.ComputeDeviceType).computeDeviceType'></a>

`computeDeviceType` [ComputeDeviceType](DiGi.Solar.ComputeSharp.Enums.md#DiGi.Solar.ComputeSharp.Enums.ComputeDeviceType 'DiGi\.Solar\.ComputeSharp\.Enums\.ComputeDeviceType')

The type of device to get\.

#### Returns
[ComputeSharp\.GraphicsDevice](https://learn.microsoft.com/en-us/dotnet/api/computesharp.graphicsdevice 'ComputeSharp\.GraphicsDevice')  
The ComputeSharp default device \([Default](DiGi.Solar.ComputeSharp.Enums.md#DiGi.Solar.ComputeSharp.Enums.ComputeDeviceType.Default 'DiGi\.Solar\.ComputeSharp\.Enums\.ComputeDeviceType\.Default')\) or the first supported hardware\-accelerated device \([Hardware](DiGi.Solar.ComputeSharp.Enums.md#DiGi.Solar.ComputeSharp.Enums.ComputeDeviceType.Hardware 'DiGi\.Solar\.ComputeSharp\.Enums\.ComputeDeviceType\.Hardware')\);
[null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when no such device can be created or it is not supported, and always for `ComputeDeviceType.Software`\.

<a name='DiGi.Solar.ComputeSharp.Query'></a>

## Query Class

```csharp
public static class Query
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Query
### Methods

<a name='DiGi.Solar.ComputeSharp.Query.IsSupported(thisComputeSharp.GraphicsDevice)'></a>

## Query\.IsSupported\(this GraphicsDevice\) Method

Determines whether the ComputeSharp shading solver can run on the specified graphics device: the device must be hardware\-accelerated and support double precision\.

The WARP software device is rejected even though it reports double precision as available: it loses shadows cast between buildings and needs about 16 minutes to create the shading pipeline
            (ZiolkowskiJakub/DiGi.Solar#10). The CPU [DiGi\.Solar\.Classes\.ShadingSolver](https://learn.microsoft.com/en-us/dotnet/api/digi.solar.classes.shadingsolver 'DiGi\.Solar\.Classes\.ShadingSolver') is the software path.

```csharp
public static bool IsSupported(this ComputeSharp.GraphicsDevice? graphicsDevice);
```
#### Parameters

<a name='DiGi.Solar.ComputeSharp.Query.IsSupported(thisComputeSharp.GraphicsDevice).graphicsDevice'></a>

`graphicsDevice` [ComputeSharp\.GraphicsDevice](https://learn.microsoft.com/en-us/dotnet/api/computesharp.graphicsdevice 'ComputeSharp\.GraphicsDevice')

The graphics device to check\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True if the device is hardware\-accelerated and supports double precision; otherwise, false \(including for a null device\)\.