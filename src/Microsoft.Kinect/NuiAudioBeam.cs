// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.NuiAudioBeam
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

namespace Microsoft.Kinect
{
  internal class NuiAudioBeam : INuiAudioBeam
  {
    private readonly Microsoft.Kinect.Interop.INuiAudioBeam _beam;

    public object Wrapped => (object) this._beam;

    public NuiAudioBeam(Microsoft.Kinect.Interop.INuiAudioBeam beam) => this._beam = beam;

    public void GetBeam(out double angle) => KinectExceptionHelper.CheckHr(this._beam.GetBeam(out angle));

    public void SetBeam(double angle) => KinectExceptionHelper.CheckHr(this._beam.SetBeam(angle));

    public void GetPosition(out double angle, out double confidence) => KinectExceptionHelper.CheckHr(this._beam.GetPosition(out angle, out confidence));
  }
}
