// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.NativeMethods
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using Microsoft.Kinect.Interop;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect
{
    internal static class NativeMethods
    {
        internal const int S_OK = 0;

        [DllImport("Kinect10.dll", CallingConvention = CallingConvention.StdCall)]
        internal static extern void NuiSetDeviceStatusCallback(
          [In] NativeMethods.NuiStatusCallback pCallback,
          IntPtr pUserData);

        [DllImport("KinectAudio10.dll", CallingConvention = CallingConvention.StdCall)]
        internal static extern void NuiGetMicrophoneArrayDevices(
          [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1), Out] MicrophoneArrayDevice[] deviceBuffer,
          [In] int size,
          [In, Out] ref int count);

        [DllImport("KinectAudio10.dll", CallingConvention = CallingConvention.StdCall)]
        internal static extern void NuiGetSpeakerDevices(
          [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1), Out] SpeakerDevice[] deviceBuffer,
          [In] int size,
          [In, Out] ref int count);

        [DllImport("Kinect10.dll", CallingConvention = CallingConvention.StdCall)]
        internal static extern int NuiGetSensorCount(out int pCount);

        [DllImport("Kinect10.dll", CallingConvention = CallingConvention.StdCall)]
        internal static extern int NuiCreateSensorByIndex(int index, out INuiSensor pNuiSensor);

        [DllImport("Kinect10.dll", CallingConvention = CallingConvention.StdCall)]
        internal static extern int NuiCreateSensorById([MarshalAs(UnmanagedType.LPWStr)] string instanceName, out INuiSensor pNuiSensor);

        [DllImport("avrt.dll", EntryPoint = "AvSetMmThreadCharacteristicsW", CallingConvention = CallingConvention.StdCall)]
        internal static extern int AvSetMmThreadCharacteristics([MarshalAs(UnmanagedType.LPWStr)] string taskName, [In, Out] ref int taskIndex);

        [DllImport("avrt.dll", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool AvRevertMmThreadCharacteristics(int avrtHandle);

        [DllImport("Kinect10.dll", EntryPoint = "#1", CallingConvention = CallingConvention.StdCall)]
        internal static extern ulong NuiDebugGetFailureStack();

        [DllImport("Kinect10.dll", CallingConvention = CallingConvention.StdCall)]
        internal static extern int NuiSkeletonCalculateBoneOrientations(
          [In] ref _NUI_SKELETON_DATA pSkeletonData,
          [Out] _NUI_SKELETON_BONE_ORIENTATION[] pBoneOrientations);

        [DllImport("Kinect10.dll", CallingConvention = CallingConvention.StdCall)]
        internal static extern int NuiCreateCoordinateMapperFromParameters(
          [In] uint length,
          [In] IntPtr pMapperParameters,
          out INuiCoordinateMapper pCoordinateMapper);

        internal static unsafe void CopyMemory(IntPtr destination, IntPtr source, int length)
        {
            Unsafe.CopyBlockUnaligned(destination.ToPointer(), source.ToPointer(), (uint)length);
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void NuiStatusCallback(
          int status,
          [MarshalAs(UnmanagedType.LPWStr)] string instanceName,
          [MarshalAs(UnmanagedType.LPWStr)] string uniqueDeviceName,
          IntPtr pUserData);
    }
}
