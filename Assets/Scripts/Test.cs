using System;
using System.IO;
using HotDogBush.Infrastructure.Service;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] private Text testText;
    [SerializeField] private Text testText_json;

    private string androidDirectoryPath = "/data/data/air.com.bigwigmedia.hotdogbush/air.com.bigwigmedia.hotdogbush/Local Store";
    private string androidFilePath = "/data/data/air.com.bigwigmedia.hotdogbush/air.com.bigwigmedia.hotdogbush/Local Store/hdb_next.sav";

    private string iOSDirectoryPath = "/Container/Library/Application Support/com.bigwigmedia.hotdogbush/Local Store";
    private string iOSFilePath = "/Container/Library/Application Support/com.bigwigmedia.hotdogbush/Local Store/hdb_next.sav";

    private string directoryPath;
    private string filePath;

    private void Start()
    {
        var dataPath = Application.persistentDataPath;
        InitSystem();
        Debug.Log($"--- (ROOT) persistent data path: {dataPath}");
    }

    private void InitSystem()
    {
        var platform = Application.platform;

        if (platform == RuntimePlatform.Android)
        {
            filePath = androidFilePath;
            directoryPath = androidDirectoryPath;
        }
        else if (platform == RuntimePlatform.IPhonePlayer)
        {
            filePath = iOSFilePath;
            directoryPath = iOSDirectoryPath;
        }
    }

    public void RunTest()
    {
        testText.text = Directory.Exists(directoryPath) ? $"------> Exist directory Path {directoryPath} \n" : $"------> Not Exist directory Path {directoryPath} \n";
        testText.text += File.Exists(filePath) ? "------> Exist file hdb_next.sav" : "------> Not Exist file hdb_next.sav";

        SynchronizeSavingData();
    }

    [ContextMenu("TEST")]
    private void SynchronizeSavingData()
    {
        Debug.Log($"--- (ROOT) Synchronizing Data...");

        var localStoreService = new LocalStoreService();
        var jsonData = localStoreService.GetJsonData();

        if (string.IsNullOrEmpty(jsonData))
        {
            Debug.Log($"--- (ROOT) Cannot synchronizing Data...");
            testText_json.text = "Cannot synchronizing Data...";
        }
        else
        {
            testText_json.text = jsonData;
        }
    }
}