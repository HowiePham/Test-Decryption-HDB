using System;
using System.Runtime.InteropServices;
using UnityEngine;

public static class IOSHelper
{
    [DllImport("__Internal")]
    private static extern IntPtr _getApplicationSupportDirectory();

    public static string GetApplicationSupportDirectory()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            IntPtr ptr = _getApplicationSupportDirectory();
            return Marshal.PtrToStringAnsi(ptr);
        }
        else
        {
            return string.Empty;
        }
    }
}