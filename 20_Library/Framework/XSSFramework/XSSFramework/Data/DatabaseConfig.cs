using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSSFramework.Serialize;

namespace XSSFramework.Data
{
    [Serializable]
    public class DatabaseConfig
    {
        private const string ConstantDatabaseConfigPath = "config\\database.xml";
        private static object _lockObj = new object();
        private static DatabaseConfig _instance;
        public static DatabaseConfig Instance
        {
            get
            {
                if (_instance == null)
                    lock (_lockObj)
                    {
                        DatabaseConfig tmpConfig = LoadDatabasebConfig();
                        if (_instance == null)
                            _instance = tmpConfig;
                    }
                return _instance;
            }
        }

        private static DatabaseConfig LoadDatabasebConfig()
        {
            using (FileStream fs = File.OpenRead(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConstantDatabaseConfigPath)))
            {
                return XmlUtils.Deserialize<DatabaseConfig>(fs);
            }
        }

        public string Type { get; set; }
        public string ConnectionString { get; set; }
    }
}
