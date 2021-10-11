using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GDS3
{
    public class MainMenuFadeController : MonoBehaviour
    {
        [SerializeField] private Image _myImage;
        [SerializeField] private float _fadeOutTime;

        private IEnumerator FadeImage(bool fadeOut)
        {
            float interpolationPoint;
            float currentAlpha;
            for (float t = 0; t < _fadeOutTime; t += Time.deltaTime)
            {
                interpolationPoint = t / _fadeOutTime;
                if (fadeOut)
                {
                    currentAlpha = Mathf.Lerp(1.0f, 0.0f, interpolationPoint);
                }
                else
                {
                    currentAlpha = Mathf.Lerp(0.0f, 1.0f, interpolationPoint);
                }
                _myImage.color = new Color(_myImage.color.r, _myImage.color.g, _myImage.color.b, currentAlpha);
                yield return 0;
            }
            if (fadeOut)
            {
                gameObject.SetActive(false);
            }
            else
            {
                SceneManager.LoadScene("LevelScene");
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(FadeImage(true));
        }

        public void StartFadeIn()
        {
            gameObject.SetActive(true);
            StartCoroutine(FadeImage(false));
        }
    }
}
