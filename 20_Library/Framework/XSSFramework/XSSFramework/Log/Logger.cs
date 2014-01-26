using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSSFramework.Serialize;

namespace XSSFramework.Log
{
    public class Logger : IInterceptor
    {
        private const string cCallMethod = "Call method:";
        private const string cParameters = "Parameters:";
        private const string cExceptionThrowed = "Exception throwed by method：";
        private const string cMethodReturn = "Method return:";
        private const string cResult = "Result:";
        private const string cSpace = "\t";
        private const string cMethod = "Method:";
        private const string cTarget = "Target:";

        public void Intercept(IInvocation invocation)
        {

            logBeforeInvocation(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                StringBuilder nameBuilder = new StringBuilder();
                nameBuilder.AppendLine(cExceptionThrowed);
                nameBuilder.Append(cSpace).Append(cTarget).Append(invocation.TargetType.FullName).AppendLine();
                nameBuilder.Append(cSpace).Append(cMethod).Append(invocation.Method.Name).AppendLine();
                nameBuilder.Append(cParameters).AppendLine();
                Array.ForEach(invocation.Arguments, t => nameBuilder.Append(cSpace).Append(XmlUtils.SerializeData(t)).AppendLine());
                LogUtils.Error(nameBuilder, ex);
                throw ex;
            }
            logAfterInvocation(invocation);


        }

        private void logAfterInvocation(IInvocation invocation)
        {
            if (LogUtils.LogLevel == LogLevelEnum.Debug)
            {
                StringBuilder lb = new StringBuilder();
                lb.Append(cMethodReturn).AppendLine();
                lb.Append(cTarget).Append(cSpace).Append(invocation.TargetType.FullName).AppendLine();
                lb.Append(cMethod).Append(cSpace).Append(invocation.Method.Name).AppendLine();
                lb.Append(cResult).AppendLine();
                lb.Append(cSpace).Append(XmlUtils.SerializeData(invocation.ReturnValue)).AppendLine();
                LogUtils.Debug(lb);
            }
        }

        private void logBeforeInvocation(IInvocation invocation)
        {
            if (LogUtils.LogLevel == LogLevelEnum.Debug)
            {
                StringBuilder logBuilder = new StringBuilder();
                logBuilder.Append(cCallMethod).AppendLine();
                logBuilder.Append(cSpace).Append(cTarget).Append(invocation.TargetType.FullName).AppendLine();
                logBuilder.Append(cSpace).Append(cMethod).Append(invocation.Method.Name).AppendLine();
                logBuilder.Append(cParameters).AppendLine();
                Array.ForEach(invocation.Arguments, t => logBuilder.Append(cSpace).Append(XmlUtils.SerializeData(t)).AppendLine());
                LogUtils.Debug(logBuilder);
            }
        }
    }
}
