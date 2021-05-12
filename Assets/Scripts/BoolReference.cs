using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BoolReference
{
    public bool _useConstant = true;
    public bool _constantValue;
    public BoolVariable _variable;

    public bool Value
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
