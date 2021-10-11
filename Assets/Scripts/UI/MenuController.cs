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

        private void Awake()
        {
            _menuPanel.SetActive(false);
            _isMenuActive = false;
            _yesNoPanel = null;
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
            GameObject settingsPanel = Instantiate(_settingsPrefab, transform.parent);
            settingsPanel.transform.localPosition = new Vector3(0, -100, 0);
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
