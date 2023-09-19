// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.INuiAudioBeam
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System.Runtime.InteropServices;

namespace Microsoft.Kinect
{
  [Guid("8C3CEBFA-A35D-497E-BC9A-E9752A8155E0")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComVisible(true)]
  [ComImport]
  internal interface INuiAudioBeam
  {
    void GetBeam(out double angle);

    void SetBeam(double angle);

    void GetPosition(out double angle, out double confidence);
  }
}
