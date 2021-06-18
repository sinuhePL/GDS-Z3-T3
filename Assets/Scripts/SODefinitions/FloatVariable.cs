using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "myFloat", menuName = "Scriptable Objects/Float Variable")]
    public class FloatVariable : ScriptableObject
    {
        public float Value;
    }
}
