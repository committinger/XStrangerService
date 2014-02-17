using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Committinger.XStrangerService.ServiceInterface.DataContracts
{
    [DataContract(Namespace = ServiceContext.DATACONTRACT_NAMESPACE)]
    public class ConversationControlData : MessageData
    {
        [DataMember(Name = "max_time")]
        public int MaxPeriodSec { get; set; }
        [DataMember(Name = "min_time")]
        public int MinPeriodSec { get; set; }
    }
}
