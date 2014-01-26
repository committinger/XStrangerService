﻿using Autofac.Extras.DynamicProxy2;
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
    public class EndMessageHandler : BaseMessageHandler
    {
        protected override void DoHandleMessage(MessageData message, int sequence)
        {
            User sender = UserModule.Instance.GetUserByName(message.UserFrom);
            if (sender != null)
            {
                Conversation c = ConversationModule.Instance.GetConversation(sender);
                ConversationModule.Instance.StopConveration(c);
                MessageModule.Instance.CreateEndMessage(c);
                ConversationModule.Instance.RemoveConveration(c);
            }
        }
    }
}
