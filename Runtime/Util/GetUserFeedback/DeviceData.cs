using Mono.CSharp;
using UnityEngine;

namespace Meangpu.Util
{
    public static class DeviceData
    {
        public static string DeviceModel => SystemInfo.deviceModel;
        public static string DeviceUID => SystemInfo.deviceUniqueIdentifier;
        public static string DeviceName => SystemInfo.deviceName;
        public static string GetDeviceData() => $"Platform: {DeviceModel}\nName: {DeviceName}\nUID: {DeviceUID}";
    }
}