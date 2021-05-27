using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController
{
    private Rigidbody2D _controlledBody;
    private Transform _controlledTransform;
    private bool _isFacingRight = true;
    private Vector3 _velocity = Vector3.zero;
    private float _movementSmoothing;

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 theScale = _controlledTransform.localScale;
        theScale.x *= -1;
        _controlledTransform.localScale = theScale;
    }

    public CharacterMovementController(Rigidbody2D parentBody, bool isStartingFacingRight, float smoothing)
    {
        _controlledBody = parentBody;
        _controlledTransform = parentBody.gameObject.transform;;
        _movementSmoothing = smoothing;
        if(!isStartingFacingRight)
        {
            Flip();
        }
    }

    public void Move(float moveSpeed)
    {
        Vector3 targetVelocity = new Vector2(moveSpeed, _controlledBody.velocity.y);
        _controlledBody.velocity = Vector3.SmoothDamp(_controlledBody.velocity, targetVelocity, ref _velocity, _movementSmoothing);

        if (moveSpeed > 0 && !_isFacingRight)
        {
            Flip();
        }
        else if (moveSpeed < 0 && _isFacingRight)
        {
            Flip();
        }
    }
}
