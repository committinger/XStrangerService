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
        public User()
        {
            Available = true;
            _lastActiveTime = DateTime.Now;
        }

        private DateTime _lastActiveTime;

        public int RecId { get; set; }

        public bool Available { get; set; }

        public Circle In { get; set; }

        public string Name { get; set; }


        public bool TimedOut(int periodSec)
        {
            return DateTime.Now.Subtract(_lastActiveTime).TotalSeconds > periodSec;
        }

        public void HeartBeat()
        {
            _lastActiveTime = DateTime.Now;
        }
    }
}
