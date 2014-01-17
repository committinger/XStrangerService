using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSSFramework.Data;
using XSSFramework.Log;

namespace ConsoleTest
{
    public class DatabaseTest
    {
        public static void Run()
        {
            QueryTest();
        }

        private static void QueryTest()
        {
            var helper = DataHelper.CreateHelper();
            string sql = "select * from Test1 where recid = @recid";
            IEnumerable<DbParameter> parameterList = new List<MySqlParameter>(){
            new MySqlParameter("@recid", DbType.Int64) { Value = 1 }
            };

            DataSet ds = helper.Query(sql, parameterList);


            if (ds != null && ds.Tables != null
                && ds.Tables.Count > 0 && ds.Tables[0] != null
                && ds.Tables[0].Rows != null)
            {

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row != null)
                    {
                        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                        {
                            Console.Write(ds.Tables[0].Columns[i].ColumnName);
                            Console.Write("：\t");
                            Console.Write(row[i]);
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
    }
}
