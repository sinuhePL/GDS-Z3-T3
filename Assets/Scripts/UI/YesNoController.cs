using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class YesNoController : MonoBehaviour
    {
        private System.Action _yesAction;
        private System.Action _noAction;
        public void Initialize(System.Action yes, System.Action no)
        {
            _yesAction = yes;
            _noAction = no;
        }

        public void YesPressed()
        {
            _yesAction();
        }

        public void NoPressed()
        {
            _noAction();
        }
    }
}