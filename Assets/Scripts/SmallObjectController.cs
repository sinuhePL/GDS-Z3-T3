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
            float currentScaleX, currentScaleY;
            Vector3 targetScale = new Vector3(0.0f, 0.0f, 0.0f);
            if(_isSmallSize.Value)
            {
                targetScale = _startScale;
            }
            for (float t = 0; t < _sizeChangeTime.Value; t += Time.deltaTime)
            {
                currentScaleX = Mathf.Lerp(transform.localScale.x, targetScale.x, t / _sizeChangeTime.Value);
                currentScaleY = Mathf.Lerp(transform.localScale.y, targetScale.y, t / _sizeChangeTime.Value);
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
