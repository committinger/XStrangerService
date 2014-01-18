using Committinger.XStrangerService.ServiceInterface.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Committinger.XStrangerServic.Core
{
    public class Circle
    {
        public int RecId { get; set; }
        public string[] Keywords { get; set; }
        public CircleData Body { get; set; }
    }
}
