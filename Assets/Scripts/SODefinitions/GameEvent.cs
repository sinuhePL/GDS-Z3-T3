using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "myEvent", menuName = "Scriptable Objects/Game Event")]
    public class GameEvent : ScriptableObject
    {
        private List<IListenable> _listeners = new List<IListenable>();

        public void Raise()
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised(this);
            }
        }

        public void RegisterListener(IListenable listener)
        {
            _listeners.Add(listener);
        }

        public void UnregisterListener(IListenable listener)
        {
            _listeners.Remove(listener);
        }
    }
}
