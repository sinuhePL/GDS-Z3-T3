using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDS3
{
    public class GameEventListener : MonoBehaviour, IListenable
    {
        public GameEvent[] _events;
        public UnityEvent<GameEvent> _response;

        private void OnEnable()
        {
            foreach (GameEvent _event in _events)
            {
                _event.RegisterListener(this);
            }
        }

        private void OnDisable()
        {
            foreach (GameEvent _event in _events)
            {
                _event.UnregisterListener(this);
            }
        }

        public void OnEventRaised(GameEvent _event)
        {
            _response.Invoke(_event);
        }
    }
}
