using Autofac;
using Committinger.XStrangerServic.Core.DA;
using Committinger.XStrangerService.ServiceInterface.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSSFramework.AOP;

namespace Committinger.XStrangerServic.Core.Handler
{
    public abstract class MessageHandler
    {
        public const string ConstantDateTimeFormat = "yyyy-MM-dd HH:mm:ss +0800";

        private static IContainer _msgHelperContainer;
        private static object _lockObj = new object();

        public static MessageHandler CreateHandler(int messageType)
        {
            if (_msgHelperContainer == null)
            {
                lock (_lockObj)
                {
                    ContainerBuilder cb = new ContainerBuilder();
                    cb.Register(c => ModuleInjector.Inject<NormalMessageHandler>()).Named<MessageHandler>(MessageType.Normal.ToString());
                    cb.Register(c => ModuleInjector.Inject<AcceptMessageHandler>()).Named<MessageHandler>(MessageType.Accept.ToString());
                    cb.Register(c => ModuleInjector.Inject<RejectMessageHandler>()).Named<MessageHandler>(MessageType.Reject.ToString());
                    cb.Register(c => ModuleInjector.Inject<EndMessageHandler>()).Named<MessageHandler>(MessageType.ConversationEnded.ToString());
                    cb.Register(c => ModuleInjector.Inject<EndMessageHandler>()).Named<MessageHandler>(MessageType.ConversationEnded.ToString());
                    //cb.Register(c => ModuleInjector.Inject<MySqlHelper>()).Named<MessageHandler>("MySql");
                    //cb.Register(c => ModuleInjector.Inject<MySqlHelper>()).Named<MessageHandler>("MySql");
                    //cb.Register(c => ModuleInjector.Inject<MySqlHelper>()).Named<MessageHandler>("MySql");
                    //cb.Register(c => ModuleInjector.Inject<MySqlHelper>()).Named<MessageHandler>("MySql");
                    IContainer tmpContainer = cb.Build();
                    if (_msgHelperContainer == null)
                        _msgHelperContainer = tmpContainer;
                    else
                        tmpContainer.Dispose();
                }
            }
            return _msgHelperContainer.ResolveNamed<MessageHandler>(messageType.ToString());
        }

        public virtual MessageCollectionData HandleMessage(MessageData message, int sequence)
        {
            /**
             *  1、保存消息 
             *  2、后续处理
             *  3、获得返回值 
             */
            //DoPreHandleMessage(message, sequence);
            DoHandleMessage(message, sequence);
            //DoAfterHandleMessage(message, sequence);
            MessageCollectionData result = GetMessage(sequence, message);
            return result;
        }

        protected virtual void DoHandleMessage(MessageData message, int sequence)
        {
            int insertedSequence = SaveMessage(message);
        }



        protected virtual int SaveMessage(MessageData msg)
        {
            return 0;
            //return MessageDA.Instance.SaveMessage(msg);
        }

        protected virtual MessageCollectionData GetMessage(int sequence, MessageData msg)
        {
            return MessageDA.Instance.GetMessage(sequence, msg.UserFrom);
        }

        //protected virtual void DoPreHandleMessage(MessageData message, int sequence) { }
        //protected virtual void DoAfterHandleMessage(MessageData message, int sequence) { }



        public static MessageData PreProcess(MessageCollectionData messageCollection)
        {
            MessageData msg;
            if (messageCollection.MessageList != null || messageCollection.MessageList.Count > 0)
            {
                msg = messageCollection.MessageList[0];
                msg.UserFrom = messageCollection.UserFrom;
            }
            else
            {
                msg = BuildHeartbeatMessage(messageCollection);
            }
            return msg;
        }
        private static MessageData BuildHeartbeatMessage(MessageCollectionData messageCollection)
        {
            return new MessageData()
            {
                UserFrom = messageCollection.UserFrom,
                MessageType = MessageType.HeartBeat
            };
        }
    }
}
