using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GDS3
{
    public class MonologController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] _clouds;
        [SerializeField] private TextMeshPro _cloudText;
        [SerializeField] private float _displayTime;
        [SerializeField] private float _sequenceDelay;
        [SerializeField] private BoolReference _isInputBlocked;

        private void Awake()
        {
            _cloudText.enabled = false;
            foreach (SpriteRenderer cloud in _clouds)
            {
                cloud.enabled = false;
            }
        }

        private IEnumerator ShowInSequence()
        {
            foreach(SpriteRenderer cloud in _clouds)
            {
                cloud.enabled = true;
                yield return new WaitForSeconds(_sequenceDelay);
            }
            _cloudText.enabled = true;
            yield return new WaitForSeconds(_displayTime);
            foreach(SpriteRenderer cloud in _clouds)
            {
                cloud.enabled = false;
            }
            _isInputBlocked.Value = false;
            _cloudText.enabled = false;
        }

        public void ShowMonolog(string monolog)
        {
            _isInputBlocked.Value = true;
            _cloudText.text = monolog;
            if (transform.parent.localScale.x < 0)
            {
                _cloudText.transform.localScale = new Vector3(-Mathf.Abs(_cloudText.transform.localScale.x), _cloudText.transform.localScale.y, _cloudText.transform.localScale.z);
            }
            else
            {
                _cloudText.transform.localScale = new Vector3(Mathf.Abs(_cloudText.transform.localScale.x), _cloudText.transform.localScale.y, _cloudText.transform.localScale.z);
            }
            StartCoroutine(ShowInSequence());
        }
    }
}
