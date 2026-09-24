#### [DiGi\.Solar\.ComputeSharp](DiGi.Solar.ComputeSharp.Overview.md 'DiGi\.Solar\.ComputeSharp\.Overview')

## DiGi\.Solar\.ComputeSharp\.Enums Namespace
### Enums

<a name='DiGi.Solar.ComputeSharp.Enums.ComputeDeviceType'></a>

## ComputeDeviceType Enum

Specifies the compute device the ComputeSharp shading solver runs on\.

```csharp
public enum ComputeDeviceType
```
### Fields

<a name='DiGi.Solar.ComputeSharp.Enums.ComputeDeviceType.Default'></a>

`Default` 0

The default ComputeSharp device: the first adapter in performance order, with the WARP software device last\.

<a name='DiGi.Solar.ComputeSharp.Enums.ComputeDeviceType.Hardware'></a>

`Hardware` 1

The first hardware\-accelerated device \(GPU\) that supports double precision\.

<a name='DiGi.Solar.ComputeSharp.Enums.ComputeDeviceType.Software'></a>

`Software` 2

The WARP software device, which runs the shaders on the CPU\.