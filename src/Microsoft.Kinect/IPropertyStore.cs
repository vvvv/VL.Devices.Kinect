// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.IPropertyStore
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System.Runtime.InteropServices;

namespace Microsoft.Kinect
{
  [Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IPropertyStore
  {
    void GetCount(out uint cProps);

    void GetAt([In] uint iProp, out PROPERTYKEY pkey);

    void GetValue([In] ref PROPERTYKEY key, out object pv);

    void SetValue([In] ref PROPERTYKEY key, [In] ref object pv);

    void Commit();
  }
}
