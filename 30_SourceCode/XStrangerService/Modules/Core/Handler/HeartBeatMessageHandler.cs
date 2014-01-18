using Autofac.Extras.DynamicProxy2;
using Committinger.XStrangerService.ServiceInterface.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSSFramework.Log;

namespace Committinger.XStrangerServic.Core.Handler
{
    [Intercept(typeof(Logger))]
    class HeartBeatMessageHandler : MessageHandler
    {
        protected override void DoHandleMessage(MessageData message, int sequence)
        {
            UserModule.Instance.GetUserByName(message.UserFrom).HeartBeat();
        }
    }
}
