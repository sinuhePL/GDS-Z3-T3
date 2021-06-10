using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDS3
{
    public class GameCharacterController : MonoBehaviour, IControllable
    {
        [SerializeField] private CharacterBrain _myBrain;
        /*[SerializeField] private CharacterAttackController _myAttack;*/
        [SerializeField] private Animator _myAnimator;
        [SerializeField] private Rigidbody2D _myBody;
        [SerializeField] private bool _isFacingRight;
        private CharacterMovementController _myMovement;
        private CharacterJumpController _myJump;

        private void Awake()
        {
            _myMovement = new CharacterMovementController(_myBody, _isFacingRight);
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

        private void OnDrawGizmos()
        {
            _myBrain.DrawGizmo();
        }

        public Rigidbody2D GetRigidbody2D()
        {
            return _myBody;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void MoveMe(float moveSpeed)
        {
            _myMovement.Move(moveSpeed);
            _myAnimator.SetFloat("walk_speed", Mathf.Abs(moveSpeed));
        }

        public void Jump(float jumpYVelocity)
        {
            _myJump.Jump(jumpYVelocity);
        }

        public void ApplyForce(Vector2 force)
        {
            _myMovement.ApplyForce(force);
        }

    }
}
