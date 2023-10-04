# VL.Devices.Kinect
A package for using Kinect and Kinect XBOX 360 by Microsoft in VL.

Try it with vvvv, the visual live-programming environment for .NET  
Download: http://visualprogramming.net

## Requirements
There are 2 different versions of the Kinect 1 available. Depending on which you have it requires either of the following drivers:
* Kinect v1: [Kinect for Windows Runtime v1.8](http://www.microsoft.com/en-us/download/details.aspx?id=40277)
* Kinect "XBOX 360": [Kinect for Windows Runtime v1.8 SDK](https://www.microsoft.com/en-us/download/details.aspx?id=40278)

## Using the library
In order to use this library with VL you have to install the nuget that is available via nuget.org. For information on how to use nugets with VL, see [Managing Nugets](https://thegraybook.vvvv.org/reference/libraries/dependencies.html#manage-nugets) in the VL documentation. As described there you go to the commandline and then type:

    nuget install VL.Devices.Kinect

Once the VL.Devices.Kinect nuget is installed and referenced in your VL document you'll see the category "Kinect" under "Devices" in the nodebrowser. 

Demos are available via the Help Browser!

## Credits
Initial development by [chaupow](https://github.com/chaupow).
