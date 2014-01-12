using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Committinger.XStrangerServic.Core
{
    public class Conversation
    {
        public Conversation(User starter, User acceptor)
        {
            this._starter = starter;
            this._acceptor = acceptor;
            this._timer.Interval = 12000;
            this._timer.Elapsed += _timer_Elapsed;
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this._timer.Elapsed -= _timer_Elapsed;
            //CreateMessage
        }

        private User _starter { get; set; }
        private User _acceptor { get; set; }
        private Timer _timer { get; set; }

        public List<Message> _messageList { get; set; }

    }
}
