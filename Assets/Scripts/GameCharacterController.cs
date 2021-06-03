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
        [SerializeField] private FloatReference _myMaxVelocity;
        /*[SerializeField] private CharacterAttackController _myAttack;*/
        [SerializeField] private Animator _myAnimator;
        [SerializeField] private Rigidbody2D _myBody;
        [SerializeField] private bool _isFacingRight;
        [Range(0, .3f)] [SerializeField] private float _movementSmoothing = 0.05f;
        public UnityEvent _changeSizeEvent;
        private CharacterMovementController _myMovement;
        private CharacterJumpController _myJump;

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
            float startingScaleX = transform.localScale.x;
            float targetScaleX = transform.localScale.x * resizeFactor;
            float startingScaleY = transform.localScale.y;
            float targetScaleY = transform.localScale.y * resizeFactor;
            float startingMaxVelocity = _myMaxVelocity.Value;
            float targetMaxVelocity = _myMaxVelocity.Value * resizeFactor;
            for (float t = 0; t < resizeTime; t += Time.deltaTime)
            {
                float interpolationPoint = t / resizeTime;
                interpolationPoint = interpolationPoint * interpolationPoint * (3f - 2f * interpolationPoint);
                if (isFacingRight && transform.localScale.x < 0.0 || !isFacingRight && transform.localScale.x > 0.0) // used when character orientation flipped during shrinking
                {
                    isFacingRight = !isFacingRight;
                    targetScaleX = -targetScaleX;
                    startingScaleX = -startingScaleX;
                }
                currentScaleX = Mathf.Lerp(startingScaleX, targetScaleX, interpolationPoint);
                currentScaleY = Mathf.Lerp(startingScaleY, targetScaleY, interpolationPoint);
                transform.localScale = new Vector3(currentScaleX, currentScaleY, 0.0f);
                _myMaxVelocity.Value = Mathf.Lerp(startingMaxVelocity, targetMaxVelocity, interpolationPoint);
                yield return 0;
            }
            transform.localScale = new Vector3(targetScaleX, targetScaleY, 0.0f);
            _myMaxVelocity.Value = targetMaxVelocity;
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
