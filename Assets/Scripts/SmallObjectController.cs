using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class SmallObjectController : MonoBehaviour
    {
        [SerializeField] private BoolReference _isSmallSize;
        [SerializeField] private FloatReference _sizeChangeTime;
        private Vector3 _initialScale;
        private int _resizeCoroutineId;

        private void Awake()
        {
            _initialScale = transform.localScale;
            transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            _resizeCoroutineId = 0;
        }

        private IEnumerator ChangeSize()
        {
            float currentScaleX, currentScaleY, interpolationPoint;
            Vector3 startingScale;
            Vector3 targetScale;
            float proportion = _sizeChangeTime.Value/ _initialScale.y;
            int myId = Random.Range(1, 999999999);
            _resizeCoroutineId = myId;
            if (_isSmallSize.Value)
            {
                startingScale = transform.localScale;
                targetScale = _initialScale;
            }
            else
            {
                startingScale = transform.localScale;
                targetScale = new Vector3(0.0f, 0.0f, 0.0f);
            }
            float resizeTime = proportion * Mathf.Abs(startingScale.y - targetScale.y);
            for (float t = 0; t < resizeTime && myId == _resizeCoroutineId; t += Time.deltaTime)
            {
                interpolationPoint = t / resizeTime;
                /*interpolationPoint = interpolationPoint * interpolationPoint * (3f - 2f * interpolationPoint);*/
                currentScaleX = Mathf.Lerp(startingScale.x, targetScale.x, interpolationPoint);
                currentScaleY = Mathf.Lerp(startingScale.y, targetScale.y, interpolationPoint);
                transform.localScale = new Vector3(currentScaleX, currentScaleY, 0.0f);
                Debug.Log(transform.localScale);
                yield return 0;
            }
            if(myId == _resizeCoroutineId)
            {
                transform.localScale = targetScale;
            }
        }

        public void Resize()
        {
            StartCoroutine(ChangeSize());
        }
    }
}
