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

        public virtual int InsertAndGetId(string sql, IEnumerable<DbParameter> parameterCollection)
        {
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
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        DataSet ds = new DataSet();
                        cmd.CommandText = "SELECT LAST_INSERT_ID()";
                        DbDataAdapter adapter = GetDbDataAdapter(cmd);
                        adapter.Fill(ds);
                        return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    }
                }
            }
            catch (Exception ex)
            {
                try { connection.Close(); }
                catch { }
                throw ex;
            }
            return -1;
        }

        public virtual int Insert(string sql, IEnumerable<DbParameter> parameterCollection)
        {
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
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                try { connection.Close(); }
                catch { }
                throw ex;
            }
            return -1;
        }
    }
}
