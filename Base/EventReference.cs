using System.Diagnostics;
using UnityEngine;
using System;

namespace ScriptableEvents
{

    [Serializable]
    public class EventReference : EventReferenceBase<Action, ScriptableEvent>
    {
        public static EventReference operator +(EventReference reference, Action listener)
        {
            reference.AddListener(listener);
            return reference;
        }
        public static EventReference operator -(EventReference reference, Action listener)
        {
            reference.RemoveListener(listener);
            return reference;
        }

        public void Invoke()
        {
            Handler?.Invoke();
            InvokeLog();
        }
    }


    [Serializable]
    public class EventReference<T> : EventReferenceBase<Action<T>, ScriptableEvent<T>>
    {
        public static EventReference<T> operator +(EventReference<T> reference, Action<T> listener)
        {
            reference.AddListener(listener);
            return reference;
        }
        public static EventReference<T> operator -(EventReference<T> reference, Action<T> listener)
        {
            reference.RemoveListener(listener);
            return reference;
        }

        public void Invoke(T value)
        {
            Handler?.Invoke(value);
            InvokeLog(value);
        }
    }

    [Serializable]
    public class EventReference<T1, T2> : EventReferenceBase<Action<T1, T2>, ScriptableEvent<T1, T2>>
    {
        public static EventReference<T1, T2> operator +(EventReference<T1, T2> reference, Action<T1, T2> listener)
        {
            reference.AddListener(listener);
            return reference;
        }
        public static EventReference<T1, T2> operator -(EventReference<T1, T2> reference, Action<T1, T2> listener)
        {
            reference.RemoveListener(listener);
            return reference;
        }

        public void Invoke(T1 a, T2 b)
        {
            Handler?.Invoke(a, b);
            InvokeLog(a, b);
        }
    }


    [Serializable]
    public abstract class EventReferenceBase<TDelegate, TEventAsset>
        where TDelegate : Delegate
        where TEventAsset : ScriptableEventBase<TDelegate>
    {
        [SerializeField] private EventType _type;
        [SerializeField] private TEventAsset _externalEvent;
        private TDelegate _internalEvent;

        public void AddListener(TDelegate listener)
        {
            if (_type == EventType.Internal)
                _internalEvent = Delegate.Combine(_internalEvent, listener) as TDelegate;
            else
                _externalEvent.AddListener(listener);
        }

        public void RemoveListener(TDelegate listener)
        {
            if (_type == EventType.Internal)
                _internalEvent = Delegate.Remove(_internalEvent, listener) as TDelegate;
            else
                _externalEvent.RemoveListener(listener);
        }

        protected TDelegate Handler =>
            _type == EventType.Internal
                ? _internalEvent
                : _externalEvent.Event;
                
        [Conditional("LOG_SE")]
        protected void InvokeLog(params object[] args)
        {
            var argList = (args?.Length > 0)
                ? ": " + string.Join(", ", args)
                : "";
            UnityEngine.Debug.Log($"[{DateTime.Now:HH:mm:ss.fff}] [LOG_ER({Time.time:F2})] " +
                      $"Invoking {GetType().Name} (“{_externalEvent?.name ?? "(internal)"}”){argList}");
        }
    }

    public enum EventType
    {
        Internal,
        External
    }
}