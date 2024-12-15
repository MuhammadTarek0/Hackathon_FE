
    using System.Collections.Generic;
    using UnityEngine;
    
    public abstract class GameEventT<T> : ScriptableObject
    {
        private List<GameEventListenerT<T>> listeners = new List<GameEventListenerT<T>>();

        public void RegisterListener(GameEventListenerT<T> listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListenerT<T> listener)
        {
            listeners.Remove(listener);
        }
        
        [ContextMenu("Raise Event")]
        public void Raise(T value)
        {
            Debug.Log($"{this.name}: Raised");
            foreach (var listener in listeners)
            {
                if(!listener) continue;
                listener.OnGameEventRaised(value);
            }
        }
    }
