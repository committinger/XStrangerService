using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSSFramework.IO;

namespace XSSFramework.Log
{
    public sealed class LogUtils
    {
        public static LogLevelEnum LogLevel;
        private const string ConstantDateTimeFormat = "yyyy-MM-dd HH:mm:ss fff";
        private const string ConstantNullString = "null";
        private const string ConstantSplitString = "    ";
        private const string ConstantDebugLevel = "DEBUG";
        private const string ConstantInfoLevel = "INFO";
        private const string ConstantErrorLevel = "ERROR";
        private const string ConstantMessage = "Message：";
        private const string ConstantException = "Exception capture：";
        private const string ConstantStacktrace = "Stacktrace：";

        private const string ConstantLogPath = @"logs\";
        private const string ConstantLogPathLogFileDateFormat = "yyyyMMdd";
        private const string ConstantLogPathLogDot = ".";
        private const string ConstantLogPathLogFileAffix = "txt";

        static Action<object, Exception> actionError = (object message, Exception ex) => Error(message, ex);
        static Action<object> actionInfo = (object message) => Info(message);
        static Action<object> actionDebug = (object message) => Debug(message);

        public static void Error(object message, Exception ex = null)
        {
            if (ex == null)
                WriteLog(message, ConstantErrorLevel);

            StringBuilder exBuilder = new StringBuilder();
            exBuilder.Append(ConstantMessage).Append(message.ToString()).AppendLine();
            exBuilder.Append(ConstantException).Append(ex.Message).AppendLine();
            exBuilder.AppendLine(ConstantStacktrace);
            exBuilder.AppendLine(ex.StackTrace);

            WriteLog(exBuilder, ConstantErrorLevel);
        }
        public static void AsyncError(object message, Exception ex = null)
        {
            actionError.BeginInvoke(message, ex, null, null);
        }

        public static void Debug(object message)
        {
            if (LogLevel == LogLevelEnum.Debug)
                WriteLog(message, ConstantDebugLevel);
        }
        public static void AsyncDebug(object message)
        {
            actionDebug.BeginInvoke(message, null, null);
        }

        public static void Info(object message)
        {
            WriteLog(message, ConstantInfoLevel);
        }
        public static void AsyncInfo(object message)
        {
            actionInfo.BeginInvoke(message, null, null);
        }

        private static void WriteLog(object message, string level)
        {
            StringBuilder logBuilder = new StringBuilder().AppendLine();
            logBuilder.Append(DateTime.Now.ToString(ConstantDateTimeFormat)).Append(ConstantSplitString).Append(level).AppendLine();
            logBuilder.AppendLine(message == null ? ConstantNullString : message.ToString());
            FileUtils.ThreadsafeAppendText(GetLogFileFullPath(), logBuilder.ToString());
        }

        private static string GetLogFileFullPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConstantLogPath + DateTime.Now.ToString(ConstantLogPathLogFileDateFormat) + ConstantLogPathLogDot + ConstantLogPathLogFileAffix);
        }
    }
}
