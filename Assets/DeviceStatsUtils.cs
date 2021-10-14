using UnityEngine;
using System.Collections.Generic;

namespace Pixowl {
    public static class DeviceStatsUtils {

        public static bool UseTexturesHD() {
#if UNITY_EDITOR
            return true;
#elif UNITY_IOS
            return !IsLowIosDevice();// && IsHDScreen() && IsHighMemory();
#elif UNITY_ANDROID
            return IsHDScreen() && IsHighMemory();
#endif
        }

        public static bool UseElementsHD() {
#if UNITY_EDITOR
            return true;
#elif UNITY_IOS
            return !IsLowIosDevice() && !IsMedIosDevice();
#elif UNITY_ANDROID
            return IsHDScreen() && IsHighMemory();
#endif
        }

        const int MEMORY_LOW = 1024;
        public static bool IsHighMemory() {
            return SystemInfo.systemMemorySize > MEMORY_LOW;
        }

        //https://developer.android.com/reference/android/util/DisplayMetrics#DENSITY_HIGH
        const float DENSITY_HIGH = 240f;
        public static bool IsHDScreen() {
            return UnityEngine.Screen.dpi >= DENSITY_HIGH;
        }

#if UNITY_IOS

        //models: https://www.theiphonewiki.com/wiki/Models
        static readonly List<string> _lowIosDevices = new List<string>() {
            //iPad
            "iPad1,1",
            "iPad2,1",
            "iPad2,2",
            "iPad2,3",
            "iPad2,4",
            "iPad3,1",
            "iPad3,2",
            "iPad3,3",
            "iPad3,4",
            "iPad3,5",
            "iPad3,6",
            "iPad4,1",
            "iPad4,2",
            "iPad4,3",
            "iPad5,3",
            "iPad5,4",
            "iPad11,3",
            "iPad11,4",
            "iPad2,5",
            "iPad2,6",
            "iPad2,7",
            "iPad4,4",
            "iPad4,5",
            "iPad4,6",
            "iPad4,7",
            "iPad4,8",
            "iPad4,9",
            //iPhone
            "iPhone1,1",
            "iPhone1,2",
            "iPhone2,1",
            "iPhone3,1",
            "iPhone3,2",
            "iPhone3,3",
            "iPhone4,1",
            "iPhone5,1",
            "iPhone5,2",
            "iPhone5,3",
            "iPhone5,4",
            "iPhone6,1",
            "iPhone6,2",
            "iPhone8,4",//SE
            //iPod
            "iPod1,1",
            "iPod2,1",
            "iPod3,1",
            "iPod4,1",
            "iPod5,1"
        };

        static readonly List<string> _medIosDevices = new List<string>() {
            //iPad
            "iPad7,5",
            "iPad7,6",
            //iPhone
            "iPhone7,1",
            "iPhone7,2",
            //iPod
            "iPod7,1"
        };

        public static bool IsLowIosDevice() {
            var iosModel = SystemInfo.deviceModel;
            return _lowIosDevices.Contains(iosModel);
        }

        public static bool IsMedIosDevice() {
            var iosModel = SystemInfo.deviceModel;
            return _medIosDevices.Contains(iosModel);
        }


        /*
        A7
        iPhone 5S
        iPad Air
        iPad Mini 2
        iPad Mini 3

        A8(Can use ASTC)
        iPhone 6 and 6 Plus – September 2014
        iPod touch(6th generation) – July 2015
        iPad mini 4 – September 2015
        */
        public static bool UseA8OrOver() {
            var iosDevice = UnityEngine.iOS.Device.generation;

            switch(iosDevice) {
                case UnityEngine.iOS.DeviceGeneration.iPhone:
                case UnityEngine.iOS.DeviceGeneration.iPhone3G:
                case UnityEngine.iOS.DeviceGeneration.iPhone3GS:
                case UnityEngine.iOS.DeviceGeneration.iPodTouch1Gen:
                case UnityEngine.iOS.DeviceGeneration.iPodTouch2Gen:
                case UnityEngine.iOS.DeviceGeneration.iPodTouch3Gen:
                case UnityEngine.iOS.DeviceGeneration.iPhone4:
                case UnityEngine.iOS.DeviceGeneration.iPodTouch4Gen:
                case UnityEngine.iOS.DeviceGeneration.iPad2Gen:
                case UnityEngine.iOS.DeviceGeneration.iPhone4S:
                case UnityEngine.iOS.DeviceGeneration.iPad3Gen:
                case UnityEngine.iOS.DeviceGeneration.iPhone5:
                case UnityEngine.iOS.DeviceGeneration.iPodTouch5Gen:
                case UnityEngine.iOS.DeviceGeneration.iPadMini1Gen:
                //case UnityEngine.iOS.DeviceGeneration.iPad4Gen:
                case UnityEngine.iOS.DeviceGeneration.iPhone5C:
                case UnityEngine.iOS.DeviceGeneration.iPhone5S:
                case UnityEngine.iOS.DeviceGeneration.iPadAir1:
                case UnityEngine.iOS.DeviceGeneration.iPadMini2Gen:
                case UnityEngine.iOS.DeviceGeneration.iPhone6:
                //case UnityEngine.iOS.DeviceGeneration.iPhone6Plus:
                case UnityEngine.iOS.DeviceGeneration.iPadMini3Gen:
                case UnityEngine.iOS.DeviceGeneration.iPadAir2:
                //case UnityEngine.iOS.DeviceGeneration.iPhone6S:
                //case UnityEngine.iOS.DeviceGeneration.iPhone6SPlus:
                //case UnityEngine.iOS.DeviceGeneration.iPadPro1Gen:
                //case UnityEngine.iOS.DeviceGeneration.iPadMini4Gen:
                case UnityEngine.iOS.DeviceGeneration.iPhoneSE1Gen:
                //case UnityEngine.iOS.DeviceGeneration.iPadPro10Inch1Gen:
                //case UnityEngine.iOS.DeviceGeneration.iPhone7:
                //case UnityEngine.iOS.DeviceGeneration.iPhone7Plus:
                //case UnityEngine.iOS.DeviceGeneration.iPodTouch6Gen:
                //case UnityEngine.iOS.DeviceGeneration.iPad5Gen:
                //case UnityEngine.iOS.DeviceGeneration.iPadPro2Gen:
                //case UnityEngine.iOS.DeviceGeneration.iPadPro10Inch2Gen:
                //case UnityEngine.iOS.DeviceGeneration.iPhone8:
                //case UnityEngine.iOS.DeviceGeneration.iPhone8Plus:
                //case UnityEngine.iOS.DeviceGeneration.iPhoneX:
                //case UnityEngine.iOS.DeviceGeneration.iPhoneXS:
                //case UnityEngine.iOS.DeviceGeneration.iPhoneXSMax:
                //case UnityEngine.iOS.DeviceGeneration.iPhoneXR:
                //case UnityEngine.iOS.DeviceGeneration.iPadPro11Inch:
                //case UnityEngine.iOS.DeviceGeneration.iPadPro3Gen:
                //case UnityEngine.iOS.DeviceGeneration.iPad6Gen:
                //case UnityEngine.iOS.DeviceGeneration.iPadAir3Gen:
                //case UnityEngine.iOS.DeviceGeneration.iPadMini5Gen:
                //case UnityEngine.iOS.DeviceGeneration.iPhone11:
                //case UnityEngine.iOS.DeviceGeneration.iPhone11Pro:
                //case UnityEngine.iOS.DeviceGeneration.iPhone11ProMax:
                //case UnityEngine.iOS.DeviceGeneration.iPodTouch7Gen:
                //case UnityEngine.iOS.DeviceGeneration.iPad7Gen:
                //case UnityEngine.iOS.DeviceGeneration.iPadPro11Inch2Gen:
                //case UnityEngine.iOS.DeviceGeneration.iPadPro4Gen:
                    return false;
                case UnityEngine.iOS.DeviceGeneration.iPhoneUnknown:
                case UnityEngine.iOS.DeviceGeneration.iPadUnknown:
                case UnityEngine.iOS.DeviceGeneration.iPodTouchUnknown:
                    return true;
                default:
                    return true;
            }
        }
#endif //UNITY_IOS
    }
}