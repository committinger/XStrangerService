using Autofac;
using Committinger.XStrangerServic.Core.DA;
using Committinger.XStrangerService.ServiceInterface.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSSFramework.AOP;
using XSSFramework.Log;

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
                    cb.Register(c => ModuleInjector.Inject<ContinueMessageHandler>()).Named<BaseMessageHandler>(MessageType.ConversationContinue.ToString());
                    //cb.Register(c => ModuleInjector.Inject<HeartBeatMessageHandler>()).Named<BaseMessageHandler>(MessageType.HeartBeat.ToString());
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
            DoHandleMessage(message, sequence);
            MessageCollectionData result = GetMessage(sequence, message.UserFrom);
            return result;
        }

        protected virtual void DoHandleMessage(MessageData message, int sequence)
        {
            int insertedSequence = SaveMessage(message);
        }

        protected virtual int SaveMessage(MessageData msg)
        {
            return 0;
        }

        public static MessageCollectionData GetMessage(int sequence, string userFrom)
        {
            return MessageDA.Instance.GetMessage(sequence, userFrom);
        }
        public static List<MessageData> PreProcess(MessageCollectionData messageCollection)
        {
            List<MessageData> msgList = new List<MessageData>();

            if (messageCollection.MessageList != null && messageCollection.MessageList.Count > 0)
            {
                msgList = messageCollection.MessageList;
                if (msgList != null)
                    msgList.ForEach(t => t.UserFrom = messageCollection.UserFrom);


                if (msgList != null)
                    msgList.ForEach(t =>
                    {
                        if (t != null)
                        {
                            LogUtils.Info("收到的消息：" + t.Content);
                        }
                    });
            }
            return msgList;
        }
    }
}
