// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Interop.INuiDepthFilter
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect.Interop
{
  [Guid("1D7C07DD-2304-49BB-9B7F-2FDC6E00C1B2")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface INuiDepthFilter
  {
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int ProcessFrame(
      [In] _LARGE_INTEGER liTimeStamp,
      [In] uint Width,
      [In] uint Height,
      [In] IntPtr pDepthImagePixels,
      out int pFrameModified);
  }
}
