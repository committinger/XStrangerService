using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Committinger.XStrangerServic.Core
{
    public class MessageType
    {
        public const int HeartBeat = -1;
        public const int Normal = 0;
        public const int Invite = 1;
        public const int BeRejected = 2;
        public const int Reject = 3;
        public const int Accept = 4;
        public const int ConversationEnded = 5;
        public const int ConversationBreak = 6;
        public const int ConversationContinue = 7;
    }
}
