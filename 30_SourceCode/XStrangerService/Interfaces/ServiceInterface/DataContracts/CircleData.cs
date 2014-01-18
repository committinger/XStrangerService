using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Committinger.XStrangerService.ServiceInterface.DataContracts
{
    [DataContract(Namespace = ServiceContext.DATACONTRACT_NAMESPACE)]
    public class CircleData
    {
        [DataMember(Name = "circle_name")]
        public string Name { get; set; }

        [DataMember(Name = "circle_icon")]
        public string IconUrl { get; set; }

        [DataMember(Name = "circle_desc")]
        public string Description { get; set; }

        [DataMember(Name = "circle_images")]
        public string[] ImageUrls { get; set; }
    }
}
