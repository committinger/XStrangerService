using Committinger.XStrangerService.ServiceInterface.DataContracts;
using Committinger.XStrangerService.ServiceInterface.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Committinger.XStrangerServic.ServiceImplementation
{
    public class StrangerService : IStrangerService
    {

        public ResultData Test(string req_timestamp, string req_client_platform, string req_client_version)
        {
            return new ResultData()
            {
                Head = new HeadData()
                {
                    Code = "0",
                    Description = "Success. hello, " + req_timestamp +","+ req_client_platform +","+ req_client_version,
                    TimeStamp = DateTime.Now.Ticks.ToString()
                }
            };
        }
    }
}
