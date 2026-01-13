using System;
using System.Collections.Generic;
using Runtime.Colony.Achievements.Events;

namespace Runtime.Services
{
    public class MessageBroker
    {
        private static MessageBroker _instance;
        public static MessageBroker Instance => _instance ??= new MessageBroker();

        private readonly Dictionary<string, List<Action<GameEvent>>> _subscribers = new();

        public void Publish(GameEvent gameEvent)
        {
            if (!_subscribers.TryGetValue(gameEvent.Type, out var list))
            {
                return;
            }
            
            var callbacks = new List<Action<GameEvent>>(list);

            foreach (var newEvent in callbacks)
            {
                newEvent?.Invoke(gameEvent);
            }
        }
        
        public void Subscribe(string eventType, Action<GameEvent> callback)
        {
            if (!_subscribers.TryGetValue(eventType, out var list))
            {
                list = new List<Action<GameEvent>>();
                _subscribers[eventType] = list;
            }

            list.Add(callback);
        }

        public void Unsubscribe(string eventType, Action<GameEvent> callback)
        {
            if (_subscribers.TryGetValue(eventType, out var list))
            {
                list.Remove(callback);
            }
        }
    }
}