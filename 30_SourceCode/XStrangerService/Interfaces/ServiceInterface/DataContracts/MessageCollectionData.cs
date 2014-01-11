using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Committinger.XStrangerService.ServiceInterface.DataContracts
{
    [DataContract(Namespace = ServiceContext.DATACONTRACT_NAMESPACE)]
    public class MessageCollectionData
    {
        [DataMember(Name = "sequence")]
        public int Sequence { get; set; }

        [DataMember(Name = "message")]
        public List<MessageData> MessageList { get; set; }
    }
}
