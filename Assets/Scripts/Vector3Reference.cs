using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GDS3
{
    [Serializable]
    public class Vector3Reference
    {
        public bool _useConstant = true;
        public Vector3 _constantValue;
        public Vector3Variable _variable;

        public Vector3 Value
        {
            get
            {
                return _useConstant ? _constantValue : _variable.Value;
            }

            set
            {
                if (_useConstant)
                {
                    _constantValue = value;
                }
                else
                {
                    _variable.Value = value;
                }
            }
        }
    }
}
