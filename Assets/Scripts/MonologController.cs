using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

namespace GDS3
{
    public class MonologController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _myRigidbody;
        [SerializeField] private SpriteRenderer _cloud;
        [SerializeField] private TextMeshPro _cloudText;
        [SerializeField] private float _displayTime;
        [SerializeField] private float _sequenceDelay;
        [SerializeField] private BoolReference _isInputBlocked;
        private bool _isAnyKeyPressed;

        private void Awake()
        {
            _cloudText.enabled = false;
            _cloud.enabled = false;
            _isAnyKeyPressed = true;
        }

        private IEnumerator ShowCloud()
        {
            _cloud.enabled = true;
            _cloudText.enabled = true;
            for (float t = 0.0f; t < _displayTime && !_isAnyKeyPressed; t += Time.deltaTime)
            {
                yield return 0;
            }
            _cloud.enabled = false;
            /*_isInputBlocked.Value = false;*/
            _cloudText.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Monolog")
            {
                MonologTriggerController monologTrigger = collision.gameObject.GetComponent<MonologTriggerController>();
                if (monologTrigger != null)
                {
                    string myMonolog = monologTrigger.GetMonolog();
                    if (myMonolog.Length > 0)
                    {
                        ShowMonolog(myMonolog);
                    }
                }
            }
        }

        public void ShowMonolog(string monolog)
        {
            /*_isInputBlocked.Value = true;*/
            _cloudText.text = monolog;
            if(monolog.Length < 70)
            {
                _cloud.transform.localScale = new Vector3(6.5f, 6.5f, 6.5f);
            }
            else
            {
                _cloud.transform.localScale = new Vector3(9.0f, 10.0f, 6.5f);
            }
            if (transform.localScale.x < 0)
            {
                _cloudText.transform.localScale = new Vector3(-Mathf.Abs(_cloudText.transform.localScale.x), _cloudText.transform.localScale.y, _cloudText.transform.localScale.z);
            }
            else
            {
                _cloudText.transform.localScale = new Vector3(Mathf.Abs(_cloudText.transform.localScale.x), _cloudText.transform.localScale.y, _cloudText.transform.localScale.z);
            }
            _isAnyKeyPressed = false;
            StartCoroutine(ShowCloud());
        }

        private void Update()
        {
            if(Keyboard.current.anyKey.wasPressedThisFrame && !_isAnyKeyPressed && !Keyboard.current.qKey.isPressed)
            {
                _cloud.enabled = false;
                /*_isInputBlocked.Value = false;*/
                _cloudText.enabled = false;
                _isAnyKeyPressed = true;
            }
        }
    }
}
