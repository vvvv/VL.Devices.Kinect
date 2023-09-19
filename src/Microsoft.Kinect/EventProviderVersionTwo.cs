// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.EventProviderVersionTwo
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

// Adapted to .NET Core

using System;
using System.Diagnostics.Eventing;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect
{
    [EventSource(Guid = "53eb7889-836a-4f06-838e-0be45cca7f78")]
    internal unsafe class EventProviderVersionTwo : EventSource
    {
        internal bool TemplateEventDescriptor(ref EventDescriptor eventDescriptor)
        {
            if (!this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
                return true;
            this.WriteEventCore(eventDescriptor.id, 0, null);
            return true;
        }

        internal unsafe bool TemplateFrameData(
          ref EventDescriptor eventDescriptor,
          uint streamId,
          ulong width,
          ulong height,
          ulong frameNumber)
        {
            bool flag = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData) * 4];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                eventDataPtr->DataPointer = new IntPtr(&streamId);
                eventDataPtr->Size = 4;
                eventDataPtr[1].DataPointer = new IntPtr(&width);
                eventDataPtr[1].Size = 8;
                eventDataPtr[2].DataPointer = new IntPtr(&height);
                eventDataPtr[2].Size = 8;
                eventDataPtr[3].DataPointer = new IntPtr(&frameNumber);
                eventDataPtr[3].Size = 8;
                this.WriteEventCore(eventDescriptor.id, 4, eventDataPtr);
            }
            return flag;
        }

        internal unsafe bool TemplateProcessFrameData(
          ref EventDescriptor eventDescriptor,
          IntPtr frameOverlapId,
          uint streamId)
        {
            bool flag = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData) * 2];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                eventDataPtr->DataPointer = new IntPtr(&frameOverlapId);
                eventDataPtr->Size = sizeof(IntPtr);
                eventDataPtr[1].DataPointer = new IntPtr(&streamId);
                eventDataPtr[1].Size = 4;
                this.WriteEventCore(eventDescriptor.id, 2, eventDataPtr);
            }
            return flag;
        }

        internal unsafe bool TemplateFrameLifecycleData(
          ref EventDescriptor eventDescriptor,
          uint streamType,
          int bufferIndex,
          int frameNumber)
        {
            bool flag = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData) * 3];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                eventDataPtr->DataPointer = new IntPtr(&streamType);
                eventDataPtr->Size = 4;
                eventDataPtr[1].DataPointer = new IntPtr(&bufferIndex);
                eventDataPtr[1].Size = 4;
                eventDataPtr[2].DataPointer = new IntPtr(&frameNumber);
                eventDataPtr[2].Size = 4;
                this.WriteEventCore(eventDescriptor.id, 3, eventDataPtr);
            }
            return flag;
        }

        internal unsafe bool TemplateAllFramesReadyData(
          ref EventDescriptor eventDescriptor,
          long colorFrameTimestamp,
          long depthFrameTimestamp,
          long skeletonFrameTimestamp)
        {
            bool flag = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData) * 3];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                eventDataPtr->DataPointer = new IntPtr(&colorFrameTimestamp);
                eventDataPtr->Size = 8;
                eventDataPtr[1].DataPointer = new IntPtr(&depthFrameTimestamp);
                eventDataPtr[1].Size = 8;
                eventDataPtr[2].DataPointer = new IntPtr(&skeletonFrameTimestamp);
                eventDataPtr[2].Size = 8;
                this.WriteEventCore(eventDescriptor.id, 3, eventDataPtr);
            }
            return flag;
        }

        internal unsafe bool TemplateFrameReadyData(ref EventDescriptor eventDescriptor, long timestamp)
        {
            bool flag = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData)];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                eventDataPtr->DataPointer = new IntPtr(&timestamp);
                eventDataPtr->Size = 8;
                this.WriteEventCore(eventDescriptor.id, 1, eventDataPtr);
            }
            return flag;
        }

        internal unsafe bool TemplateStreamTypeOnly(
          ref EventDescriptor eventDescriptor,
          uint streamType)
        {
            bool flag = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData)];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                eventDataPtr->DataPointer = new IntPtr(&streamType);
                eventDataPtr->Size = 4;
                this.WriteEventCore(eventDescriptor.id, 1, eventDataPtr);
            }
            return flag;
        }

        internal unsafe bool TemplateFrameStateData(
          ref EventDescriptor eventDescriptor,
          uint streamType,
          int frameNumber,
          uint state)
        {
            bool flag = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData) * 3];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                eventDataPtr->DataPointer = new IntPtr(&streamType);
                eventDataPtr->Size = 4;
                eventDataPtr[1].DataPointer = new IntPtr(&frameNumber);
                eventDataPtr[1].Size = 4;
                eventDataPtr[2].DataPointer = new IntPtr(&state);
                eventDataPtr[2].Size = 4;
                this.WriteEventCore(eventDescriptor.id, 3, eventDataPtr);
            }
            return flag;
        }

        internal unsafe bool TemplateFrameDroppedData(
          ref EventDescriptor eventDescriptor,
          uint streamType,
          int frameNumber)
        {
            bool flag = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData) * 2];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                eventDataPtr->DataPointer = new IntPtr(&streamType);
                eventDataPtr->Size = 4;
                eventDataPtr[1].DataPointer = new IntPtr(&frameNumber);
                eventDataPtr[1].Size = 4;
                this.WriteEventCore(eventDescriptor.id, 2, eventDataPtr);
            }
            return flag;
        }

        internal unsafe bool TemplateNuiImageStreamOpenData(
          ref EventDescriptor eventDescriptor,
          IntPtr hStream,
          uint imageType,
          uint imageResolution,
          uint imageFrameFlags,
          IntPtr hNextFrameEvent)
        {
            bool flag = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData) * 5];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                eventDataPtr->DataPointer = new IntPtr(&hStream);
                eventDataPtr->Size = sizeof(IntPtr);
                eventDataPtr[1].DataPointer = new IntPtr(&imageType);
                eventDataPtr[1].Size = 4;
                eventDataPtr[2].DataPointer = new IntPtr(&imageResolution);
                eventDataPtr[2].Size = 4;
                eventDataPtr[3].DataPointer = new IntPtr(&imageFrameFlags);
                eventDataPtr[3].Size = 4;
                eventDataPtr[4].DataPointer = new IntPtr(&hNextFrameEvent);
                eventDataPtr[4].Size = sizeof(IntPtr);
                this.WriteEventCore(eventDescriptor.id, 5, eventDataPtr);
            }
            return flag;
        }

        internal unsafe bool TemplateNuiInitializeData(
          ref EventDescriptor eventDescriptor,
          string deviceId,
          string processName,
          uint flags)
        {
            bool flag = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData) * 3];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                fixed (char* chPtr1 = deviceId)
                fixed (char* chPtr2 = processName)
                {
                    eventDataPtr->DataPointer = new IntPtr(chPtr1);
                    eventDataPtr->Size = ((deviceId.Length + 1) * 2);
                    eventDataPtr[1].DataPointer = new IntPtr(chPtr2);
                    eventDataPtr[1].Size = ((processName.Length + 1) * 2);
                    eventDataPtr[2].DataPointer = new IntPtr(&flags);
                    eventDataPtr[2].Size = 4;
                    this.WriteEventCore(eventDescriptor.id, 3, eventDataPtr);
                }
            }
            return flag;
        }

        internal unsafe bool TemplateNuiSkeletonTrackingData(
          ref EventDescriptor eventDescriptor,
          IntPtr hNextFrameEvent,
          uint dwFlags)
        {
            bool flag = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData) * 2];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                eventDataPtr->DataPointer = new IntPtr(&hNextFrameEvent);
                eventDataPtr->Size = sizeof(IntPtr);
                eventDataPtr[1].DataPointer = new IntPtr(&dwFlags);
                eventDataPtr[1].Size = 4;
                this.WriteEventCore(eventDescriptor.id, 2, eventDataPtr);
            }
            return flag;
        }

        internal unsafe bool TemplateNuiTransformSmoothParams(
          ref EventDescriptor eventDescriptor,
          float smoothing,
          float correction,
          float prediction,
          float jitterRadius,
          float maxDeviationRadius)
        {
            bool flag = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData) * 5];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                eventDataPtr->DataPointer = new IntPtr(&smoothing);
                eventDataPtr->Size = 4;
                eventDataPtr[1].DataPointer = new IntPtr(&correction);
                eventDataPtr[1].Size = 4;
                eventDataPtr[2].DataPointer = new IntPtr(&prediction);
                eventDataPtr[2].Size = 4;
                eventDataPtr[3].DataPointer = new IntPtr(&jitterRadius);
                eventDataPtr[3].Size = 4;
                eventDataPtr[4].DataPointer = new IntPtr(&maxDeviationRadius);
                eventDataPtr[4].Size = 4;
                this.WriteEventCore(eventDescriptor.id, 5, eventDataPtr);
            }
            return flag;
        }

        internal unsafe bool TemplateNuiSkeletonSetTrackedSkeletonsData(
          ref EventDescriptor eventDescriptor,
          uint skeleton1,
          uint skeleton2)
        {
            bool flag = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData) * 2];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                eventDataPtr->DataPointer = new IntPtr(&skeleton1);
                eventDataPtr->Size = 4;
                eventDataPtr[1].DataPointer = new IntPtr(&skeleton2);
                eventDataPtr[1].Size = 4;
                this.WriteEventCore(eventDescriptor.id, 2, eventDataPtr);
            }
            return flag;
        }

        internal unsafe bool TemplateNuiSetFrameEndEventData(
          ref EventDescriptor eventDescriptor,
          IntPtr hEvent,
          uint dwFrameEventFlag)
        {
            bool flag = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData) * 2];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                eventDataPtr->DataPointer = new IntPtr(&hEvent);
                eventDataPtr->Size = sizeof(IntPtr);
                eventDataPtr[1].DataPointer = new IntPtr(&dwFrameEventFlag);
                eventDataPtr[1].Size = 4;
                this.WriteEventCore(eventDescriptor.id, 2, eventDataPtr);
            }
            return flag;
        }

        internal unsafe bool TemplateNuiImageStreamGetNextFrameData(
          ref EventDescriptor eventDescriptor,
          IntPtr hStream,
          uint dwMillisecondsToWait)
        {
            bool nextFrameData = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData) * 2];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                eventDataPtr->DataPointer = new IntPtr(&hStream);
                eventDataPtr->Size = sizeof(IntPtr);
                eventDataPtr[1].DataPointer = new IntPtr(&dwMillisecondsToWait);
                eventDataPtr[1].Size = 4;
                this.WriteEventCore(eventDescriptor.id, 2, eventDataPtr);
            }
            return nextFrameData;
        }

        internal unsafe bool TemplateNuiSkeletonGetNextFrameData(
          ref EventDescriptor eventDescriptor,
          uint dwMillisecondsToWait)
        {
            bool nextFrameData = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData)];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                eventDataPtr->DataPointer = new IntPtr(&dwMillisecondsToWait);
                eventDataPtr->Size = 4;
                this.WriteEventCore(eventDescriptor.id, 1, eventDataPtr);
            }
            return nextFrameData;
        }

        internal unsafe bool TemplateKinectIdStringData(
          ref EventDescriptor eventDescriptor,
          string kinectId)
        {
            bool flag = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData)];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                fixed (char* chPtr = kinectId)
                {
                    eventDataPtr->DataPointer = new IntPtr(chPtr);
                    eventDataPtr->Size = ((kinectId.Length + 1) * 2);
                    this.WriteEventCore(eventDescriptor.id, 1, eventDataPtr);
                }
            }
            return flag;
        }

        internal unsafe bool TemplateNuiCreateSensorByIndexData(
          ref EventDescriptor eventDescriptor,
          int index)
        {
            bool sensorByIndexData = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData)];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                eventDataPtr->DataPointer = new IntPtr(&index);
                eventDataPtr->Size = 4;
                this.WriteEventCore(eventDescriptor.id, 1, eventDataPtr);
            }
            return sensorByIndexData;
        }

        internal unsafe bool TemplateNuiCreateSensorByIdData(
          ref EventDescriptor eventDescriptor,
          string strInstanceId)
        {
            bool sensorByIdData = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData)];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                fixed (char* chPtr = strInstanceId)
                {
                    eventDataPtr->DataPointer = new IntPtr(chPtr);
                    eventDataPtr->Size = ((strInstanceId.Length + 1) * 2);
                    this.WriteEventCore(eventDescriptor.id, 1, eventDataPtr);
                }
            }
            return sensorByIdData;
        }

        internal unsafe bool TemplateNuiImageStreamSetImageFrameFlagsData(
          ref EventDescriptor eventDescriptor,
          IntPtr hStream,
          uint dwImageFrameFlags)
        {
            bool flag = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData) * 2];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                eventDataPtr->DataPointer = new IntPtr(&hStream);
                eventDataPtr->Size = sizeof(IntPtr);
                eventDataPtr[1].DataPointer = new IntPtr(&dwImageFrameFlags);
                eventDataPtr[1].Size = 4;
                this.WriteEventCore(eventDescriptor.id, 2, eventDataPtr);
            }
            return flag;
        }

        internal unsafe bool TemplateAudioAngleData(ref EventDescriptor eventDescriptor, double dAngle)
        {
            bool flag = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData)];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                eventDataPtr->DataPointer = new IntPtr(&dAngle);
                eventDataPtr->Size = 8;
                this.WriteEventCore(eventDescriptor.id, 1, eventDataPtr);
            }
            return flag;
        }

        internal unsafe bool TemplateAudioPropertyData(
          ref EventDescriptor eventDescriptor,
          uint pid,
          uint value)
        {
            bool flag = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData) * 2];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                eventDataPtr->DataPointer = new IntPtr(&pid);
                eventDataPtr->Size = 4;
                eventDataPtr[1].DataPointer = new IntPtr(&value);
                eventDataPtr[1].Size = 4;
                this.WriteEventCore(eventDescriptor.id, 2, eventDataPtr);
            }
            return flag;
        }

        internal unsafe bool TemplateManagedFramesReadyRegistrationData(
          ref EventDescriptor eventDescriptor,
          int type)
        {
            bool flag = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData)];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                eventDataPtr->DataPointer = new IntPtr(&type);
                eventDataPtr->Size = 4;
                this.WriteEventCore(eventDescriptor.id, 1, eventDataPtr);
            }
            return flag;
        }

        internal unsafe bool TemplateManagedImageStreamData(
          ref EventDescriptor eventDescriptor,
          int type)
        {
            bool flag = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData)];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                eventDataPtr->DataPointer = new IntPtr(&type);
                eventDataPtr->Size = 4;
                this.WriteEventCore(eventDescriptor.id, 1, eventDataPtr);
            }
            return flag;
        }

        internal unsafe bool TemplateManagedOpenNextFrameData(
          ref EventDescriptor eventDescriptor,
          int streamType,
          int millisecondsWait)
        {
            bool flag = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData) * 2];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                eventDataPtr->DataPointer = new IntPtr(&streamType);
                eventDataPtr->Size = 4;
                eventDataPtr[1].DataPointer = new IntPtr(&millisecondsWait);
                eventDataPtr[1].Size = 4;
                this.WriteEventCore(eventDescriptor.id, 2, eventDataPtr);
            }
            return flag;
        }

        internal unsafe bool TemplateKinectSensorStartData(
          ref EventDescriptor eventDescriptor,
          string deviceID)
        {
            bool flag = true;
            if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
            {
                byte* data = stackalloc byte[sizeof(EventProviderVersionTwo.EventData)];
                EventProviderVersionTwo.EventData* eventDataPtr = (EventProviderVersionTwo.EventData*)data;
                fixed (char* chPtr = deviceID)
                {
                    eventDataPtr->DataPointer = new IntPtr(chPtr);
                    eventDataPtr->Size = ((deviceID.Length + 1) * 2);
                    this.WriteEventCore(eventDescriptor.id, 1, eventDataPtr);
                }
            }
            return flag;
        }
    }
}
