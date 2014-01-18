using Autofac.Extras.DynamicProxy2;
using Committinger.XStrangerServic.Core.DA;
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
    class NormalMessageHandler : MessageHandler
    {
        protected override int SaveMessage(MessageData msg)
        {
            return MessageDA.Instance.SaveMessage(msg);
        }

        protected override void DoHandleMessage(MessageData message, int sequence)
        {
            User sender = UserModule.Instance.GetUserByName(message.UserFrom);

            if (string.IsNullOrEmpty(message.UserTo))
            {
                startNewConversation(sender);
            }
            else
            {
                addMessageToConversation(sender, message);
            }
        }

        protected virtual void addMessageToConversation(User sender, MessageData message)
        {
            Conversation conversation = ConversationModule.Instance.GetConversation(sender);
            if (conversation.ConversationOpen)
                SaveMessage(message);
            else
                MessageModule.Instance.CreateFailMessage(sender, "会话没有开放");

        }

        protected virtual void startNewConversation(User sender)
        {
            User receiver = UserModule.Instance.GetRandomUser();
            if (receiver == null)
            {

                SaveMessage(new MessageData()
                {
                    MessageType = MessageType.Reject,
                    UserTo = sender.Name,
                    Time = DateTime.Now.ToString(ConstantDateTimeFormat),
                    sequence = 0,
                    Content = "查询空闲用户失败"
                });
            }
            ConversationModule.Instance.StartNewConversation(sender, receiver);
        }
    }
}
