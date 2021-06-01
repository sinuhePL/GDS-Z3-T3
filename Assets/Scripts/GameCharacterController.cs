using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDS3
{
    public class GameCharacterController : MonoBehaviour, IResizable
    {
        [SerializeField] private BoolReference _isSmallSize;
        [SerializeField] private CharacterBrain _myBrain;
        [SerializeField] private CharacterJumpController _myJump;
        [SerializeField] private Collider2D _myCollider;
        /*[SerializeField] private CharacterAttackController _myAttack;*/
        [SerializeField] private Animator _myAnimator;
        [SerializeField] private Rigidbody2D _myBody;
        [SerializeField] private bool _isFacingRight;
        [Range(0, .3f)] [SerializeField] private float _movementSmoothing = 0.05f;
        public UnityEvent _changeSizeEvent;
        private CharacterMovementController _myMovement;

        private void Awake()
        {
            _myMovement = new CharacterMovementController(_myBody, _isFacingRight, _movementSmoothing);
            _myJump = new CharacterJumpController(_myBody);
            _myBrain.Initialize(this);
            _isSmallSize.Value = false;
        }

        private void Update()
        {
            _myBrain.ThinkAboutAnimation(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _myBrain.ThinkAboutPhysics();
        }

        public IEnumerator Resize(float resizeFactor, float resizeTime)
        {
            bool isFacingRight;
            float currentScaleX, currentScaleY;

            _isSmallSize.Value = !_isSmallSize.Value;
            _changeSizeEvent.Invoke();
            if (transform.localScale.x > 0.0)
            {
                isFacingRight = true;
            }
            else
            {
                isFacingRight = false;
            }
            float targetScaleX = transform.localScale.x * resizeFactor;
            float targetScaleY = transform.localScale.y * resizeFactor;
            for (float t = 0; t < resizeTime; t += Time.deltaTime)
            {
                if (isFacingRight && transform.localScale.x < 0.0 || !isFacingRight && transform.localScale.x > 0.0) // used when character orientation fliped during shrinking
                {
                    isFacingRight = !isFacingRight;
                    targetScaleX = -targetScaleX;
                }
                currentScaleX = Mathf.Lerp(transform.localScale.x, targetScaleX, t / resizeTime);
                currentScaleY = Mathf.Lerp(transform.localScale.y, targetScaleY, t / resizeTime);
                transform.localScale = new Vector3(currentScaleX, currentScaleY, 0.0f);
                yield return 0;
            }
        }

        public bool CheckIfSmall()
        {
            return _isSmallSize.Value;
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
