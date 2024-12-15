
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "GameEvent")]
    public class GameEvent : ScriptableObject
    {
        private List<GameEventListener> listeners = new List<GameEventListener>();

        public void RegisterListener(GameEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            listeners.Remove(listener);
        }
        
        [ContextMenu("Raise Event")]
        public void Raise()
        {
            Debug.Log($"{this.name}: Raised");
            foreach (var listener in listeners)
            {
                if(!listener) continue;
                listener.OnGameEventRaised();
            }
        }
    }
