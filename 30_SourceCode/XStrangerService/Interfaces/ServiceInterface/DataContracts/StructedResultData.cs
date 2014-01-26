using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Committinger.XStrangerService.ServiceInterface.DataContracts
{
    [DataContract(Namespace = ServiceContext.DATACONTRACT_NAMESPACE)]
    public class StructedResultData<T> : ResultData
    {
        public StructedResultData() : base() { }
        public StructedResultData(string code, string description) : base(code, description) { }

        [DataMember(Name = "body")]
        public T Body { get; set; }

    }
}
