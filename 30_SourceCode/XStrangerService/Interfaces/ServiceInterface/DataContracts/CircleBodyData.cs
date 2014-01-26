using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Committinger.XStrangerService.ServiceInterface.DataContracts
{
    [DataContract(Namespace = ServiceContext.DATACONTRACT_NAMESPACE)]
    public class CircleBodyData
    {
        public CircleBodyData()
        {
            Circle = new CircleData();
            UserName = string.Empty;
        }
        [DataMember(Name = "circle")]
        public CircleData Circle { get; set; }

        [DataMember(Name = "user_name")]
        public string UserName { get; set; }
    }
}
