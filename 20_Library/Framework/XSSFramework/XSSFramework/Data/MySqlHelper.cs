using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSSFramework.Data
{
    public class MySqlHelper : DataHelper
    {
        protected override DbDataAdapter GetDbDataAdapter(DbCommand cmd)
        {
            return new MySqlDataAdapter(cmd as MySqlCommand);
        }

        protected override DbConnection GetConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }
    }
}
