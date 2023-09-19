// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.IMediaBuffer
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect
{
  [ComVisible(true)]
  [Guid("59eff8b9-938c-4a26-82f2-95cb84cdc837")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IMediaBuffer
  {
    void SetLength(uint ulLength);

    void GetMaxLength(out uint pcbMaxLength);

    void GetBufferAndLength([Out] IntPtr pBuffer, out uint cbLength);
  }
}
