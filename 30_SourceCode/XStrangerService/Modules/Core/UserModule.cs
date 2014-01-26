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
            //_pool.Add("AAA", new User() { Name = "AAA", Available = true });
            //_pool.Add("BBB", new User() { Name = "BBB", Available = true });
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

        public virtual User GetOrRegisterUserByName(string user_name)
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

        public virtual User GetUserByName(string user_name)
        {
            if (!string.IsNullOrEmpty(user_name) && Pool.ContainsKey(user_name))
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
            return null;

        }

        public virtual User GetRandomUser(string exclude)
        {
            List<User> users = Pool.Values.Where(t => (t.Available && !string.Equals(exclude, t.Name))).ToList();
            if (users.Count <= 0)
                return null;
            User user = users[new Random(DateTime.Now.Millisecond).Next(users.Count())];
            if (user.Available)
                return user;
            return null;
        }


        public virtual void DeleteUserByName(string user_name)
        {
            User u = UserModule.Instance.GetUserByName(user_name);
            if (u == null)
                return;
            Conversation c = ConversationModule.Instance.GetConversation(u);
            if (c != null)
                ConversationModule.Instance.RemoveConveration(c);
            UserModule.Instance.Remove(u);
        }

        public virtual void Remove(User u)
        {
            u.Available = false;
            if (Pool.ContainsKey(u.Name))
                Pool.Remove(u.Name);
        }
    }
}
