using Autofac;
using Committinger.XStrangerServic.Core.DA;
using Committinger.XStrangerService.ServiceInterface.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSSFramework.AOP;

namespace Committinger.XStrangerServic.Core.MessageHandler
{
    public abstract class BaseMessageHandler
    {
        public const string ConstantDateTimeFormat = "yyyy-MM-dd HH:mm:ss +0800";

        private static IContainer _msgHelperContainer;
        private static object _lockObj = new object();

        public static BaseMessageHandler CreateHandler(int messageType)
        {
            if (_msgHelperContainer == null)
            {
                lock (_lockObj)
                {
                    ContainerBuilder cb = new ContainerBuilder();
                    cb.Register(c => ModuleInjector.Inject<NormalMessageHandler>()).Named<BaseMessageHandler>(MessageType.Normal.ToString());
                    cb.Register(c => ModuleInjector.Inject<AcceptMessageHandler>()).Named<BaseMessageHandler>(MessageType.Accept.ToString());
                    cb.Register(c => ModuleInjector.Inject<RejectMessageHandler>()).Named<BaseMessageHandler>(MessageType.Reject.ToString());
                    cb.Register(c => ModuleInjector.Inject<EndMessageHandler>()).Named<BaseMessageHandler>(MessageType.ConversationEnded.ToString());
                    //cb.Register(c => ModuleInjector.Inject<EndMessageHandler>()).Named<BaseMessageHandler>(MessageType.ConversationEnded.ToString());
                    cb.Register(c => ModuleInjector.Inject<HeartBeatMessageHandler>()).Named<BaseMessageHandler>(MessageType.HeartBeat.ToString());
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
            return _msgHelperContainer.ResolveNamed<BaseMessageHandler>(messageType.ToString());
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



        public static List<MessageData> PreProcess(MessageCollectionData messageCollection)
        {
            List<MessageData> msgList = new List<MessageData>();

            if (messageCollection.MessageList != null && messageCollection.MessageList.Count > 0)
            {
                msgList = messageCollection.MessageList;
                if (msgList != null)
                    msgList.ForEach(t => t.UserFrom = messageCollection.UserFrom);
            }
            else
            {
                msgList.Add(BuildHeartbeatMessage(messageCollection));
            }

            return msgList;
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
