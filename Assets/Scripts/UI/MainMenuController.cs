using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GDS3
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _settingsPrefab;
        [SerializeField] private GameObject _creditsPrefab;

        public void StartGamePressed()
        {
            SceneManager.LoadScene("TestScene");
        }

        public void CreditsPressed()
        {
            GameObject creditsPanel = Instantiate(_creditsPrefab, transform);
            creditsPanel.transform.localPosition = new Vector3(0, 0, 0);
        }

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