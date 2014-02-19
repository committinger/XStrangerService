using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Encoding.Default.EncodingName);
            string str = "你好啊";
            //byte[] bs = Encoding.GetEncoding("GB18030").GetBytes(str);
            //byte[] bs = Encoding.GetEncoding("GB2312").GetBytes(str);
            //byte[] bs = Encoding.GetEncoding("GBK").GetBytes(str);
            //byte[] bs = Encoding.GetEncoding("iso-8859-1").GetBytes(str);
            //byte[] bs = Encoding.GetEncoding("GBK").GetBytes(str);
            string result = Encoding.UTF8.GetString(bs);
            Console.WriteLine(result);

            byte[] bs2 = Encoding.UTF8.GetBytes(result);
            string result2 = Encoding.GetEncoding("GBK").GetString(bs2);
            Console.WriteLine(result2);
            Console.ReadKey();
        }
        //static void Main(string[] args)
        //{
        //    MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection();
        //    connection.ConnectionString = "Server=192.168.1.100;Port=3306;Database=xss;Uid=xss;Pwd=xss";
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        connection.Open();
        //        using (var command = connection.CreateCommand())
        //        {
        //            command.CommandText = "select * from Test1";
        //            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
        //            adapter.Fill(ds);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //    if (ds != null && ds.Tables != null
        //        && ds.Tables.Count > 0 && ds.Tables[0] != null
        //        && ds.Tables[0].Rows != null)
        //    {

        //        foreach (DataRow row in ds.Tables[0].Rows)
        //        {
        //            if (row != null)
        //            {
        //                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
        //                {
        //                    Console.Write(ds.Tables[0].Columns[i].ColumnName);
        //                    Console.Write("：\t");
        //                    Console.Write(row[i]);
        //                    Console.WriteLine();
        //                }
        //            }
        //        }
        //    }
        //    Console.ReadKey();
        //}
    }
}
