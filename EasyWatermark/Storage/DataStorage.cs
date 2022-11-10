using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace EasyWatermark.Storage
{
    public class DataStorage
    {
        public DictionaryDataMemorizer Memorizer { get; }

        public DataStorage(IDataManager<Dictionary<string, object>> dataManager)
        {
            Memorizer = new DictionaryDataMemorizer(dataManager);
        }

        private string _prefixKey = "";

        public DataStorage(IDataManager<Dictionary<string, object>> dataManager, Type storageForType)
        {
            Memorizer = new DictionaryDataMemorizer(dataManager);
            _prefixKey = storageForType.FullName;
        }

        public DataStorage(Type type) : this(Assembly.GetExecutingAssembly(), type)
        {

        }

        public DataStorage(string fileName)
        {
            Memorizer = new DictionaryDataMemorizer(fileName);
        }

        public DataStorage(string fileName, Type storageForType)
        {
            Memorizer = new DictionaryDataMemorizer(fileName);
            _prefixKey = storageForType.FullName;
        }

        public DataStorage(Assembly execAssembly)
        {
            var assFileName = execAssembly.Location;
            var assPath = new FileInfo(assFileName).Directory.FullName;
            var fileName = Path.Combine(assPath, "data.ini");
            Memorizer = new DictionaryDataMemorizer(fileName);
        }

        public DataStorage(Assembly execAssembly, string fileName)
        {
            var assFileName = execAssembly.Location;
            var assPath = new FileInfo(assFileName).Directory.FullName;
            fileName = fileName.Split('\\').LastOrDefault();
            var dataFileName = Path.Combine(assPath, fileName);
            Memorizer = new DictionaryDataMemorizer(dataFileName);
        }

        public DataStorage(Assembly execAssembly, Type storageForType)
        {
            var assFileName = execAssembly.Location;
            var assPath = new FileInfo(assFileName).Directory.FullName;
            var fileName = Path.Combine(assPath, "data.ini");
            Memorizer = new DictionaryDataMemorizer(fileName);
            _prefixKey = storageForType.FullName;
        }

        public DataStorage(Assembly execAssembly, Type storageForType, string fileName)
        {
            var assFileName = execAssembly.Location;
            var assPath = new FileInfo(assFileName).Directory.FullName;
            fileName = fileName.Split('\\').LastOrDefault();
            var dataFileName = Path.Combine(assPath, fileName);
            Memorizer = new DictionaryDataMemorizer(dataFileName);
            _prefixKey = storageForType.FullName;
        }

        private Dictionary<string, object> _lastData;

        private bool _useLastData;

        private bool _allowSaveToFile = true;

        private Dictionary<string, object> LoadData()
        {
            if(_useLastData && _lastData != null)
            {
                return _lastData;
            }
            var data = Memorizer.Load();
            _lastData = data;
            return data;
        }

        private object GetObjectValue(string name)
        {
            if (_prefixKey != null)
            {
                name = _prefixKey + "-" + name;
            }
            var data = LoadData() ?? new Dictionary<string, object>();
            if (!data.ContainsKey(name))
            {
                return null;
            }
            return data[name];
        }

        public object GetValue(string name, object defaultIfNotFound = null)
        {
            var value = GetObjectValue(name);
            return value ?? defaultIfNotFound;

        }

        public int GetInt32(string name, int def = 0)
        {
            var value = GetObjectValue(name);
            if(value == null)
            {
                return def;
            }
            return Convert.ToInt32(value);
        }

        public long GetInt64(string name, long def = 0)
        {
            var value = GetObjectValue(name);
            if (value == null)
            {
                return def;
            }
            return Convert.ToInt64(value);
        }

        public double GetDouble(string name, double def = 0)
        {
            var value = GetObjectValue(name);
            if (value == null)
            {
                return def;
            }
            return Convert.ToDouble(value);
        }

        public string GetString(string name, string def = null)
        {
            var value = GetObjectValue(name);
            if (value == null)
            {
                return def;
            }
            return value.ToString();
        }

        public bool GetBool(string name, bool def = false)
        {
            var value = GetObjectValue(name);
            if (value == null)
            {
                return def;
            }
            return Convert.ToBoolean(value);

        }

        public DateTime GetDateTime(string name, DateTime def = default(DateTime))
        {
            var value = GetObjectValue(name);
            if (value == null)
            {
                return def;
            }
            var dt = JsonConvert.DeserializeObject<DateTime>(value.ToString());
            return dt;
        }

        public T GetAs<T>(string name, T def = default(T)) where T : class
        {
            var value = GetObjectValue(name);
            if (value == null)
            {
                return def;
            }
            var rs = JsonConvert.DeserializeObject<T>(value.ToString());
            return rs;
        }

        public static object SynChronizedObject = new object();

        public void AddOrUpdate(string name, object value)
        {
            if (_prefixKey != null)
            {
                name = _prefixKey + "-" + name;
            }
            if (value == null) return;
            var data = LoadData() ?? new Dictionary<string, object>();

            var valueJson = JsonConvert.SerializeObject(value);
            if (!data.ContainsKey(name))
            {
                data.Add(name, value);
            }
            else
            {
                data[name] = value;
            }
            lock (SynChronizedObject)
            {
                if (_allowSaveToFile)
                {
                    Memorizer.Update(data);
                }
            }
        }

        public void BeginUpdate()
        {
            _useLastData = true;
            _allowSaveToFile = false;
        }

        public void EndUpdate()
        {
            _useLastData = false;
            _allowSaveToFile = true;
            Memorizer.Update(_lastData);
        }
    }
}
