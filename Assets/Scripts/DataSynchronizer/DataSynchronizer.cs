using HotDogBush.Infrastructure.Service;
using UnityEngine;

public class DataSynchronizer : MonoBehaviour
{
    private const string SynchronizingKey = "DATA_SYNCHRONIZING";

    private void Awake()
    {
        SynchronizeSavingData();
    }

    public void SynchronizeSavingData()
    {
        Debug.Log($"--- (ROOT) Synchronizing Data...");

        if (!CanSynchronizeData())
        {
            Debug.Log($"--- (ROOT) Already synchronizing Data...");
            return;
        }

        var localStoreService = new LocalStoreService();
        var jsonData = localStoreService.GetJsonData();
        if (string.IsNullOrEmpty(jsonData))
        {
            Debug.Log($"--- (ROOT) Cannot synchronizing Data...");
        }
    }

    private bool CanSynchronizeData()
    {
        return !PlayerPrefs.HasKey(SynchronizingKey);
    }
}