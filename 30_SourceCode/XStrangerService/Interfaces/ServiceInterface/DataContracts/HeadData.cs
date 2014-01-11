using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Committinger.XStrangerService.ServiceInterface.DataContracts
{
    [DataContract(Namespace = ServiceContext.DATACONTRACT_NAMESPACE)]
    public class HeadData
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        [DataMember(Name = "resp_timestamp")]
        public string TimeStamp { get; set; }
        /// <summary>
        /// 结果代码
        /// </summary>
        [DataMember(Name = "resp_code")]
        public string Code { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [DataMember(Name = "resp_desc")]
        public string Description { get; set; }
    }
}
