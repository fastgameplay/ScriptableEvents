using System.Diagnostics;
using UnityEngine;
using System;

namespace ScriptableEvents
{
    #region  ScriptableEvent
    [CreateAssetMenu(fileName = "Void Event", menuName = "Events/CSharp/Void")]
    public class ScriptableEvent : ScriptableEventBase<Action>
    {
        public void Invoke()
        {
            eventHandler?.Invoke();
            InvokeLog();
        }
    }
    public abstract class ScriptableEvent<T> : ScriptableEventBase<Action<T>>
    {
        public void Invoke(T value)
        {
            eventHandler?.Invoke(value);
            InvokeLog(value);
        }
    }

    public abstract class ScriptableEvent<T1, T2> : ScriptableEventBase<Action<T1, T2>>
    {
        public void Invoke(T1 a, T2 b)
        {
            eventHandler?.Invoke(a, b);
            InvokeLog(a, b);
        }
    }
    #endregion
    #region Base Class
    public abstract class ScriptableEventBase<TDelegate> : ScriptableObject where TDelegate : Delegate
    {
        public TDelegate Event
        {
            get { return eventHandler; }
            set { eventHandler = value; }
        }
        protected TDelegate eventHandler;

        public void AddListener(TDelegate listener)
        {
            eventHandler = Delegate.Combine(eventHandler, listener) as TDelegate;
        }

        public void RemoveListener(TDelegate listener)
        {
            eventHandler = Delegate.Remove(eventHandler, listener) as TDelegate;
        }

        [Conditional("LOG_SE")]
        protected void InvokeLog(params object[] args)
        {
            var argList = (args?.Length > 0)
                ? ": " + string.Join(", ", args)
                : "";
            UnityEngine.Debug.Log($"[{DateTime.Now.ToString("HH:mm:ss.fff")}] [LOG_SE({Time.time})] Invoking {GetType().Name} (“{name}”){argList}");
        }
    }
    #endregion
}