using Committinger.XStrangerServic.Core;
using Committinger.XStrangerServic.Core.DA;
using Committinger.XStrangerService.ServiceInterface.DataContracts;
using Committinger.XStrangerService.ServiceInterface.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSSFramework.Log;

namespace Committinger.XStrangerServic.ServiceImplementation
{
    public class StrangerService : IStrangerService
    {

        public ResultData Test(string req_timestamp, string req_client_platform, string req_client_version)
        {
            return new ResultData()
            {
                Head = new HeadData()
                {
                    Code = "0",
                    Description = "Success. hello, " + req_timestamp + "," + req_client_platform + "," + req_client_version,
                    TimeStamp = DateTime.Now.Ticks.ToString()
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req_timestamp"></param>
        /// <param name="req_client_platform"></param>
        /// <param name="req_client_version"></param>
        /// <param name="circle_key"></param>
        /// <param name="user_name"></param>
        /// <returns></returns>
        public StructedResultData<CircleData> RetrieveCircle(string req_timestamp, string req_client_platform, string req_client_version, string circle_key, string user_name)
        {
            /**
             *  1、注册用户 
             *  2、取出圈子
             *  3、设置该用户在圈子中* 
             *  
             */
            User user = UserModule.Instance.GetUserByName(user_name);
            Circle circle = CircleDA.Instance.GetCircleByKey(circle_key);

            if (circle == null)
            {
                LogUtils.AsyncDebug(new StringBuilder("未能根据关键字找到对应的圈子：").Append(circle_key));
                return new StructedResultData<CircleData>("-1", "未能根据关键字找到对应的圈子");
            }

            user.In = circle;
            user.Available = true;

            StructedResultData<CircleData> result = new StructedResultData<CircleData>();
            result.Body = circle.Body;
            result.UserName = user.Name;
            return result;
        }

        public StructedResultData<MessageCollectionData> SyncMessage(string req_timestamp, string req_client_platform, string req_client_version, MessageCollectionData messageCollection)
        {
            StructedResultData<MessageCollectionData> result = new StructedResultData<MessageCollectionData>()
            {
                Head = new HeadData()
                {
                    Code = "0",
                    Description = "Success. hello, " + req_timestamp + "," + req_client_platform + "," + req_client_version,
                    TimeStamp = DateTime.Now.Ticks.ToString()
                },
                Body = new MessageCollectionData()
                {
                    Sequence = messageCollection.Sequence + 10000,
                    MessageList = new List<MessageData>()
                }
            };
            messageCollection.MessageList.Add(new MessageData()
            {
                Content = "你好，我们正在聊天BASE",
                MessageType = 0,
                Time = DateTime.Now.Subtract(new TimeSpan(0, 0, 5)).ToString("yyyy-MM-dd HH:mm:ss +0800"),
                UserTo = "someoneelse"
            });
            switch (messageCollection.Sequence % 10)
            {
                case 1:
                    messageCollection.MessageList.Insert(0, new MessageData()
                    {
                        Content = "",
                        MessageType = 1,
                        Time = DateTime.Now.Subtract(new TimeSpan(0, 0, 30)).ToString("yyyy-MM-dd HH:mm:ss +0800"),
                        UserTo = "someoneelse"
                    });
                    break;
                case 2:
                    messageCollection.MessageList.Add(new MessageData()
                    {
                        Content = "",
                        MessageType = 2,
                        Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss +0800"),
                        UserTo = "someoneelse"
                    });
                    break;
                case 5:
                    messageCollection.MessageList.Add(new MessageData()
                    {
                        Content = "",
                        MessageType = 5,
                        Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss +0800"),
                        UserTo = "someoneelse"
                    });
                    break;
                default:
                    messageCollection.MessageList.Add(new MessageData()
                    {
                        Content = "你好，我们正在聊天CASE0",
                        MessageType = 0,
                        Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss +0800"),
                        UserTo = "someoneelse"
                    });
                    break;
            }

            return result;
        }
    }
}
