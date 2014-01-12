using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Committinger.XStrangerService.ServiceInterface.DataContracts
{
    [DataContract(Namespace = ServiceContext.DATACONTRACT_NAMESPACE)]
    public class MessageData
    {
        [DataMember(Name = "message_to_user")]
        public string UserTo { get; set; }

        [DataMember(Name = "message_type")]
        public int MessageType { get; set; }

        [DataMember(Name = "message_content")]
        public string Content { get; set; }

        [DataMember(Name = "message_time")]
        public string Time { get; set; }
    }
}
