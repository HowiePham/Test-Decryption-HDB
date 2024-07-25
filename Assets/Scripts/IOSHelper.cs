using System.Runtime.InteropServices;
using UnityEngine;

public static class IOSHelper
{
    [DllImport("__Internal")]
    private static extern string _getApplicationSupportDirectory();

    public static string GetApplicationSupportDirectory()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return _getApplicationSupportDirectory();
        }
        else
        {
            return string.Empty;
        }
    }
}