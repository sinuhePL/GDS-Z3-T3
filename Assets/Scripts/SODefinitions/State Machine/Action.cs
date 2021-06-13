using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Act(CharacterBrain brain);
        public abstract void ActFixed(CharacterBrain brain);
    }
}
