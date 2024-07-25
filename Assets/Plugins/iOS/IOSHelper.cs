using System;
using System.Runtime.InteropServices;
using UnityEngine;

public static class IOSHelper
{
    [DllImport("__Internal")]
    private static extern IntPtr getApplicationSupportDirectory();

    public static string GetApplicationSupportDirectory()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            IntPtr ptr = getApplicationSupportDirectory();
            return Marshal.PtrToStringAnsi(ptr);
        }
        else
        {
            return string.Empty;
        }
    }

    public static string ConvertToApplicationSupportPath(string documentsPath)
    {
        string applicationSupportPath = documentsPath.Replace("Documents", "Library/Application Support");
        return applicationSupportPath;
    }
}