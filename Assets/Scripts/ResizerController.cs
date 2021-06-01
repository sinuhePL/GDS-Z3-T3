using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [RequireComponent(typeof(Collider2D))]
    public class ResizerController : MonoBehaviour
    {
        private enum ResizerEnum {Shrinker, Enlarger}
        [SerializeField] private ResizerEnum _resizerType;
        [SerializeField] private FloatReference _sizeChangeFactor;
        [SerializeField] private FloatReference _sizeChangeTime;

        private void OnTriggerExit2D(Collider2D collision)
        {
            IResizable resizable = collision.gameObject.GetComponent<IResizable>();
            if (collision.gameObject.transform.position.x < transform.position.x)
            {
                if (_resizerType == ResizerEnum.Shrinker && resizable.CheckIfSmall())
                {
                    StartCoroutine(resizable.Resize(_sizeChangeFactor.Value, _sizeChangeTime.Value));
                }
                else if (_resizerType == ResizerEnum.Enlarger && !resizable.CheckIfSmall())
                {
                    StartCoroutine(resizable.Resize(1 / _sizeChangeFactor.Value, _sizeChangeTime.Value));
                }
            }
            else if(collision.gameObject.transform.position.x > transform.position.x)
            {
                if (_resizerType == ResizerEnum.Shrinker && !resizable.CheckIfSmall())
                {
                    StartCoroutine(resizable.Resize(1 / _sizeChangeFactor.Value, _sizeChangeTime.Value));
                }
                else if (_resizerType == ResizerEnum.Enlarger && resizable.CheckIfSmall())
                {
                    StartCoroutine(resizable.Resize(_sizeChangeFactor.Value, _sizeChangeTime.Value));
                }
            }
        }
    }
}
