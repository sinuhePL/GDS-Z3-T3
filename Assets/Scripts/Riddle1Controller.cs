using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class Riddle1Controller : MonoBehaviour
    {
        [SerializeField] private PlayerButtonController[] _buttons;
        [SerializeField] private RiddleDoorController _myDoor;
        private int _pressedButtons;
        private bool _properOrder;

        private void Awake()
        {
            _pressedButtons = 0;
            _properOrder = true;
        }

        public void ButtonPressed(PlayerButtonController button)
        {
            if(_pressedButtons == 0 && button == _buttons[0])
            {
                _pressedButtons = 1;
            }
            else if (_pressedButtons == 1 && button == _buttons[1])
            {
                _pressedButtons = 2;
            }
            else if(_pressedButtons == 2 && button == _buttons[2])
            {
                _pressedButtons = 0;
                if (_properOrder)
                {
                    _myDoor.OpenDoor();
                }
                _properOrder = true;
                foreach(PlayerButtonController playerButton in _buttons)
                {
                    playerButton.ReleaseButton();
                }
            }
            else
            {
                _properOrder = false;
                _pressedButtons++;
                if(_pressedButtons == 3)
                {
                    _properOrder = true;
                    _pressedButtons = 0;
                    foreach (PlayerButtonController playerButton in _buttons)
                    {
                        playerButton.ReleaseButton();
                    }
                }
            }
        }
    }
}