using Autofac.Extras.DynamicProxy2;
using Committinger.XStrangerServic.Core.DA;
using Committinger.XStrangerServic.Core.MessageHandler;
using Committinger.XStrangerService.ServiceInterface.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSSFramework.AOP;
using XSSFramework.Log;

namespace Committinger.XStrangerServic.Core
{
    [Intercept(typeof(Logger))]
    public class MessageModule
    {
        public const string ConstantDateTimeFormat = "yyyy-MM-dd HH:mm:ss +0800";
        public static MessageModule Instance { get { return ModuleInjector.Inject<MessageModule>(); } }

        public virtual MessageCollectionData Process(MessageCollectionData messageCollection)
        {

            MessageData data = BaseMessageHandler.PreProcess(messageCollection);
            var handler = BaseMessageHandler.CreateHandler(data.MessageType);
            return handler.HandleMessage(data, messageCollection.Sequence);
        }

        private static object _lockObj = new object();
        private static Action<Conversation> _createBreakMessageAction;
        public static Action<Conversation> CreateBreakMessageAction
        {
            get
            {
                if (_createBreakMessageAction == null)
                {
                    lock (_lockObj)
                    {
                        Action<Conversation> tempAction = t =>
                        {
                            if (t != null && t.Originator != null && t.Recipient != null)
                            {
                                MessageData msg1 = new MessageData()
                                {
                                    UserFrom = t.Originator.Name,
                                    UserTo = t.Recipient.Name,
                                    Time = DateTime.Now.ToString(ConstantDateTimeFormat),
                                    Content = "是否继续",
                                    MessageType = MessageType.ConversationBreak
                                };

                                MessageData msg2 = new MessageData()
                                {
                                    UserFrom = t.Recipient.Name,
                                    UserTo = t.Originator.Name,
                                    Time = DateTime.Now.ToString(ConstantDateTimeFormat),
                                    Content = "是否继续",
                                    MessageType = MessageType.ConversationBreak
                                };

                                MessageDA.Instance.SaveMessage(msg1);
                                MessageDA.Instance.SaveMessage(msg2);
                            }
                        };
                        if (_createBreakMessageAction == null)
                            _createBreakMessageAction = tempAction;
                    }
                }
                return _createBreakMessageAction;
            }
        }

        public virtual void CreateInviteMessage(Conversation c)
        {
            if (c != null && c.Originator != null && c.Recipient != null)
            {
                MessageData msg = CreateMessage(c.Originator.Name, c.Recipient.Name, MessageType.Invite);
                MessageDA.Instance.SaveMessage(msg);
            }
        }

        public virtual void CreateAcceptMessage(Conversation c)
        {
            if (c != null && c.Originator != null && c.Recipient != null)
            {
                MessageData msg = CreateMessage(c.Recipient.Name, c.Originator.Name, MessageType.Accept);
                MessageDA.Instance.SaveMessage(msg);
            }
        }

        public virtual void CreateRejectMessage(Conversation c)
        {
            if (c != null && c.Originator != null && c.Recipient != null)
            {
                MessageData msg = CreateMessage(c.Recipient.Name, c.Originator.Name, MessageType.Reject);
                MessageDA.Instance.SaveMessage(msg);
            }
        }

        public virtual void CreateEndMessage(Conversation c)
        {
            if (c != null && c.Originator != null && c.Recipient != null)
            {
                MessageData msg1 = CreateMessage(c.Recipient.Name, c.Originator.Name, MessageType.ConversationEnded);
                MessageDA.Instance.SaveMessage(msg1);
                MessageData msg2 = CreateMessage(c.Originator.Name, c.Recipient.Name, MessageType.ConversationEnded);
                MessageDA.Instance.SaveMessage(msg2);
            }
        }

        public virtual void CreateContinueMessage(Conversation c, User sender)
        {
            if (c != null && c.Originator != null && c.Recipient != null)
            {
                string receiverName = string.Equals(c.Originator.Name, sender.Name) ? c.Recipient.Name : c.Originator.Name;
                MessageData msg = CreateMessage(sender.Name, receiverName, MessageType.ConversationContinue);
                MessageDA.Instance.SaveMessage(msg);
            }
        }

        public virtual void CreateFailMessage(User sender, string message)
        {
            MessageData msg = CreateMessage(string.Empty, sender.Name, MessageType.BeRejected, message);
            MessageDA.Instance.SaveMessage(msg);
        }

        public virtual MessageData CreateMessage(string from, string to, int msgType, string content = "")
        {
            return new MessageData()
                {
                    UserFrom = from,
                    UserTo = to,
                    Time = DateTime.Now.ToString(ConstantDateTimeFormat),
                    MessageType = msgType,
                    Content = string.IsNullOrEmpty(content) ? buildContent(msgType) : content
                };
        }

        private string buildContent(int msgType)
        {
            if (msgType == MessageType.Invite)
                return "you are invited to an conversation";
            if (msgType == MessageType.BeRejected)
                return "your invitation is rejected";
            return string.Empty;
        }
    }
}
