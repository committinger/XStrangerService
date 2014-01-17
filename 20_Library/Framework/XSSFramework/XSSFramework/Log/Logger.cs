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
        private const string ConstantCallMethod = "Call method:";
        private const string ConstantParameters = "Parameters:";
        private const string ConstantExceptionThrowed = "Exception throwed by method：";
        private const string ConstantMethodReturn = "Method return:";
        private const string ConstantResult = "Result:";
        private const string ConstantSpace = "\t";
        private const string ConstantMethod = "Method:";
        private const string ConstantTarget = "Target:";

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
                nameBuilder.AppendLine(ConstantExceptionThrowed);
                nameBuilder.Append(ConstantSpace).Append(ConstantTarget).Append(invocation.TargetType.FullName).AppendLine();
                nameBuilder.Append(ConstantSpace).Append(ConstantMethod).Append(invocation.Method.Name).AppendLine();
                LogUtils.Error(nameBuilder, ex);
                throw ex;
            }
            logAfterInvocation(invocation);


        }

        private void logAfterInvocation(IInvocation invocation)
        {
            if (LogUtils.LogLevel == LogLevelEnum.Debug)
            {
                StringBuilder logBuilder = new StringBuilder();
                logBuilder.Append(ConstantMethodReturn).AppendLine();
                logBuilder.Append(ConstantSpace).Append(ConstantTarget).Append(invocation.TargetType.FullName).AppendLine();
                logBuilder.Append(ConstantSpace).Append(ConstantMethod).Append(invocation.Method.Name).AppendLine();
                logBuilder.Append(ConstantResult).AppendLine();
                logBuilder.Append(ConstantSpace).Append(XmlUtils.SerializeData(invocation.ReturnValue)).AppendLine();
                LogUtils.Debug(logBuilder);
            }
        }

        private void logBeforeInvocation(IInvocation invocation)
        {
            if (LogUtils.LogLevel == LogLevelEnum.Debug)
            {
                StringBuilder logBuilder = new StringBuilder();
                logBuilder.Append(ConstantCallMethod).AppendLine();
                logBuilder.Append(ConstantSpace).Append(ConstantTarget).Append(invocation.TargetType.FullName).AppendLine();
                logBuilder.Append(ConstantSpace).Append(ConstantMethod).Append(invocation.Method.Name).AppendLine();
                logBuilder.Append(ConstantParameters).AppendLine();
                Array.ForEach(invocation.Arguments, t => logBuilder.Append(ConstantSpace).Append(XmlUtils.SerializeData(t)).AppendLine());
                LogUtils.Debug(logBuilder);
            }
        }
    }
}
