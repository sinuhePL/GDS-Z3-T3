using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController
{
    private Rigidbody2D _controlledBody;
    private Transform _controlledTransform;
    private bool _isFacingRight = true;
    private Vector3 _velocity = Vector3.zero;

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 theScale = _controlledTransform.localScale;
        theScale.x *= -1;
        _controlledTransform.localScale = theScale;
    }

    public CharacterMovementController(Rigidbody2D parentBody, bool isStartingFacingRight)
    {
        _controlledBody = parentBody;
        _controlledTransform = parentBody.gameObject.transform;
        if(!isStartingFacingRight)
        {
            Flip();
            _isFacingRight = !_isFacingRight;
        }
    }

    public void Move(float moveSpeed)
    {
        _controlledBody.velocity = new Vector3(moveSpeed, _controlledBody.velocity.y); 

        if (moveSpeed > 0 && !_isFacingRight)
        {
            Flip();
        }
        else if (moveSpeed < 0 && _isFacingRight)
        {
            Flip();
        }
    }

    public void ApplyForce(Vector2 force)
    {
        _controlledBody.AddForce(force);
        if (_controlledBody.velocity.x > 0 && !_isFacingRight)
        {
            Flip();
        }
        else if (_controlledBody.velocity.x < 0 && _isFacingRight)
        {
            Flip();
        }
    }
}
