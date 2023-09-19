// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.DmoAudioWrapper
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System.Globalization;
using System.Threading;

namespace Microsoft.Kinect
{
  internal class DmoAudioWrapper : INativeAudioWrapper
  {
    private readonly KinectSensor _sensor;

    public DmoAudioWrapper(KinectSensor sensor) => this._sensor = sensor;

    public object CreateDmoInstance() => this._sensor == null || this._sensor.NuiSensor == null ? (object) null : (object) this._sensor.NuiSensor.NuiGetAudioSource();

    public void DestroyDmoInstance(object dmo)
    {
    }

    public void GetMicrophoneArrayDevices(MicrophoneArrayDevice[] deviceBuffer, ref int count) => NativeMethods.NuiGetMicrophoneArrayDevices(deviceBuffer, deviceBuffer != null ? deviceBuffer.Length : 0, ref count);

    public void GetSpeakerDevices(SpeakerDevice[] deviceBuffer, ref int count) => NativeMethods.NuiGetSpeakerDevices(deviceBuffer, deviceBuffer != null ? deviceBuffer.Length : 0, ref count);

    public bool MatchAudioDeviceToSensor(MicrophoneArrayDevice device)
    {
      if (this._sensor == null || this._sensor.NuiSensor == null)
        return false;
      string str = this._sensor.NuiSensor.NuiAudioArrayId();
      string deviceId = device.DeviceID;
      if (str == null || deviceId == null)
        return false;
      string[] strArray1 = str.ToUpper(CultureInfo.InvariantCulture).Split('\\');
      string[] strArray2 = deviceId.ToUpper(CultureInfo.InvariantCulture).Split('#');
      return strArray1.Length > 2 && strArray2.Length > 2 && strArray1[1] == strArray2[1] && strArray1[2] == strArray2[2];
    }

    public ManualResetEvent CreateStreamStopEvent() => new ManualResetEvent(false);
  }
}
