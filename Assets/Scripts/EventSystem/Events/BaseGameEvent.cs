using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Events
{

    public abstract class BaseGameEvent<T> : ScriptableObject
    {

        private readonly List<IGameEventListener<T>> eventListeners = new List<IGameEventListener<T>>();

        public void Raise(T item)
        {
            for (int i = this.eventListeners.Count - 1; i >= 0; i--)
            {
                this.eventListeners[i].OnEventRaised(item);
            }
        }

        public void RegisterListener(IGameEventListener<T> listener)
        {
            if (!this.eventListeners.Contains(listener))
            {
                this.eventListeners.Add(listener);
            }
        }

        public void UnregisterListener(IGameEventListener<T> listener)
        {
            if (this.eventListeners.Contains(listener))
            {
                this.eventListeners.Remove(listener);
            }
        }

    }

}
