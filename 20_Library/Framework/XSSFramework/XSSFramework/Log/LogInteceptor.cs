using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using XSSFramework.Serialize;

namespace XSSFramework.Log
{
    public class LogInteceptor : StandardInterceptor
    {
        private const string ConstantCallMethod = "Call method:";
        private const string ConstantParameters = "Parameters:";
        private const string ConstantExceptionThrowed = "Exception throwed by method：";
        private const string ConstantMethodReturn = "Method return:";
        private const string ConstantResult = "Result:";

        protected override void PerformProceed(IInvocation invocation)
        {

            try
            {
                base.PerformProceed(invocation);
            }
            catch (Exception ex)
            {
                StringBuilder nameBuilder = new StringBuilder();
                nameBuilder.AppendLine(ConstantExceptionThrowed);
                nameBuilder.AppendLine(invocation.Method.Name);
                LogUtils.Error(nameBuilder, ex);

                throw ex;
            }
        }

        protected override void PreProceed(IInvocation invocation)
        {
            if (LogUtils.LogLevel == LogLevelEnum.Debug)
            {
                StringBuilder logBuilder = new StringBuilder();
                logBuilder.Append(ConstantCallMethod).AppendLine();
                logBuilder.Append(invocation.Method.Name).AppendLine();
                logBuilder.Append(ConstantParameters).AppendLine();
                Array.ForEach(invocation.Arguments, t => logBuilder.Append(XmlUtils.SerializeData(t)).AppendLine());
                LogUtils.Debug(logBuilder);
            }
            base.PreProceed(invocation);

        }

        protected override void PostProceed(IInvocation invocation)
        {
            if (LogUtils.LogLevel == LogLevelEnum.Debug)
            {
                StringBuilder logBuilder = new StringBuilder();
                logBuilder.Append(ConstantMethodReturn).AppendLine();
                logBuilder.Append(invocation.Method.Name).AppendLine();
                logBuilder.Append(ConstantResult).AppendLine();
                logBuilder.Append(XmlUtils.SerializeData(invocation.ReturnValue)).AppendLine();
                LogUtils.Debug(logBuilder);
            }
            base.PostProceed(invocation);
        }
    }
}
