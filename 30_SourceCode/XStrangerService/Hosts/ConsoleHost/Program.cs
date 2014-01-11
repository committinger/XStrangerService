using Committinger.XStrangerServic.ServiceImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            WebServiceHost host = new WebServiceHost(typeof(StrangerService));
            host.Open();
            Console.WriteLine("service open");
            Console.WriteLine("输入#回车结束服务.");
            while (!"#".Equals(Console.ReadLine())) ;
            host.Close();
        }
    }
}
