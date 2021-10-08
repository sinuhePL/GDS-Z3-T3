using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class ColorCrossFade : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _mySpriteRenderer;
        [SerializeField] private Color _targetColor;
        [SerializeField] private float _crossFadeTime;
        [SerializeField] private float _delayTime;

        private IEnumerator FadeCoroutine()
        {
            float interpolationPoint;
            yield return new WaitForSeconds(_delayTime);
            Color currentColor = new Color();
            Color startingColor = new Color(_mySpriteRenderer.color.r, _mySpriteRenderer.color.g, _mySpriteRenderer.color.b, 1.0f);
            for(float t = 0.0f; t < _crossFadeTime; t += Time.deltaTime)
            {
                interpolationPoint = t / _crossFadeTime;
                currentColor.r = Mathf.Lerp(startingColor.r, _targetColor.r, interpolationPoint);
                currentColor.g = Mathf.Lerp(startingColor.g, _targetColor.g, interpolationPoint);
                currentColor.b = Mathf.Lerp(startingColor.b, _targetColor.b, interpolationPoint);
                currentColor.a = 1.0f;
                _mySpriteRenderer.color = currentColor;
                yield return 0;
            }
        }

        public void StartFade()
        {
            StartCoroutine(FadeCoroutine());
        }
    }
}
