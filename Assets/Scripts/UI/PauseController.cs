using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GDS3
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] private Text _myText;
        private bool _isGamePaused;

        private void Awake()
        {
            _isGamePaused = false;
            _myText.enabled = false;
        }

        public void OnPausePressed()
        {
            _isGamePaused = !_isGamePaused;
            _myText.enabled = _isGamePaused;
        }
    }
}
