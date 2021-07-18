using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class Riddle1Controller : MonoBehaviour
    {
        [SerializeField] private PlayerButtonController _button1;
        [SerializeField] private PlayerButtonController _button2;
        [SerializeField] private PlayerButtonController _button3;
        [SerializeField] private RiddleDoorController _myDoor;
        private bool _isButton1Pressed;
        private bool _isButton2Pressed;
        private bool _isButton3Pressed;

        public bool _button1Pressed
        {
            get 
            {
                
                return _isButton1Pressed; 
            }
        }
        private void Awake()
        {
            _isButton1Pressed = false;
            _isButton2Pressed = false;
            _isButton3Pressed = false;
        }
    }
}