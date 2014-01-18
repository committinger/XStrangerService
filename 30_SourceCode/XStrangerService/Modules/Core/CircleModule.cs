using Committinger.XStrangerServic.Core.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSSFramework.AOP;

namespace Committinger.XStrangerServic.Core
{
    public class CircleModule
    {
        public static UserModule Instance { get { return ModuleInjector.Inject<UserModule>(); } }

        public virtual Circle GetCircleByKey(string key)
        {
            return CircleDA.Instance.GetCircleByKey(key);
        }
    }
}
