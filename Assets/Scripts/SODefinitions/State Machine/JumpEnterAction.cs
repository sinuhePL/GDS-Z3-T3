﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "JumpEnterAction", menuName = "Scriptable Objects/AI/Jump Enter Action")]
    public class JumpEnterAction : Action
    {
        public override void Act(CharacterBrain brain)
        {
           
        }

        public override void ActFixed(CharacterBrain brain)
        {
            GameCharacterController controlledCharacter = brain._controlledCharacter;
            Rigidbody2D controlledBody = controlledCharacter.GetRigidbody2D();
            controlledBody.isKinematic = false;
            controlledCharacter.Jump(brain._jumpYVelocity);
        }
    }
}
