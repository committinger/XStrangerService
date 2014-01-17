using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSSFramework.Log;

namespace ConsoleTest
{
    public class LogTest
    {
        public static void Run()
        {
            WriteDebugLog();
        }

        private static void WriteDebugLog()
        {
            string info = "WARNWARNWARN";
            string debug = "DEBUGDEBUGDEBUG";
            LogUtils.LogLevel = LogLevelEnum.Debug;
            LogUtils.Info(info);
            LogUtils.Debug(debug);
        }
    }
}
