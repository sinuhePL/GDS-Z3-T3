using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDS3
{
    public class MonologTriggerController : MonoBehaviour
    {
        [SerializeField] private string _monologText;
        [SerializeField] private UnityEvent _relatedEvent;
        private bool _wasDisplayed;

        private void Start()
        {
            _wasDisplayed = false;
        }

        public string GetMonolog()
        {
            if (!_wasDisplayed)
            {
                _wasDisplayed = true;
                _relatedEvent.Invoke();
                return _monologText;
            }
            else
            {
                return "";
            }
        }

        public bool CheckIfVisited()
        {
            return _wasDisplayed;
        }
    }
}