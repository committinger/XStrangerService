using Autofac.Extras.DynamicProxy2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XSSFramework.AOP;
using XSSFramework.Log;

namespace Committinger.XStrangerServic.Core
{
    [Intercept(typeof(Logger))]
    public class ConversationModule
    {
        public const int ConstantTimedOutSec = 120;
        static ConversationModule()
        {
            _pool = new Dictionary<string, Conversation>();
        }

        public static ConversationModule Instance { get { return ModuleInjector.Inject<ConversationModule>(); } }

        private static object _lockObj = new object();
        private static Dictionary<string, Conversation> _pool;
        public static Dictionary<string, Conversation> Pool { get { return _pool; } }

        public virtual Conversation StartNewConversation(User originator, User recipient)
        {
            if (!Pool.ContainsKey(originator.Name + recipient.Name))
                lock (_lockObj)
                {
                    Conversation c = new Conversation(originator, recipient, ConstantTimedOutSec);

                    if (!Pool.ContainsKey(originator.Name + recipient.Name))
                    {
                        Pool.Add(originator.Name, c);
                        Pool.Add(recipient.Name, c);
                        c.BreakAction = MessageModule.CreateBreakMessageAction;
                    }
                }
            MessageModule.Instance.CreateInviteMessage(Pool[originator.Name + recipient.Name]);
            return Pool[originator.Name + recipient.Name];
        }

        public virtual Conversation GetConversation(User user)
        {
            string key = Array.Find(Pool.Keys.ToArray(), t => string.Equals(t, user.Name));
            if (Pool.ContainsKey(key))
            {
                return Pool[key];
            }
            return null;
        }

        public virtual void RemoveConveration(Conversation c)
        {
            Pool.Remove(c.Originator.Name);
            Pool.Remove(c.Recipient.Name);
            c.Originator.Available = true;
            c.Recipient.Available = true;
        }

        public virtual void StopConveration(Conversation c)
        {
            c.OriginatorOpen = false;
            c.RecipientOpen = false;
            c.BreakAction = null;
        }

        public virtual void ContinueConveration(Conversation c, User user)
        {
            if (string.Equals(user.Name, c.Originator.Name))
                c.OriginatorOpen = true;
            else
                c.RecipientOpen = true;
        }
    }
}
