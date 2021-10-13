using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace GDS3
{
    public class FadeOutController : MonoBehaviour
    {
        [SerializeField] private Image _myImage;
        [SerializeField] private Text _myText;
        [Range(0.0f, 3.0f)]
        [SerializeField] private float _textFadeTime;
        [Range(0.0f, 3.0f)]
        [SerializeField] private float _fadeOutTime;
        [Range(0.0f, 10.0f)]
        [SerializeField] private float _waitTime;
        [Range(10.0f, 40.0f)]
        [SerializeField] private float _endDelayTime;
        [SerializeField] private UnityEvent _pauseEvent;
        [SerializeField] private string _startString;
        [SerializeField] private string _endString;
        [SerializeField] private UnityEvent _startLevelEvent;
        [SerializeField] private float _sceneChangeDelay;
        [SerializeField] private float _sceneChangeFadeTime;

        private IEnumerator FadeText(bool isGameEnd, bool isFadeOut)
        {
            float interpolationPoint;
            float currentAlpha;
            for (float t = 0; t < _textFadeTime; t += Time.deltaTime)
            {
                interpolationPoint = t / _textFadeTime;
                if(isFadeOut)
                {
                    currentAlpha = Mathf.Lerp(1.0f, 0.0f, interpolationPoint);
                }
                else
                {
                    currentAlpha = Mathf.Lerp(0.0f, 1.0f, interpolationPoint);
                }
                _myText.color = new Color(_myText.color.r, _myText.color.g, _myText.color.b, currentAlpha);
                yield return 0;
            }
            if (!isFadeOut && !isGameEnd)
            {
                yield return new WaitForSeconds(_waitTime);
                StartCoroutine(FadeText(false, true));
            }
            else if(isFadeOut && !isGameEnd)
            {
                _myText.color = new Color(_myText.color.r, _myText.color.g, _myText.color.b, 0.0f);
                StartCoroutine(FadeImage(false));
            }
        }

        private IEnumerator FadeImage(bool isGameEnd, float delay=0.0f)
        {
            float interpolationPoint;
            float currentAlpha;

            if (!isGameEnd)
            {
                _pauseEvent.Invoke();
            }
            yield return new WaitForSeconds(delay);
            for (float t = 0; t < _fadeOutTime; t += Time.deltaTime)
            {
                interpolationPoint = t / _fadeOutTime;
                if (!isGameEnd)
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
            if(isGameEnd)
            {
                StartCoroutine(FadeText(true, false));
            }
        }

        private IEnumerator ChangeScene(float delay)
        {
            yield return new WaitForSeconds(delay);
            float interpolationPoint;
            float currentAlpha;
            float textInitialAlpha = _myText.color.a;
            float imageInitialAlpha = _myImage.color.a;
            MusicSystem.Instance.StopMusic();
            for (float t = 0; t < _sceneChangeFadeTime; t += Time.deltaTime)
            {
                interpolationPoint = t / _sceneChangeFadeTime;
                if (textInitialAlpha > 0.9f)
                {
                    currentAlpha = Mathf.Lerp(1.0f, 0.0f, interpolationPoint);
                    _myText.color = new Color(_myText.color.r, _myText.color.g, _myText.color.b, currentAlpha);
                }
                if(imageInitialAlpha < 0.1f)
                {
                    currentAlpha = Mathf.Lerp(0.0f, 1.0f, interpolationPoint);
                    _myImage.color = new Color(_myImage.color.r, _myImage.color.g, _myImage.color.b, currentAlpha);
                }
                yield return 0;
            }
            SceneManager.LoadScene("MainScene");
        }

        void Start()
        {
            _myText.color = new Color(_myText.color.r, _myText.color.g, _myText.color.b, 0.0f);
            _myText.text = _startString;
            _pauseEvent.Invoke();
            StartCoroutine(FadeText(false, false));
            _startLevelEvent.Invoke();
        }

        public void EndGameFade()
        {
            _myText.text = _endString;
            StartCoroutine(FadeImage(true, _endDelayTime));
            StartCoroutine(ChangeScene(_sceneChangeDelay));
        }

        public void BackToMainMenu()
        {
            StartCoroutine(ChangeScene(0.0f));
        }
    }
}
