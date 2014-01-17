using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Committinger.XStrangerServic.Core
{
    public class Message
    {
        private static object _lockObj = new object();
        private static long _seed = DateTime.Now.Ticks;
        public static long NextSequence
        {
            get
            {
                long tmp;
                lock (_lockObj)
                {
                    tmp = _seed++;
                }
                return tmp;
            }
        }

        public Message(int type, string content, string from, string to)
        {
            this.Sequence = NextSequence;
            this.Time = DateTime.Now;
            this.Type = type;
            this.Content = content;
            this.FromKey = from;
            this.ToKey = to;
        }

        public DateTime Time { get; set; }
        public long Sequence { get; set; }
        public int Type { get; set; }
        public string Content { get; set; }
        public string FromKey { get; set; }
        public string ToKey { get; set; }
    }
}
