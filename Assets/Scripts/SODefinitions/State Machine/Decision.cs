using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(CharacterBrain brain);
    }
}
