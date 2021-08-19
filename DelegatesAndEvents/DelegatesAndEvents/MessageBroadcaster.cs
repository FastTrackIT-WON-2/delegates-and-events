using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAndEvents
{
    public class MessageBroadcaster
    {
        private event MessageBroadcast onMessageRecevied;

        public event MessageBroadcast OnMessageReceived
        {
            add { onMessageRecevied += value; }
            remove { onMessageRecevied -= value; }
        }

        public void Broadcast(string message)
        {
            onMessageRecevied?.Invoke(message);
        }
    }
}
