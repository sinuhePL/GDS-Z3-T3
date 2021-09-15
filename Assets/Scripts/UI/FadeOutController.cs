using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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
        [SerializeField] private UnityEvent _pauseEvent;

        private IEnumerator FadeInText()
        {
            float interpolationPoint;
            float currentAlpha;
            for (float t = 0; t < _textFadeTime; t += Time.deltaTime)
            {
                interpolationPoint = t / _textFadeTime;
                currentAlpha = Mathf.Lerp(0.0f, 1.0f, interpolationPoint);
                _myText.color = new Color(_myText.color.r, _myText.color.g, _myText.color.b, currentAlpha);
                yield return 0;
            }
            yield return new WaitForSeconds(_waitTime);
            StartCoroutine(FadeOutText());
        }

        private IEnumerator FadeOutText()
        {
            float interpolationPoint;
            float currentAlpha;
            for (float t = 0; t < _textFadeTime; t += Time.deltaTime)
            {
                interpolationPoint = t / _textFadeTime;
                currentAlpha = Mathf.Lerp(1.0f, 0.0f, interpolationPoint);
                _myText.color = new Color(_myText.color.r, _myText.color.g, _myText.color.b, currentAlpha);
                yield return 0;
            }
            StartCoroutine(FadeOutImage());
        }

        private IEnumerator FadeOutImage()
        {
            float interpolationPoint;
            float currentAlpha;
            
            _pauseEvent.Invoke();
            for (float t = 0; t < _fadeOutTime; t += Time.deltaTime)
            {
                interpolationPoint = t / _fadeOutTime;
                currentAlpha = Mathf.Lerp(1.0f, 0.0f, interpolationPoint);
                _myImage.color = new Color(_myImage.color.r, _myImage.color.g, _myImage.color.b, currentAlpha);
                yield return 0;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            _myText.color = new Color(_myText.color.r, _myText.color.g, _myText.color.b, 0.0f);
            _pauseEvent.Invoke();
            StartCoroutine(FadeInText());
        }
    }
}
