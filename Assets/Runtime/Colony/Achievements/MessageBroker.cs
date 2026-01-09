using System;
using System.Collections.Generic;
using Runtime.Colony.Achievements.Events;

namespace Runtime.Colony.Achievements
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

            foreach (var newEvent in list)
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