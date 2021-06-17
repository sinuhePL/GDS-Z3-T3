using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "PlayerMoveAction", menuName = "Scriptable Objects/AI/Player Move Action")]
    public class PlayerMoveAction : Action
    {
        private float _horizontalMove;

        public override void Act(CharacterBrain brain)
        {
            _horizontalMove = Input.GetAxisRaw("Horizontal");
            if(Input.GetButtonDown("Fire3"))
            {
                brain._isCharacterSmall.Value = !brain._isCharacterSmall.Value;
                brain._sizeChangeEvent.Invoke();
            }
        }

        public override void ActFixed(CharacterBrain brain)
        {
            brain._controlledCharacter.MoveMe(_horizontalMove * brain._currentMovementSpeed);
        }
    }
}
