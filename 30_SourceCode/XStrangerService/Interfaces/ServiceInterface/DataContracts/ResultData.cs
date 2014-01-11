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
    public class ResultData
    {
        [DataMember(Name = "head")]
        public HeadData Head { get; set; }
    }
}
