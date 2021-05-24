using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class GameCharacterController : MonoBehaviour
    {
        [SerializeField] private CharacterBrain _myBrain;
        [SerializeField] private CharacterJumpController _myJump;
        /*[SerializeField] private CharacterAttackController _myAttack;*/
        [SerializeField] private Animator _myAnimator;
        [SerializeField] private Rigidbody2D _myBody;
        [SerializeField] private bool _isFacingRight;
        [Range(0, .3f)] [SerializeField] private float _movementSmoothing = 0.05f;
        private CharacterMovementController _myMovement;

        private void Awake()
        {
            _myMovement = new CharacterMovementController(_myBody, transform, _isFacingRight, _movementSmoothing);
            _myJump = new CharacterJumpController(_myBody);
            _myBrain.Initialize(this);
        }

        private void Update()
        {
            _myBrain.ThinkAboutAnimation(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _myBrain.ThinkAboutPhysics();
        }

        public void MoveMe(float moveSpeed)
        {
            _myMovement.Move(moveSpeed);
            _myAnimator.SetFloat("walk_speed", Mathf.Abs(moveSpeed));
        }

        public void JumpMe(float jumpForce)
        {
            _myJump.Jump(jumpForce);
        }
    }
}
