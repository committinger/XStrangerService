﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Committinger.XStrangerServic.Core
{
    public class Conversation
    {
        public Conversation(User originator, User recipient, int timedOutSec)
        {
            this.Originator = originator;
            this.Recipient = recipient;
            this.Timer = new Timer(timedOutSec * 1000);
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Timer.Elapsed -= _timer_Elapsed;
            if (this.BreakAction != null)
                this.BreakAction.Invoke(this);
        }

        public Action<Conversation> BreakAction;

        public string Key { get { return Originator.Name + Recipient.Name; } }

        public User Originator { get; set; }
        public User Recipient { get; set; }
        public Timer Timer { get; set; }
        public bool OriginatorOpen { get; set; }
        public bool RecipientOpen { get; set; }
        public bool ConversationOpen { get { return OriginatorOpen && RecipientOpen; } }


        internal void Initial()
        {
            Originator.Available = false;
            Recipient.Available = false;
            OriginatorOpen = true;
            RecipientOpen = false;
            this.Timer.Elapsed += _timer_Elapsed;
        }
    }
}
