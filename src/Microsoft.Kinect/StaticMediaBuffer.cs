// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.StaticMediaBuffer
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect
{
  [ComVisible(true)]
  [StructLayout(LayoutKind.Sequential)]
  internal class StaticMediaBuffer : IMediaBuffer
  {
    private readonly IntPtr _pData;
    private uint _ulSize;
    private uint _ulData;

    private StaticMediaBuffer()
    {
    }

    public void Reset(uint size)
    {
      this._ulSize = size;
      this._ulData = 0U;
    }

    public StaticMediaBuffer(IntPtr pData, uint size)
    {
      this._pData = pData;
      this.Reset(size);
    }

    public void SetLength(uint ulLength) => this._ulData = ulLength;

    public void GetMaxLength(out uint pcbMaxLength) => pcbMaxLength = this._ulSize;

    public void GetBufferAndLength(IntPtr ppBuffer, out uint cbLength)
    {
      if (ppBuffer != IntPtr.Zero)
        Marshal.WriteIntPtr(ppBuffer, this._pData);
      cbLength = this._ulData;
    }
  }
}
