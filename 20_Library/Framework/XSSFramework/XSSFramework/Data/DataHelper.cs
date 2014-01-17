using Autofac;
using Autofac.Extras.DynamicProxy2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSSFramework.AOP;
using XSSFramework.Log;

namespace XSSFramework.Data
{
    [Intercept(typeof(Logger))]
    public abstract class DataHelper
    {
        private static IContainer _dataHelperContainer;
        private static object _lockObj = new object();

        public static DataHelper CreateHelper()
        {
            if (_dataHelperContainer == null)
            {
                lock (_lockObj)
                {
                    ContainerBuilder cb = new ContainerBuilder();
                    cb.Register(c => ModuleInjector.Inject<MySqlHelper>()).Named<DataHelper>("MySql");
                    IContainer tmpContainer = cb.Build();
                    if (_dataHelperContainer == null)
                        _dataHelperContainer = tmpContainer;
                    else
                        tmpContainer.Dispose();
                }
            }
            return _dataHelperContainer.ResolveNamed<DataHelper>(DatabaseConfig.Instance.Type.ToString());
        }
        protected abstract DbConnection GetConnection(string connectionString);
        protected abstract DbDataAdapter GetDbDataAdapter(DbCommand cmd);

        public virtual DataSet Query(string sql, IEnumerable<DbParameter> parameterCollection)
        {
            DataSet ds = new DataSet();
            DbConnection connection = GetConnection(DatabaseConfig.Instance.ConnectionString);
            try
            {
                connection.Open();
                using (DbCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = sql;
                    if (parameterCollection != null)
                        foreach (var p in parameterCollection)
                            cmd.Parameters.Add(p);
                    DbDataAdapter adapter = GetDbDataAdapter(cmd);
                    adapter.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                try { connection.Close(); }
                catch { }
                throw ex;
            }
            return ds;
        }
    }
}
