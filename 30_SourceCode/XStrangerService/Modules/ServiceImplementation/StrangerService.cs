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
        public StructedResultData<CircleBodyData> RetrieveCircle(string req_timestamp, string req_client_platform, string req_client_version, string circle_key, string user_name)
        {
            StringBuilder sb = new StringBuilder("查询圈子");
            sb.AppendLine("req_timestamp：" + req_timestamp);
            sb.AppendLine("req_client_platform：" + req_client_platform);
            sb.AppendLine("req_client_version：" + req_client_version);
            sb.AppendLine("circle_key：" + circle_key);
            sb.AppendLine("user_name：" + user_name);
            LogUtils.Debug(sb);
            /**
             *  1、注册用户 
             *  2、取出圈子
             *  3、设置该用户在圈子中* 
             *  
             */

            try
            {
                if (string.IsNullOrEmpty(circle_key))
                {
                    User deleteU = UserModule.Instance.GetOrRegisterUserByName(user_name);
                    Conversation deleteC = ConversationModule.Instance.GetConversation(deleteU);
                    if (deleteC != null) ConversationModule.Instance.RemoveConveration(deleteC);
                    return new StructedResultData<CircleBodyData>("0", "离开社区成功")
                    {
                        Body = new CircleBodyData()
                    };
                }

                User user = UserModule.Instance.GetOrRegisterUserByName(user_name);
                Conversation c = ConversationModule.Instance.GetConversation(user);
                if (c != null) ConversationModule.Instance.RemoveConveration(c);
                Circle circle = CircleDA.Instance.GetCircleByKey(circle_key);
                if (circle == null)
                {
                    LogUtils.AsyncDebug(new StringBuilder("未能根据关键字找到对应的圈子：").Append(circle_key));
                    return new StructedResultData<CircleBodyData>("-1", "未能根据关键字找到对应的圈子")
                    {
                        Body = new CircleBodyData()
                    };
                }

                //user.In = circle;
                //user.Available = true;

                StructedResultData<CircleBodyData> result = new StructedResultData<CircleBodyData>();
                result.Body = new CircleBodyData()
                {
                    Circle = circle.Body,
                    UserName = user.Name
                };

                return result;
            }
            catch (Exception ex)
            {
                LogUtils.Error("发生异常", ex);
            }
            return new StructedResultData<CircleBodyData>("-1", "发生异常")
            {
                Body = new CircleBodyData()
            };
        }

        public StructedResultData<MessageCollectionData> SyncMessage(string req_timestamp, string req_client_platform, string req_client_version, MessageCollectionData messageCollection)
        {
            try
            {
                MessageCollectionData result = MessageModule.Instance.Process(messageCollection);
                return new StructedResultData<MessageCollectionData>()
                {
                    Body = result
                };
            }
            catch (Exception ex)
            {
                LogUtils.Error("发生异常", ex);
            }
            return new StructedResultData<MessageCollectionData>("-1", "发生异常");
        }
    }
}
