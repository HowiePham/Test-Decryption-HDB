using System;
using System.IO;
using System.Text;
using HDB.DataNew;
using Newtonsoft.Json;
using UnityEngine;

namespace HotDogBush.Infrastructure.Service
{
    public class NewLocalStoreService
    {
        private Guid _keyAndroid = new Guid("aa859713-bbd2-447a-9c89-9762c93ad8da");
        private string _filePath = $"D:/Unity/UnityProject/Test-SQLite/Assets/StreamingAssets/Local Store/hdb_next.sav";

        private SaveData _sessionData;
        private RC4 _rc4;
        private string _jsonData;

        public NewLocalStoreService()
        {
            _rc4 = new RC4(_keyAndroid);
            _sessionData = Load();
        }

        private SaveData Load()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    return InitSessionData();
                }

                var encryptedData = File.ReadAllBytes(_filePath);
                var decryptedData = Decrypt(encryptedData);
                var jsonData = Encoding.UTF8.GetString(decryptedData);

                Debug.Log($"LOADED DATA: {jsonData}");
                return JsonConvert.DeserializeObject<SaveData>(jsonData);
            }
            catch (Exception error)
            {
                Debug.LogError(error);
                return InitSessionData();
            }
        }

        private SaveData InitSessionData()
        {
            return new SaveData();
        }

        public void ResetProgress()
        {
            _sessionData = InitSessionData();
            SaveSessionData();
        }

        public object GetItem(string name)
        {
            return _sessionData.GetType().GetProperty(name)?.GetValue(_sessionData, null);
        }

        public void SetItem(string name, object data)
        {
            _sessionData.GetType().GetProperty(name)?.SetValue(_sessionData, data);
            SaveSessionData();
        }

        public SaveData GetData()
        {
            return _sessionData;
        }

        private void SaveSessionData()
        {
            var jsonData = JsonConvert.SerializeObject(_sessionData, Formatting.Indented);
            var encryptedData = Encrypt(Encoding.UTF8.GetBytes(jsonData));

            try
            {
                File.WriteAllBytes(_filePath, encryptedData);
            }
            catch (Exception error)
            {
                Debug.LogError(error);
            }
        }

        private byte[] Encrypt(byte[] data)
        {
            _rc4.Crypt(data);
            return data;
        }

        private byte[] Decrypt(byte[] data)
        {
            _rc4.Crypt(data);
            return data;
        }

        public string GetJsonData()
        {
            return _jsonData;
        }
    }
}