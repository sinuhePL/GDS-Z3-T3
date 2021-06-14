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
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private Transform _hitCheck;
        private CharacterMovementController _myMovement;
        private CharacterJumpController _myJump;

        private void Awake()
        {
            _myMovement = new CharacterMovementController(_myBody, _isFacingRight);
            _myJump = new CharacterJumpController(_myBody);
            if (_myBrain != null)
            {
                _myBrain.Initialize(this);
            }
        }

        private void Update()
        {
            if (_myBrain != null)
            {
                _myBrain.ThinkAboutAnimation();
            }
        }

        private void FixedUpdate()
        {
            if (_myBrain != null)
            {
                _myBrain.ThinkAboutPhysics();
            }
        }

        private void OnDrawGizmos()
        {
            if (_myBrain != null)
            {
                _myBrain.DrawGizmo(transform.position);
            }
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

        public LayerMask GetGroundMask()
        {
            return _whatIsGround;
        }

        public Transform GetGroundCheck()
        {
            return _groundCheck;
        }

        public Transform GetHitCheck()
        {
            return _hitCheck;
        }
    }
}
