using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class MonologTriggerController : MonoBehaviour
    {
        [SerializeField] private string _monologText;
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
                return _monologText;
            }
            else
            {
                return "";
            }
        }
    }
}