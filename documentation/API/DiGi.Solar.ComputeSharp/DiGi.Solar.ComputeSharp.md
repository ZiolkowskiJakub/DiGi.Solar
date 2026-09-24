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
The default device \([Default](DiGi.Solar.ComputeSharp.Enums.md#DiGi.Solar.ComputeSharp.Enums.ComputeDeviceType.Default 'DiGi\.Solar\.ComputeSharp\.Enums\.ComputeDeviceType\.Default')\), the first hardware\-accelerated device \([Hardware](DiGi.Solar.ComputeSharp.Enums.md#DiGi.Solar.ComputeSharp.Enums.ComputeDeviceType.Hardware 'DiGi\.Solar\.ComputeSharp\.Enums\.ComputeDeviceType\.Hardware')\)
or the WARP software device \([Software](DiGi.Solar.ComputeSharp.Enums.md#DiGi.Solar.ComputeSharp.Enums.ComputeDeviceType.Software 'DiGi\.Solar\.ComputeSharp\.Enums\.ComputeDeviceType\.Software')\); [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') when no such device can be created
or it does not support double precision, which the shading shaders require\.