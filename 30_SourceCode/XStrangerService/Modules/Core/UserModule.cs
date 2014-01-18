using Autofac.Extras.DynamicProxy2;
using Committinger.XStrangerServic.Core.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using XSSFramework.AOP;
using XSSFramework.Log;

namespace Committinger.XStrangerServic.Core
{
    [Intercept(typeof(Logger))]
    public class UserModule
    {
        static UserModule()
        {
            _pool = new Dictionary<string, User>();
            _timedOutCheckTimer = new Timer(timedOutCheckIntervalSec * 1000);
            _timedOutCheckTimer.AutoReset = true;
            _timedOutCheckTimer.Elapsed += _timedOutCheckTimer_Elapsed;
        }

        public static UserModule Instance { get { return ModuleInjector.Inject<UserModule>(); } }

        public const int timedOutSec = 600;
        private const double timedOutCheckIntervalSec = 30;

        private static Dictionary<string, User> _pool;
        public static Dictionary<string, User> Pool { get { return _pool; } }

        private static object _lockObj = new object();
        private static Timer _timedOutCheckTimer;

        private static void _timedOutCheckTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (_lockObj)
            {
                Array.ForEach(Pool.Keys.ToArray(), t =>
                {
                    if (Pool[t].TimedOut(timedOutSec))
                        Pool.Remove(t);
                });
            }
        }

        public virtual User Register()
        {
            User user = new User()
            {
                Available = true,
                Name = Guid.NewGuid().ToString("N")
            };
            Pool.Add(user.Name, user);
            return user;
        }

        public virtual User GetUserByName(string user_name)
        {
            if (string.IsNullOrEmpty(user_name))
                return Instance.Register();

            if (Pool.ContainsKey(user_name))
            {
                try
                {
                    User user = Pool[user_name];
                    user.HeartBeat();
                    if (Pool.ContainsKey(user_name))
                        return user;
                }
                catch (Exception ex)
                {
                    LogUtils.Error(ex);
                }
            }
            return Instance.Register();

        }

        public virtual User GetRandomUser()
        {
            List<User> users = Pool.Values.Where(t => t.Available).ToList();
            User user = users[new Random(DateTime.Now.Millisecond).Next(users.Count())];
            if (user.Available)
                return user;
            return null;
        }

    }
}
