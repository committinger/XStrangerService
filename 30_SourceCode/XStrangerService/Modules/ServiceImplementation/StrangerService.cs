using Committinger.XStrangerService.ServiceInterface.DataContracts;
using Committinger.XStrangerService.ServiceInterface.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public StructedResultData<CircleData> RetrieveCircle(string req_timestamp, string req_client_platform, string req_client_version, string circle_key, string user_name)
        {
            StructedResultData<CircleData> result = new StructedResultData<CircleData>()
            {
                Head = new HeadData()
                {
                    Code = "0",
                    Description = "Success. hello, " + req_timestamp + "," + req_client_platform + "," + req_client_version,
                    TimeStamp = DateTime.Now.Ticks.ToString()
                },
                Body = new CircleData()
                {
                    Description = "上海轨道交通2号线是上海第二条地下铁路线路，于2000年6月11日开始运营。该线从青浦区徐泾东站，经过有中华第一街之称的南京路，穿越黄浦江，到达浦东新区陆家嘴、世纪公园、张江高科，并最终抵达浦东国际机场站，可以说是连接上海过去和未来的纽带。",
                    Name = "Shanghai subway line 2",
                    IconUrl = "http://www.csrpz.com/files/100448/1105/x_da653726.jpg",
                    ImageUrlList = new List<string>() { 
                        "http://www.csrpz.com/files/100448/1105/x_da653726.jpg",
                        "http://www.csrpz.com/files/100448/1105/x_da653726.jpg"
                    }
                },
                UserName = string.IsNullOrEmpty(user_name) ? new Guid().ToString("N") : user_name
            };
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
