// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.IMediaObject
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System.Runtime.InteropServices;

namespace Microsoft.Kinect
{
  [ComVisible(true)]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("d8ad0f58-5494-4102-97c5-ec798e59bcf4")]
  [ComImport]
  internal interface IMediaObject
  {
    void GetStreamCount(out int pcInputStreams, out int pcOutputStreams);

    void GetInputStreamInfo(int dwInputStreamIndex, out int pdwFlags);

    void GetOutputStreamInfo(int dwOutputStreamIndex, out int pdwFlags);

    void GetInputType(int dwInputStreamIndex, int dwTypeIndex, out DMO_MEDIA_TYPE pmt);

    void GetOutputType(int dwOutputStreamIndex, int dwTypeIndex, out DMO_MEDIA_TYPE pmt);

    void SetInputType(int dwInputStreamIndex, DMO_MEDIA_TYPE pmt, int dwFlags);

    void SetOutputType(int dwOutputStreamIndex, DMO_MEDIA_TYPE pmt, int dwFlags);

    void GetInputCurrentType(int dwInputStreamIndex, out DMO_MEDIA_TYPE pmt);

    void GetOutputCurrentType(int dwOutputStreamIndex, out DMO_MEDIA_TYPE pmt);

    void GetInputSizeInfo(
      int dwInputStreamIndex,
      out int pcbSize,
      out int pcbMaxLookahead,
      out int pcbAlignment);

    void GetOutputSizeInfo(int dwOutputStreamIndex, out int pcbSize, out int pcbAlignment);

    void GetInputMaxLatency(int dwInputStreamIndex, out long prtMaxLatency);

    void SetInputMaxLatency(int dwInputStreamIndex, long rtMaxLatency);

    void Flush();

    void Discontinuity(int dwInputStreamIndex);

    void AllocateStreamingResources();

    void FreeStreamingResources();

    void GetInputStatus(int dwInputStreamIndex, out int dwFlags);

    void ProcessInput(
      int dwInputStreamIndex,
      IMediaBuffer pBuffer,
      int dwFlags,
      long rtTimestamp,
      long rtTimelength);

    void ProcessOutput(
      int dwFlags,
      int cOutputBufferCount,
      [MarshalAs(UnmanagedType.LPArray)] DMO_OUTPUT_DATA_BUFFER[] pOutputBuffers,
      out int pdwStatus);

    void Lock(long bLock);
  }
}
