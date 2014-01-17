
using Autofac.Extras.DynamicProxy2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSSFramework.AOP;
using XSSFramework.Log;
using XSSFramework.Serialize;

namespace ConsoleTest
{
    [Intercept(typeof(Logger))]
    //[Intercept(typeof(LogInteceptor))]
    public class AOPTest
    {
        public virtual int add(int num1, int num2)
        {
            return num1 + num2;
        }

        public virtual AOPTest add(AOPTest data1, AOPTest data2)
        {
            if (data1 == null)
                return data2;
            if (data2 == null)
                throw new Exception("data2不能为空");
            return new AOPTest() { DataInt = data1.DataInt + data2.DataInt, DataString = data1.DataString + data2.DataString };
        }

        public static void Run()
        {
            LoggingTest();
        }

        private static void LoggingTest()
        {
            LogUtils.LogLevel = LogLevelEnum.Debug;

            Console.WriteLine(new AOPTest().add(100, 200));
            Console.WriteLine(ModuleInjector.Inject<AOPTest>().add(100, 200));

            AOPTest data1 = new AOPTest() { DataInt = 300, DataString = "111" };
            AOPTest data2 = new AOPTest() { DataInt = 500, DataString = "222" };

            Console.WriteLine(XmlUtils.SerializeData(new AOPTest().add(data1, data2)));
            Console.WriteLine(XmlUtils.SerializeData(ModuleInjector.Inject<AOPTest>().add(data1, data2)));
            Console.WriteLine(XmlUtils.SerializeData(ModuleInjector.Inject<AOPTest>().add(data1, null)));
        }

        public int DataInt { get; set; }
        public string DataString { get; set; }







    }
}
