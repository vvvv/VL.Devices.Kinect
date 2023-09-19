// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.DMO_MEDIA_TYPE
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect
{
  [ComVisible(true)]
  [StructLayout(LayoutKind.Sequential)]
  internal sealed class DMO_MEDIA_TYPE : IDisposable
  {
    public static readonly Guid MEDIATYPE_Audio = new Guid("73647561-0000-0010-8000-00AA00389B71");
    public static readonly Guid MEDIASUBTYPE_PCM = new Guid("00000001-0000-0010-8000-00AA00389B71");
    public static readonly Guid MEDIASUBTYPE_WAVE = new Guid("e436eb8b-524f-11ce-9f53-0020af0ba770");
    public static readonly Guid FORMAT_WaveFormatEx = new Guid("05589f81-c356-11ce-bf01-00aa0055595a");
    public Guid majortype;
    public Guid subtype;
    public bool bFixedSizeSamples;
    public bool bTemporalCompression;
    public int lSampleSize;
    public Guid formattype;
    public object pUnk;
    public int cbFormat;
    public IntPtr pbFormat;

    public DMO_MEDIA_TYPE() => this.pbFormat = IntPtr.Zero;

    public void SetFormat(WAVEFORMATEX fmt)
    {
      this.pbFormat = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof (WAVEFORMATEX)));
      if (this.pbFormat == IntPtr.Zero)
        throw new OutOfMemoryException();
      Marshal.StructureToPtr((object) fmt, this.pbFormat, true);
    }

    ~DMO_MEDIA_TYPE() => this.Dispose(false);

    public void Dispose()
    {
      GC.SuppressFinalize((object) this);
      this.Dispose(true);
    }

    private void Dispose(bool disposing)
    {
      if (this.pbFormat != IntPtr.Zero)
        Marshal.FreeCoTaskMem(this.pbFormat);
      this.pbFormat = IntPtr.Zero;
    }
  }
}
