using Autofac.Extras.DynamicProxy2;
using Committinger.XStrangerServic.Core.DA;
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
    public class NormalMessageHandler : BaseMessageHandler
    {
        protected override int SaveMessage(MessageData msg)
        {
            return MessageDA.Instance.SaveMessage(msg);

        }

        protected override void DoHandleMessage(MessageData message, int sequence)
        {
            User sender = UserModule.Instance.GetUserByName(message.UserFrom);
            if (sender != null)
            {
                if (string.IsNullOrEmpty(message.UserTo))
                {
                    startNewConversation(sender);
                }
                else
                {
                    addMessageToConversation(sender, message);
                }
            }
        }

        protected virtual void addMessageToConversation(User sender, MessageData message)
        {
            Conversation conversation = ConversationModule.Instance.GetConversation(sender);
            if (conversation.ConversationOpen)
                SaveMessage(message);
            else
                MessageModule.Instance.CreateFailMessage(sender, "conversation not available");

        }

        protected virtual void startNewConversation(User sender)
        {
            if (!sender.Available)
            {
                LogUtils.AsyncError(sender.Name + "锁定");
                MessageDA.Instance.SaveMessage(new MessageData()
                {
                    MessageType = MessageType.Reject,
                    UserTo = sender.Name,
                    Time = DateTime.Now.ToString(ConstantDateTimeFormat),
                    sequence = 0,
                    Content = "you are invited to a new conversation"
                });
                return;
            }
            else
            {
                LogUtils.AsyncError(sender.Name + "未锁定");
                sender.Available = false;
                try
                {
                    User receiver = null;
                    for (int count = 0; count < 3; count++)
                    {
                        receiver = UserModule.Instance.GetRandomUser(sender.Name);
                        LogUtils.AsyncError(sender.Name + "获取用户:" + count);
                        if (receiver != null)
                        {
                            LogUtils.AsyncError(sender.Name + "获取用户成功:" + count);
                            break;
                        }
                    }

                    if (receiver != null)
                    {
                        receiver.Available = false;
                        try
                        {
                            LogUtils.AsyncError(sender.Name + "获取用户成功，开始新会话");
                            ConversationModule.Instance.StartNewConversation(sender, receiver);
                        }
                        catch
                        {
                            receiver.Available = true;
                        }
                    }
                    else
                    {
                        LogUtils.AsyncError(sender.Name + "获取用户失败");
                        sender.Available = true;
                        MessageDA.Instance.SaveMessage(new MessageData()
                        {
                            MessageType = MessageType.Reject,
                            UserTo = sender.Name,
                            Time = DateTime.Now.ToString(ConstantDateTimeFormat),
                            sequence = 0,
                            Content = "failed to get free user"
                        });
                    }
                }
                catch
                {
                    sender.Available = true;
                }
            }
        }
    }
}
