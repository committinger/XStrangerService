using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.DynamicProxy2;
using XSSFramework.Log;
//using Castle.DynamicProxy;

namespace XSSFramework.AOP
{
    public sealed class ModuleInjector
    {
        private ModuleInjector() { }

        public static T Inject<T>()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<T>().EnableClassInterceptors();
            builder.Register(c => new Logger());
            return builder.Build().Resolve<T>();
        }
    }
}
