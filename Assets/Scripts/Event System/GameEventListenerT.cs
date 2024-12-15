
    using UnityEngine;
    using UnityEngine.Events;

    public abstract class GameEventListenerT<T> : MonoBehaviour
    {
        [SerializeField]
        private GameEventT<T> gameEvent;
        
        [SerializeField]
        protected UnityEvent<T> onGameEventRaised;

        private void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            gameEvent.UnregisterListener(this);
        }

        public void OnGameEventRaised(T value)
        {
            onGameEventRaised.Invoke(value);
        }
    }
