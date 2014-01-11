using Committinger.XStrangerService.ServiceInterface.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Committinger.XStrangerService.ServiceInterface.ServiceContracts
{
    [ServiceContract(Namespace = ServiceContext.SERVICECONTRACT_NAMESPACE)]
    public interface IStrangerService
    {
        [OperationContract]
        [WebGet(UriTemplate = "test?req_timestamp={req_timestamp}&req_client_platform={req_client_platform}&req_client_version={req_client_version}", ResponseFormat = WebMessageFormat.Json)]
        ResultData Test(string req_timestamp, string req_client_platform, string req_client_version);
    }
}
