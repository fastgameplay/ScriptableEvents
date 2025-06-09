namespace ScriptableEvents
{
    using UnityEngine;
    using System;
    public abstract class SO_BaseEventClass<TDelegate> : ScriptableObject where TDelegate : Delegate
    {
        public TDelegate Event
        {
            get { return eventHandler; }
            set { eventHandler = value; }
        }
        protected TDelegate eventHandler;

        public void AddListener(TDelegate listener)
            => eventHandler = Delegate.Combine(eventHandler, listener) as TDelegate;

        public void RemoveListener(TDelegate listener)
            => eventHandler = Delegate.Remove(eventHandler, listener) as TDelegate;
    }

    public abstract class SO_BaseEvent<T> : SO_BaseEventClass<Action<T>>
    {
        public void Invoke(T value) => base.eventHandler?.Invoke(value);
    }

    public abstract class SO_BaseEvent<T1, T2> : SO_BaseEventClass<Action<T1, T2>>
    {
        public void Invoke(T1 a, T2 b) => base.eventHandler?.Invoke(a, b);
    }
}