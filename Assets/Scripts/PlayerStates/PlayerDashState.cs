using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class PlayerDashState : PlayerCharacterState
    {
        public PlayerDashState(PlayerCharacterController controller) : base(controller)
        {
            RaycastHit2D hit;
            float dashDistance;
            _myController._lastDashTime = Time.time;
            Transform controlledTransform = _myController.transform;
            Rigidbody2D controlledBody = _myController._myBody;
            Animator controlledAnimator = _myController._myAnimator;
            controlledBody.isKinematic = true;
            controlledBody.velocity = new Vector2(controlledBody.velocity.x, 0.0f);
            _myController._currentCooldownTime = _myController._dashCooldownTime.Value;
            controlledAnimator.SetTrigger("dash");
            if (controlledTransform.localScale.x > 0 && controller.RightFacing() || controlledTransform.localScale.x < 0 && !controller.RightFacing())
            {
                hit = Physics2D.Raycast(controller._dashCheck.position, controlledTransform.TransformDirection(Vector3.right), _myController._dashDistance.Value, controller._dashObstacles);
                dashDistance = _myController._dashDistance.Value;
                if(hit.collider != null)
                {
                    dashDistance = hit.distance;
                }
                Debug.DrawRay(controller._dashCheck.position, controller.transform.TransformDirection(Vector3.forward) * dashDistance, Color.yellow);
                if (controller._dashParticleSystem.transform.localScale.x < 0.0f)
                {
                    controller._dashParticleSystem.transform.localScale = new Vector3(-controller._dashParticleSystem.transform.localScale.x, controller._dashParticleSystem.transform.localScale.y, controller._dashParticleSystem.transform.localScale.z);
                }
                _myController._attackEndPosition = new Vector3(controlledTransform.position.x + dashDistance, controlledTransform.position.y, controlledTransform.position.z);
            }
            else
            {
                hit = Physics2D.Raycast(controller._dashCheck.position, controlledTransform.TransformDirection(Vector3.left), _myController._dashDistance.Value, controller._dashObstacles);
                dashDistance = _myController._dashDistance.Value;
                if (hit.collider != null)
                {
                    dashDistance = hit.distance - 0.5f;
                }
                Debug.DrawRay(controller._dashCheck.position, controller.transform.TransformDirection(Vector3.forward) * dashDistance, Color.yellow);
                _myController._attackEndPosition = new Vector3(controlledTransform.position.x - dashDistance, controlledTransform.position.y, controlledTransform.position.z);
                if (controller._dashParticleSystem.transform.localScale.x > 0.0f)
                {
                    controller._dashParticleSystem.transform.localScale = new Vector3(-controller._dashParticleSystem.transform.localScale.x, controller._dashParticleSystem.transform.localScale.y, controller._dashParticleSystem.transform.localScale.z);
                }
            }
            controller._dashParticleSystem.Play();
            _gizmoColor = Color.blue;
        }

        public override PlayerCharacterState Dash()
        {
            return null;
        }

        public override PlayerCharacterState StateFixedUpdate()
        {
            Transform controlledTransform = _myController.transform;
            Rigidbody2D controlledBody = _myController._myBody;
            LayerMask groundMask = _myController._whatIsGround;
            Transform groundCheck = _myController._groundCheck;
            Collider2D[] hitColliders;
            if (_myController._attackEndPosition.x > controlledTransform.position.x)
            {
                _myController._myMovement.Move(_myController._dashVelocity.Value, true);
            }
            else if (_myController._attackEndPosition.x < controlledTransform.position.x)
            {
                _myController._myMovement.Move(-_myController._dashVelocity.Value, true);
            }
            hitColliders = Physics2D.OverlapCircleAll(_myController._dashCheck.position, 0.5f, _myController._dashObstacles);
            if (Mathf.Abs(controlledTransform.position.x - _myController._attackEndPosition.x) < 0.5f || hitColliders.Length > 0)
            {
                controlledBody.isKinematic = false;
                if (CheckIfLanded(groundMask, groundCheck.position))
                {
                    _myController._dashParticleSystem.Stop();
                    return new PlayerOnGroundState(_myController);
                }
                else
                {
                    _myController._dashParticleSystem.Stop();
                    return new PlayerJumpState(_myController);
                }
            }
            else
            {
                return null;
            }
        }

        public override void Interact()
        {
            
        }

        public override PlayerCharacterState Attack()
        {
            return null;
        }
    }
}
