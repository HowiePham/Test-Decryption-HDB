public static class IOSHelper
{
    public static string ConvertToApplicationSupportPath(string documentsPath)
    {
        string applicationSupportPath = documentsPath.Replace("Documents", "Library/Application Support");
        return applicationSupportPath;
    }
}