using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "myInteger", menuName = "Scriptable Objects/Integer Variable")]
    public class IntegerVariable : ScriptableObject
    {
        public int Value;
    }
}
