using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GDS3
{
    public class TutorialBirchController : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _myTextBox;
        [SerializeField] private string _myText; 

        private void Start()
        {
            _myTextBox.text = "";
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                _myTextBox.text = _myText;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                _myTextBox.text = "";
            }
        }
    }
}
