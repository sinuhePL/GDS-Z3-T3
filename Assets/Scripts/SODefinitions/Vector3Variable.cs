using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "myVector3", menuName = "Scriptable Objects/Vector3 Variable")]
    public class Vector3Variable : ScriptableObject
    {
        public Vector3 Value;
    }
}
