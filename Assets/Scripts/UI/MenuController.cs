using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDS3
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _settingsPrefab;
        [SerializeField] private GameObject _yesNoPrefab;
        [SerializeField] private GameObject _menuPanel;
        [SerializeField] private UnityEvent _pausePressedEvent;
        [SerializeField] private FadeOutController _myFadeOut;
        private bool _isMenuActive;
        private GameObject _yesNoPanel;
        private GameObject _settingsPanel;

        private void Awake()
        {
            _menuPanel.SetActive(false);
            _isMenuActive = false;
            _yesNoPanel = null;
            _settingsPanel = null;
        }

        private void YesClicked()
        {
            _myFadeOut.BackToMainMenu();
        }

        private void NoClicked()
        {
            Destroy(_yesNoPanel);
        }

        public void MenuPressed()
        {
            if(_isMenuActive)
            {
                if (_settingsPanel != null && _settingsPanel.activeSelf)
                {
                    Destroy(_settingsPanel);
                    _settingsPanel = null;
                }
                else if (_yesNoPanel != null && _yesNoPanel.activeSelf)
                {
                    Destroy(_yesNoPanel);
                    _yesNoPanel = null;
                }
                else
                {
                    BackPressed();
                }
            }
            else
            {
                _pausePressedEvent.Invoke();
                _menuPanel.SetActive(true);
                _isMenuActive = true;
                Cursor.visible = true;
            }
        }

        public void BackPressed()
        {
            Cursor.visible = false;
            _menuPanel.SetActive(false);
            _isMenuActive = false;
            _pausePressedEvent.Invoke();
        }

        public void SettingsPressed()
        {
            _settingsPanel = Instantiate(_settingsPrefab, transform.parent);
            _settingsPanel.transform.localPosition = new Vector3(0, -100, 0);
        }

        public void QuitPressed()
        {
            _yesNoPanel = Instantiate(_yesNoPrefab, transform.parent);
            YesNoController quitConfirmation = _yesNoPanel.GetComponent<YesNoController>();
            if(quitConfirmation != null)
            {
                quitConfirmation.Initialize(YesClicked, NoClicked);
            }
        }
    }
}
