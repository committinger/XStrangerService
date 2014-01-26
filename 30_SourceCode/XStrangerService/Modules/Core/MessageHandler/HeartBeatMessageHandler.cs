using Autofac.Extras.DynamicProxy2;
using Committinger.XStrangerService.ServiceInterface.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSSFramework.Log;

namespace Committinger.XStrangerServic.Core.MessageHandler
{
    [Intercept(typeof(Logger))]
    public class HeartBeatMessageHandler : BaseMessageHandler
    {
        protected override void DoHandleMessage(MessageData message, int sequence)
        {
            User u = UserModule.Instance.GetUserByName(message.UserFrom);
            if (u != null) u.HeartBeat();
        }
    }
}
