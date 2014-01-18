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

        public ResultData()
        {
            Head = new HeadData("0", "成功");
        }

        public ResultData(string code, string description)
        {
            Head = new HeadData(code, description);
        }

        public void SetError(string code, string error)
        {
            Head.Code = code;
            Head.Description = error;
        }

        [DataMember(Name = "head")]
        public HeadData Head { get; set; }
    }
}
