using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FloatReference
{
    public bool _useConstant = true;
    public float _constantValue;
    public FloatVariable _variable;

    public float Value
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
