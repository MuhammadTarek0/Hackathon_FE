
    using UnityEngine;
    using UnityEngine.Events;

    public class GameEventListener : MonoBehaviour
    {
        [SerializeField]
        private GameEvent gameEvent;
        
        [SerializeField]
        protected UnityEvent onGameEventRaised;

        private void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            gameEvent.UnregisterListener(this);
        }

        public void OnGameEventRaised()
        {
            onGameEventRaised.Invoke();
        }
    }
