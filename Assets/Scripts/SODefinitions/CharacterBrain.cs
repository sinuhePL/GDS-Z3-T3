using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public abstract class CharacterBrain : ScriptableObject
    {
        public virtual void Initialize(GameCharacterController character) { }
        public abstract void ThinkAboutAnimation(float deltaTime);
        public abstract void ThinkAboutPhysics();
    }
}
