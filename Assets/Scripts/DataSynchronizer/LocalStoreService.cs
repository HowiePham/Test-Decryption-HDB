using System;
using System.IO;
using HDB.DataNew;
using Newtonsoft.Json;
using FluorineFx;
using FluorineFx.IO;
using UnityEngine;

namespace HotDogBush.Infrastructure.Service
{
    public class LocalStoreService
    {
        // private Guid _keyAndroid = new Guid("aa859713-bbd2-447a-9c89-9762c93ad8da");
        // private Guid _keyAndroid = new Guid("ff2f8b4b-acef-40ab-8b56-694a30d7f07c");

        private Guid _keyAndroid = new Guid("119434de-f870-43da-84c0-4a62fc8d511c");
        private Guid _keyIOS = new Guid("aa859713-bbd2-447a-9c89-9762c93ad8da");
        private string root = "hdb_save";

        private static byte[] _tempBytes = new byte[0];
        private Guid _key;

        private RC4 _rc4;

        private string _fileAndroidPath = $"/data/data/air.com.bigwigmedia.hotdogbush/air.com.bigwigmedia.hotdogbush/Local Store/hdb_next.sav";
        private string _fileIOSPath = $"/Container/Library/Application Support/com.bigwigmedia.hotdogbush/Local Store/hdb_next.sav";

        // private string _filePath = $"D:/Unity/UnityProject/Test-SQLite/Assets/StreamingAssets/Local Store/hdb_next.sav";
        // private string _filePath = $"D:/Unity/UnityProject/Test-SQLite/Assets/StreamingAssets/Local Store/hdb_next_2.sav";
        // private string _filePath = $"D:/Unity/UnityProject/Test-SQLite/Assets/StreamingAssets/Local Store/hdb_next_ios.sav";
        private string _filePath;

        private ASObject _sessionData;
        private string _jsonData;

        public LocalStoreService()
        {
            InitSystem();

            _rc4 = new RC4(_key);
            _sessionData = Load();
        }

        private void InitSystem()
        {
            var platform = Application.platform;

            if (platform == RuntimePlatform.Android)
            {
                _key = _keyAndroid;
                _filePath = _fileAndroidPath;
            }
            else if (platform == RuntimePlatform.IPhonePlayer)
            {
                _key = _keyIOS;
                _filePath = _fileIOSPath;
            }
        }

        private ASObject Load()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    return InitSessionData();
                }

                _tempBytes = File.ReadAllBytes(_filePath);
                _rc4.Crypt(_tempBytes);
                Debug.Log($"LOADED DATA: {DeserializeSessionData(_tempBytes)}");
                return DeserializeSessionData(_tempBytes);
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        private ASObject InitSessionData()
        {
            var data = new ASObject();
            data["maxOpenStage"] = 0;
            data["levelScore"] = new System.Object[0];
            data["stageStars"] = new System.Object[0];
            data["disabledHints"] = new ASObject();
            data["unlockedAchievements"] = new ASObject();
            data["rateShownForLevel"] = new ASObject();
            data["numHappyCustomers"] = 0;
            data["totalHotDogsSold"] = 0;
            data["speedModeBestScore"] = 0;
            data["currentStage"] = -1;
            data["playedAlready"] = false;
            data["gamingServiceEnabled"] = true;
            data["tapToCookPromoWatched"] = false;
            data["stageToDay"] = new ASObject();
            data["booleans"] = new ASObject();
            data["ints"] = new ASObject();
            data["numbers"] = new ASObject();
            data["strings"] = new ASObject();

            return data;
        }

        public void ResetProgress()
        {
            _sessionData = InitSessionData();
            SaveSessionData();
        }

        public object GetItem(string name)
        {
            return _sessionData != null && _sessionData.ContainsKey(name) ? _sessionData[name] : null;
        }

        public void SetItem(string name, object data)
        {
            _sessionData[name] = data;

            SaveSessionData();
        }

        public ASObject GetData()
        {
            return _sessionData;
        }

        public string GetJsonData()
        {
            return _jsonData;
        }

        private void SaveSessionData()
        {
            _tempBytes = SerializeSessionData();
            _rc4.Crypt(_tempBytes);

            try
            {
                File.WriteAllBytes(_filePath, _tempBytes);
            }
            catch (Exception error)
            {
                throw error;
            }
            finally
            {
                _tempBytes = null;
            }
        }

        private byte[] SerializeSessionData()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                var asObject = new ASObject();

                asObject[root] = _sessionData;

                AMFWriter amfWriter = new AMFWriter(memoryStream);

                amfWriter.WriteAMF3Data(asObject);
                amfWriter.Flush();

                return memoryStream.ToArray();
            }
        }

        private ASObject DeserializeSessionData(byte[] data)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                AMFReader amfReader = new AMFReader(memoryStream);

                var deserializedObject = amfReader.ReadAMF3Data() as ASObject;
                _jsonData = JsonConvert.SerializeObject(deserializedObject);
                Debug.Log(_jsonData);

                return deserializedObject[root] as ASObject;
            }
        }
    }
}