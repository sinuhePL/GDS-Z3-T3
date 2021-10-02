using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDS3
{
    public class GameManager : MonoBehaviour
    {
        public GameEvent _event;
        public GameEvent _event2;
        public UnityEvent _startScene;

        private void Awake()
        {
            Debug.Log(SoundSystem.Instance._isOn.Value);
            Debug.Log(MusicSystem.Instance._isOn.Value);
        }

        private void Start()
        {
            if (_startScene != null)
            {
                _startScene.Invoke();
            }
        }
    }
}
