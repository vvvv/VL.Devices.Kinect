# VL.Devices.Kinect

Set of nodes to use Microsoft's Kinect 1 with vvvv gamma.

## Requirements

Make sure to use [vvvv gamma 2019.2](https://vvvv.org/blog/vvvv-gamma-2019.2-preview) >= preview 322

You need to install the official Kinect SDKs as well as the toolkit for more advanced usage:

- [Kinect for Windows SDK v1.8](https://www.microsoft.com/en-us/download/details.aspx?id=40278)
- [Kinect for Windows Developer Toolkit v1.8](https://www.microsoft.com/en-us/download/details.aspx?id=40276)

In order to be able to use the Helper patches, additionally get

- [VL.Skia3d](https://www.nuget.org/packages/VL.Skia3d/0.1.3-alpha) 

## How to get VL.Devices.Kinect

```
nuget install VL.Devices.Kinect -prerelease
```

Read the greybook on how to [manage nugets](https://vvvv.gitbooks.io/the-gray-book/content/en/reference/libraries/dependencies.html#_manage_nugets) for more help.

Then, you can find help patches with `F1 > Kinect` or in `VL.Devices.Kinect/help/Kinect`.
