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

The default ComputeSharp device \(the first adapter in performance order\) when it is hardware\-accelerated and supports double precision\.

Never the WARP software device: on a machine without a hardware adapter no device is selected and the solver falls back to the CPU [DiGi\.Solar\.Classes\.ShadingSolver](https://learn.microsoft.com/en-us/dotnet/api/digi.solar.classes.shadingsolver 'DiGi\.Solar\.Classes\.ShadingSolver').

<a name='DiGi.Solar.ComputeSharp.Enums.ComputeDeviceType.Hardware'></a>

`Hardware` 1

The first hardware\-accelerated device \(GPU\) that supports double precision\.

<a name='DiGi.Solar.ComputeSharp.Enums.ComputeDeviceType.Software'></a>

`Software` 2

The WARP software device\. Withdrawn: no device is selected for it, so a solve requesting it returns false\.

WARP loses shadows cast between buildings (13 to 28 % of the sun-facing samples of the 20 to 720 surface benchmark grids) and needs about 16 minutes to create the shading pipeline
            (ZiolkowskiJakub/DiGi.Solar#10). The CPU [DiGi\.Solar\.Classes\.ShadingSolver](https://learn.microsoft.com/en-us/dotnet/api/digi.solar.classes.shadingsolver 'DiGi\.Solar\.Classes\.ShadingSolver') is the software path.