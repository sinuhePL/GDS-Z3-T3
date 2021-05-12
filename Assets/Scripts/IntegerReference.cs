using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class IntegerReference 
{
    public bool _useConstant = true;
    public int _constantValue;
    public IntegerVariable _variable;

    public int Value
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
