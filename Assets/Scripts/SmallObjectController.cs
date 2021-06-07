using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class SmallObjectController : MonoBehaviour
    {
        [SerializeField] private BoolReference _isSmallSize;
        [SerializeField] private FloatReference _sizeChangeTime;
        private Vector3 _startScale;

        private IEnumerator ChangeSize()
        {
            float currentScaleX, currentScaleY, interpolationPoint;
            Vector3 startingScale = _startScale;
            Vector3 targetScale = new Vector3(0.0f, 0.0f, 0.0f);
            if(_isSmallSize.Value)
            {
                targetScale = _startScale;
                startingScale = new Vector3(0.0f, 0.0f, 0.0f);
            }
            for (float t = 0; t < _sizeChangeTime.Value; t += Time.deltaTime)
            {
                interpolationPoint = t / _sizeChangeTime.Value;
                interpolationPoint = interpolationPoint * interpolationPoint * (3f - 2f * interpolationPoint);
                currentScaleX = Mathf.Lerp(startingScale.x, targetScale.x, interpolationPoint);
                currentScaleY = Mathf.Lerp(startingScale.y, targetScale.y, interpolationPoint);
                transform.localScale = new Vector3(currentScaleX, currentScaleY, 0.0f);
                yield return 0;
            }
        }

        private void Start()
        {
            _startScale = transform.localScale;
            transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        }

        public void Resize()
        {
            StartCoroutine(ChangeSize());
        }
    }
}
