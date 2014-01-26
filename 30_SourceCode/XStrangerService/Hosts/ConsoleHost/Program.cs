using Committinger.XStrangerServic.Core;
using Committinger.XStrangerServic.ServiceImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using XSSFramework.Serialize;

namespace Committinger.XStrangerService.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            WebServiceHost host = new WebServiceHost(typeof(StrangerService));
            host.Open();
            Console.WriteLine("service open");
            Console.WriteLine("输入exit结束服务.");
            string cmd = string.Empty;
            char[] splitter = new char[] { ' ' };
            while (!"exit".Equals(cmd))
            {
                cmd = Console.ReadLine().Trim();
                Console.WriteLine(">" + cmd + ":");
                try
                {
                    string[] cmds = cmd.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                    switch (cmds[0])
                    {
                        case "showuser":
                            showUser(cmds);
                            break;
                        case "showconversation":
                            showConversation(cmds);
                            break;
                    }
                }
                catch
                {
                    error();
                }
            }
            host.Close();
        }

        private static void error()
        {
            Console.WriteLine("输入错误");
        }

        private static void showConversation(string[] cmds)
        {
            string[] keys = ConversationModule.Pool.Keys.ToArray();
            Console.WriteLine(string.Join("\t", keys) + "\t[count:" + keys.Length + "]");
            if (cmds.Length > 1)
                for (int i = 1; i < cmds.Length; i++)
                {
                    Array.ForEach(keys, t => { if (t != null && t.StartsWith(cmds[i]) && ConversationModule.Pool.ContainsKey(t))Console.WriteLine(XmlUtils.SerializeData(ConversationModule.Pool[t])); });
                }
        }

        private static void showUser(string[] cmds)
        {
            string[] keys = UserModule.Pool.Keys.ToArray();
            Console.WriteLine(string.Join("\t", keys) + "\t[count:" + keys.Length + "]");
            if (cmds.Length > 1)
                for (int i = 1; i < cmds.Length; i++)
                {
                    Array.ForEach(keys, t => { if (t != null && t.StartsWith(cmds[i]) && UserModule.Pool.ContainsKey(t))Console.WriteLine(XmlUtils.SerializeData(UserModule.Pool[t])); });
                }
        }
    }
}
