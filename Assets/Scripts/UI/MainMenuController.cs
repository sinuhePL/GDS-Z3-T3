using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _settingsPrefab;

        public void SettingsPressed()
        {
            GameObject settingsPanel = Instantiate(_settingsPrefab, transform);
            settingsPanel.transform.localPosition = new Vector3(0, 0, 0);
        }

        public void QuitPressed()
        {
            Application.Quit();
        }
    }
}