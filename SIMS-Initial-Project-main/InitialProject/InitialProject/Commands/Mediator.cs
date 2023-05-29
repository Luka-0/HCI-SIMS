using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Commands
{
    public class Mediator
    {
        private static readonly Mediator instance = new Mediator();
        private readonly Dictionary<string, Action<object>> subscribers = new Dictionary<string, Action<object>>();

        public static Mediator Instance => instance;

        public void Subscribe(string eventName, Action<object> action)
        {
            if (!subscribers.ContainsKey(eventName))
            {
                subscribers[eventName] = null;
            }
            subscribers[eventName] += action;
        }

        public void Unsubscribe(string eventName, Action<object> action)
        {
            if (subscribers.ContainsKey(eventName))
            {
                subscribers[eventName] -= action;
            }
        }

        public void Publish(string eventName, object args)
        {
            if (subscribers.ContainsKey(eventName) && subscribers[eventName] != null)
            {
                subscribers[eventName].Invoke(args);
            }
        }
    }
}
