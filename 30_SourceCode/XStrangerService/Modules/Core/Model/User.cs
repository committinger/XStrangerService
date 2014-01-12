using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Committinger.XStrangerServic.Core
{
    public class User
    {
        private const int timedOutSec = 600;
        private const double timedOutCheckIntervalSec = 30;

        private static object _lockObj = new object();
        private static Dictionary<string, User> _pool;
        private static Timer _timedOutCheckTimer;
        public void Initialize()
        {
            _pool = new Dictionary<string, User>();
            _timedOutCheckTimer = new Timer(timedOutCheckIntervalSec * 1000);
            _timedOutCheckTimer.AutoReset = true;
            _timedOutCheckTimer.Elapsed += _timedOutCheckTimer_Elapsed;
        }

        void _timedOutCheckTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (_lockObj)
            {
                Array.ForEach(Pool.Keys.ToArray(), t =>
                {
                    if (Pool[t].TimedOut())
                        Pool.Remove(t);
                });
            }
        }

        private DateTime _lastActiveTime;
        private string _key;
        private Circle _circle;


        private User(string key, Circle circle)
        {
            _lastActiveTime = DateTime.Now;
            _key = key;
            _circle = circle;
        }

        public static string Register(Circle circle)
        {
            string key = new Guid().ToString("N");
            User user = new User(key, circle);
            Pool.Add(key, user);
            return key;
        }

        public static Dictionary<string, User> Pool { get { return _pool; } }

        public void Active()
        {
            _lastActiveTime = DateTime.Now;
        }

        private bool TimedOut()
        {
            return DateTime.Now.Subtract(_lastActiveTime).TotalSeconds > timedOutSec;
        }

        public void ChangeCircle(Circle circle)
        {
            this._circle = circle;
        }
    }
}
