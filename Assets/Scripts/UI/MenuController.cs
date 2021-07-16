using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace GDS3
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _settingsPrefab;
        [SerializeField] private GameObject _menuPanel;
        [SerializeField] private UnityEvent _pausePressedEvent;
        private bool _isMenuActive;

        private void Awake()
        {
            _menuPanel.SetActive(false);
            _isMenuActive = false;
        }

        public void MenuPressed()
        {
            if(_isMenuActive)
            {
                _menuPanel.SetActive(false);
                _isMenuActive = false;
            }
            else
            {
                _menuPanel.SetActive(true);
                _isMenuActive = true;
            }
        }

        public void BackPressed()
        {
            _menuPanel.SetActive(false);
            _isMenuActive = false;
            _pausePressedEvent.Invoke();
        }

        public void SettingsPressed()
        {
            GameObject settingsPanel = Instantiate(_settingsPrefab, transform);
            settingsPanel.transform.localPosition = new Vector3(0, 0, 0);
        }

        public void QuitPressed()
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
