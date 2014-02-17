using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using XSSFramework.Data;

namespace Committinger.XStrangerService.ConsoleTest
{
    class Program
    {
        static Timer timer;
        static DateTime startTime;
        static void Main(string[] args)
        {
            startTime = DateTime.Now;
            timer = new Timer();
            timer.Interval = 5000;
            timer.AutoReset = true;
            timer.Elapsed += timer_Elapsed;
            timer.Start();
            //Console.Write(Encoding.UTF8.BodyName);

            //var helper = DataHelper.CreateHelper();
            //StringBuilder sqlBuilder = new StringBuilder();
            //sqlBuilder.Append("INSERT INTO `xss`.`T_MESSAGE`(`type`,`from`,`to`,`content`,`forwardtime`)");
            //sqlBuilder.Append("VALUES(@type,@from,@to,@content,@forwardtime)");

            //IEnumerable<DbParameter> parameterList = new List<MySqlParameter>(){
            //new MySqlParameter("@type", MySqlDbType.String) { Value = "0" },
            //new MySqlParameter("@from", MySqlDbType.String) { Value = "AABBCC" },
            //new MySqlParameter("@to", MySqlDbType.String) { Value = "DDEEFF"},
            //new MySqlParameter("@content", MySqlDbType.String) { Value = "中文English" },
            //new MySqlParameter("@forwardtime", MySqlDbType.DateTime) { Value = DateTime.Now }
            //};

            //Console.WriteLine(helper.InsertAndGetId(sqlBuilder.ToString(), parameterList));




            Console.ReadKey();
        }

        static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine(DateTime.Now.Subtract(startTime).TotalSeconds);

        }
    }
}
