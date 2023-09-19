// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.KinectAudioStream
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Kinect
{
  internal class KinectAudioStream : Stream
  {
    internal const uint DMO_OUTPUT_DATA_BUFFERF_INCOMPLETE = 16777216;
    private const uint AUDCLNT_E_DEVICE_INVALIDATED = 2290679812;
    internal const uint BufferSizeMilliSeconds = 50;
    internal const uint BufferSizeBytes = 1600;
    private const uint MinTotalBufferMilliseconds = 1000;
    private const uint MaxTotalBufferMilliseconds = 4000;
    private const int AdditionalStaleDataToDiscard = 250;
    private readonly ManualResetEvent _stopEvent;
    private readonly KinectAudioSource _audioSource;
    private readonly Stack<KinectAudioStream.Buffer> _writeBufferStack = new Stack<KinectAudioStream.Buffer>();
    private readonly Queue<KinectAudioStream.Buffer> _readBufferQueue = new Queue<KinectAudioStream.Buffer>();
    private readonly object _syncRoot = new object();
    private readonly AutoResetEvent _dataReady = new AutoResetEvent(false);
    private bool _started;
    private Thread _capturingThread;
    private KinectAudioStream.Buffer _currentBuffer;
    private int _currentBufferIndex;
    private long _bytesRead;
    private bool _capturing;
    private DateTime _lastRead = DateTime.MinValue;
    private readonly TimeSpan _readStaleThreshold;
    private Exception _captureException;

    internal KinectAudioStream(KinectAudioSource audioSource, TimeSpan readStaleThreshold)
    {
      this._audioSource = audioSource;
      this._readStaleThreshold = readStaleThreshold;
      this._stopEvent = audioSource.NativeWrapper.CreateStreamStopEvent();
    }

    internal void Stop()
    {
      if (!this._started)
        throw new InvalidOperationException(Resources.CaptureNotStarted);
      this._stopEvent.Set();
      this._capturingThread.Join();
    }

    public void Start()
    {
      if (this._started)
        throw new InvalidOperationException(Resources.CaptureAlreadyStarted);
      this.ResetStaleTracking();
      this._capturingThread = new Thread(new ParameterizedThreadStart(this.RunCapture));
      this._capturingThread.IsBackground = true;
      this._capturingThread.SetApartmentState(ApartmentState.MTA);
      this._capturingThread.Name = "AudioCaptureThread";
      this._capturingThread.Start((object) null);
      this._capturing = true;
      this._started = true;
    }

    internal void ResetStaleTracking() => this._lastRead = DateTime.UtcNow;

    private static void InitializeMediaObject(IMediaObject mediaObject)
    {
      WAVEFORMATEX fmt = new WAVEFORMATEX()
      {
        wFormatTag = 1,
        nChannels = 1,
        nSamplesPerSec = 16000,
        nAvgBytesPerSec = 32000,
        nBlockAlign = 2,
        wBitsPerSample = 16,
        cbSize = 0
      };
      using (DMO_MEDIA_TYPE pmt = new DMO_MEDIA_TYPE())
      {
        pmt.majortype = DMO_MEDIA_TYPE.MEDIATYPE_Audio;
        pmt.subtype = DMO_MEDIA_TYPE.MEDIASUBTYPE_PCM;
        pmt.lSampleSize = 0;
        pmt.bFixedSizeSamples = true;
        pmt.bTemporalCompression = false;
        pmt.formattype = DMO_MEDIA_TYPE.FORMAT_WaveFormatEx;
        pmt.SetFormat(fmt);
        pmt.cbFormat = Marshal.SizeOf(typeof (WAVEFORMATEX));
        mediaObject.SetOutputType(0, pmt, 0);
      }
      mediaObject.AllocateStreamingResources();
    }

    internal static uint GetNumBuffersInPool(uint readStaleThresholdMilliseconds) => (uint) Math.Ceiling((double) Math.Min(4000U, Math.Max(1000U, readStaleThresholdMilliseconds)) / 50.0);

    private void RunCapture(object notused)
    {
      byte[] data = new byte[1600];
      object dmoInstance = this._audioSource.NativeWrapper.CreateDmoInstance();
      IMediaObject mediaObject;
      IPropertyStore propertyStore1;
      if (dmoInstance is NuiAudioBeam nuiAudioBeam)
      {
        mediaObject = (IMediaObject) nuiAudioBeam.Wrapped;
        propertyStore1 = (IPropertyStore) nuiAudioBeam.Wrapped;
      }
      else
      {
        mediaObject = (IMediaObject) dmoInstance;
        propertyStore1 = (IPropertyStore) dmoInstance;
      }
      int taskIndex = 0;
      int avrtHandle = NativeMethods.AvSetMmThreadCharacteristics("Audio", ref taskIndex);
      uint numBuffersInPool = KinectAudioStream.GetNumBuffersInPool((uint) this._readStaleThreshold.TotalMilliseconds);
      for (int index = 0; (long) index < (long) numBuffersInPool; ++index)
        this._writeBufferStack.Push(new KinectAudioStream.Buffer(1600U));
      GCHandle handle1 = new GCHandle();
      GCHandle handle2 = new GCHandle();
      IntPtr pUnk = IntPtr.Zero;
      bool flag1 = false;
      ushort? nullable1 = new ushort?();
      try
      {
        handle1 = GCHandle.Alloc((object) data, GCHandleType.Pinned);
        StaticMediaBuffer o = new StaticMediaBuffer(handle1.AddrOfPinnedObject(), (uint) data.Length);
        pUnk = Marshal.GetComInterfaceForObject((object) o, typeof (IMediaBuffer));
        DMO_OUTPUT_DATA_BUFFER outputDataBuffer = new DMO_OUTPUT_DATA_BUFFER()
        {
          Buffer = pUnk,
          Status = 0
        };
        handle2 = GCHandle.Alloc((object) outputDataBuffer, GCHandleType.Pinned);
        DMO_OUTPUT_DATA_BUFFER[] pOutputBuffers = new DMO_OUTPUT_DATA_BUFFER[1]
        {
          outputDataBuffer
        };
        KinectAudioStream.AudioAngleTracker audioAngleTracker = new KinectAudioStream.AudioAngleTracker(this._audioSource, dmoInstance as INuiAudioBeam);
        KinectAudioStream.BufferManager bufferManager = new KinectAudioStream.BufferManager(this);
        bool flag2 = false;
label_28:
        if (!flag2)
        {
          do
          {
            outputDataBuffer.Status = 0U;
            o.Reset((uint) data.Length);
            while (!this._stopEvent.WaitOne(0, false))
            {
              ushort? nullable2 = nullable1;
              if (!(nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?()).HasValue)
              {
                nullable1 = this._audioSource.FindMicrophoneIndex();
                ushort? nullable3 = nullable1;
                if ((nullable3.HasValue ? new int?((int) nullable3.GetValueOrDefault()) : new int?()).HasValue)
                {
                  this._audioSource.MicrophoneIndex = nullable1.Value;
                }
                else
                {
                  bufferManager.WriteEmpty(1600U);
                  Thread.Sleep(50);
                }
              }
              else
                break;
            }
            if (this._stopEvent.WaitOne(10, false))
            {
              flag2 = true;
              break;
            }
            this._audioSource.ApplyPendingProperties(propertyStore1);
            try
            {
              if (!flag1)
              {
                KinectAudioStream.InitializeMediaObject(mediaObject);
                flag1 = true;
              }
              mediaObject.ProcessOutput(0, 1, pOutputBuffers, out int _);
            }
            catch (COMException ex)
            {
              if (ex.ErrorCode == -2004287484)
              {
                nullable1 = new ushort?();
                goto label_27;
              }
              else
                throw;
            }
            catch (ArgumentException ex)
            {
              int count = 0;
              this._audioSource.NativeWrapper.GetSpeakerDevices((SpeakerDevice[]) null, ref count);
              if (this._audioSource.EchoCancellationSpeakerIndex >= count)
              {
                IPropertyStore propertyStore2 = propertyStore1;
                PROPERTYKEY propertykey1 = KinectAudioSource.PropertyKeyFromId(2);
                ref PROPERTYKEY local1 = ref propertykey1;
                object obj1 = (object) 2;
                ref object local2 = ref obj1;
                propertyStore2.SetValue(ref local1, ref local2);
                IPropertyStore propertyStore3 = propertyStore1;
                PROPERTYKEY propertykey2 = KinectAudioSource.PropertyKeyFromId(10);
                ref PROPERTYKEY local3 = ref propertykey2;
                object obj2 = (object) 0;
                ref object local4 = ref obj2;
                propertyStore3.SetValue(ref local3, ref local4);
                goto label_27;
              }
              else
                throw;
            }
            uint cbLength;
            o.GetBufferAndLength(IntPtr.Zero, out cbLength);
            bufferManager.Write(data, cbLength);
            audioAngleTracker.TrackAngles();
label_27:;
          }
          while (((int) outputDataBuffer.Status & 16777216) != 0);
          goto label_28;
        }
        else
          bufferManager.Flush();
      }
      catch (COMException ex)
      {
        this._captureException = (Exception) ex;
      }
      catch (UnauthorizedAccessException ex)
      {
        this._captureException = (Exception) ex;
      }
      catch (Exception ex)
      {
        this._captureException = (Exception) null;
      }
      finally
      {
        this._capturing = false;
        this._dataReady.Set();
        KinectAudioStream.SafeFree(handle2);
        KinectAudioStream.SafeFree(handle1);
        if (pUnk != IntPtr.Zero)
          Marshal.Release(pUnk);
        this._audioSource.NativeWrapper.DestroyDmoInstance(dmoInstance);
        if (avrtHandle != 0)
          NativeMethods.AvRevertMmThreadCharacteristics(avrtHandle);
      }
    }

    internal Queue<KinectAudioStream.Buffer> ReadBufferQueue => this._readBufferQueue;

    private void QueueCapturedBuffer(KinectAudioStream.Buffer capturedBuffer)
    {
      lock (this._syncRoot)
      {
        this._readBufferQueue.Enqueue(capturedBuffer);
        this._dataReady.Set();
      }
    }

    private KinectAudioStream.Buffer GetWriteBuffer()
    {
      lock (this._syncRoot)
      {
        if (this._writeBufferStack.Count > 0)
          return this._writeBufferStack.Pop();
        if (this._readBufferQueue.Count <= 0)
          return (KinectAudioStream.Buffer) null;
        KinectAudioStream.Buffer writeBuffer = this._readBufferQueue.Dequeue();
        writeBuffer.DataLength = 0U;
        return writeBuffer;
      }
    }

    private static void SafeFree(GCHandle handle)
    {
      if (!(handle != new GCHandle()))
        return;
      handle.Free();
    }

    public override bool CanRead => this._started;

    public override bool CanSeek => false;

    public override bool CanWrite => false;

    public override void Flush() => throw new InvalidOperationException(Resources.NotSupported);

    public override long Length => -1;

    public override long Position
    {
      get => this._bytesRead;
      set => throw new InvalidOperationException(Resources.NotSupported);
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      if (this._captureException != null)
        throw this._captureException;
      if (DateTime.UtcNow - this._lastRead > this._readStaleThreshold)
      {
        Thread.Sleep(250);
        this.ReleaseAllBuffers();
      }
      this.ResetStaleTracking();
      int num = 0;
      while (count > 0 && this._capturing)
      {
        lock (this._syncRoot)
        {
          if (this._currentBuffer == null && this._readBufferQueue.Count != 0)
          {
            this._currentBuffer = this._readBufferQueue.Dequeue();
            this._currentBufferIndex = 0;
          }
          if (this._currentBuffer != null)
          {
            int length = (int) Math.Min((long) this._currentBuffer.DataLength - (long) this._currentBufferIndex, (long) count);
            Array.Copy((Array) this._currentBuffer.Data, this._currentBufferIndex, (Array) buffer, offset, length);
            count -= length;
            offset += length;
            num += length;
            this._currentBufferIndex += length;
            if ((long) this._currentBufferIndex >= (long) this._currentBuffer.DataLength)
            {
              this.ReleaseBuffer(this._currentBuffer);
              this._currentBuffer = this._readBufferQueue.Count != 0 ? this._readBufferQueue.Dequeue() : (KinectAudioStream.Buffer) null;
              this._currentBufferIndex = 0;
            }
          }
        }
        if (this._currentBuffer == null && count > 0)
          this._dataReady.WaitOne(-1, false);
      }
      this._bytesRead += (long) num;
      return num;
    }

    private void ReleaseBuffer(KinectAudioStream.Buffer buffer)
    {
      buffer.DataLength = 0U;
      this._writeBufferStack.Push(buffer);
    }

    private void ReleaseAllBuffers()
    {
      lock (this._syncRoot)
      {
        while (this._readBufferQueue.Count > 0)
          this.ReleaseBuffer(this._readBufferQueue.Dequeue());
        if (this._currentBuffer != null)
          this.ReleaseBuffer(this._currentBuffer);
        this._currentBufferIndex = 0;
        this._currentBuffer = (KinectAudioStream.Buffer) null;
      }
    }

    public override long Seek(long offset, SeekOrigin origin) => this._bytesRead + offset;

    public override void SetLength(long value) => throw new InvalidOperationException(Resources.NotSupported);

    public override void Write(byte[] buffer, int offset, int count) => throw new InvalidOperationException(Resources.NotSupported);

    public override void Close()
    {
      base.Close();
      this.Stop();
    }

    internal class Buffer
    {
      public Buffer(uint size) => this.Data = new byte[size];

      public uint DataLength { get; set; }

      public byte[] Data { get; private set; }
    }

    internal class AudioAngleTracker
    {
      private double _lastAngle = double.MinValue;
      private double _lastSoundSourceAngle = double.MinValue;
      private double _lastSoundSourceConfidence = double.MinValue;
      private readonly KinectAudioSource _audioSource;
      private readonly INuiAudioBeam _audioBeam;

      internal AudioAngleTracker(KinectAudioSource audioSource, INuiAudioBeam audioBeam)
      {
        this._audioSource = audioSource;
        this._audioBeam = audioBeam;
      }

      internal void TrackAngles()
      {
        bool flag = false;
        if (this._audioSource.BeamAngleMode == BeamAngleMode.Manual)
        {
          double angle = this._audioSource.FetchNextBeamAngle();
          if (angle > double.MinValue)
          {
            this._audioBeam.SetBeam(angle);
            flag = true;
          }
        }
        if (this._audioSource.BeamAngleMode == BeamAngleMode.Automatic || this._audioSource.BeamAngleMode == BeamAngleMode.Adaptive || flag)
        {
          double angle = 0.0;
          this._audioBeam.GetBeam(out angle);
          if (Math.Abs(angle - this._lastAngle) > double.Epsilon)
          {
            this._lastAngle = angle;
            this._audioSource.SetBeamAngle(angle);
          }
        }
        double angle1;
        double confidence;
        this._audioBeam.GetPosition(out angle1, out confidence);
        if (Math.Abs(angle1 - this._lastSoundSourceAngle) <= double.Epsilon && Math.Abs(confidence - this._lastSoundSourceConfidence) <= double.Epsilon)
          return;
        this._audioSource.SetSoundSourceAngleAndConfidence(angle1, confidence);
        this._lastSoundSourceAngle = angle1;
        this._lastSoundSourceConfidence = confidence;
      }
    }

    internal class BufferManager
    {
      private KinectAudioStream.Buffer _writeBuf;
      private readonly KinectAudioStream _stream;

      internal BufferManager(KinectAudioStream stream) => this._stream = stream;

      internal void Write(byte[] data, uint length) => this.WriteHelper(length, (Action<uint>) (destIndex => Array.Copy((Array) data, 0L, (Array) this._writeBuf.Data, (long) destIndex, (long) length)));

      internal void WriteEmpty(uint length) => this.WriteHelper(length, (Action<uint>) (destIndex => Array.Clear((Array) this._writeBuf.Data, (int) destIndex, (int) length)));

      private void WriteHelper(uint length, Action<uint> writeAction)
      {
        if (length <= 0U)
          return;
        if (this._writeBuf == null)
          this._writeBuf = this._stream.GetWriteBuffer();
        if ((long) (this._writeBuf.DataLength + length) < (long) this._writeBuf.Data.Length)
        {
          writeAction(this._writeBuf.DataLength);
          this._writeBuf.DataLength += length;
        }
        else
        {
          this._stream.QueueCapturedBuffer(this._writeBuf);
          this._writeBuf = this._stream.GetWriteBuffer();
          writeAction(0U);
          this._writeBuf.DataLength = length;
        }
      }

      internal void Flush()
      {
        if (this._writeBuf == null)
          return;
        this._stream.QueueCapturedBuffer(this._writeBuf);
        this._writeBuf = (KinectAudioStream.Buffer) null;
      }
    }
  }
}
