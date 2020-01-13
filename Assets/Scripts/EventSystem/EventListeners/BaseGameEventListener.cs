using UnityEngine;
using UnityEngine.Events;


namespace Tactics.Events
{

    public abstract class BaseGameEventListener<T, E, UER> : MonoBehaviour,
      IGameEventListener<T> where E : BaseGameEvent<T> where UER : UnityEvent<T>
    {

        [SerializeField] public E gameEvent = null;
        public E GameEvent { get { return this.gameEvent; } set { this.gameEvent = value; } }

        [SerializeField] public UER unityEventResponse = null;

        private void OnEnable()
        {
            if (this.gameEvent == null)
            {
                return;
            }

            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if (this.gameEvent == null)
            {
                return;
            }

            GameEvent.UnregisterListener(this);
        }

        public void OnEventRaised(T item)
        {
            if (this.unityEventResponse != null)
            {
                this.unityEventResponse.Invoke(item);
            }
        }

    }

}
