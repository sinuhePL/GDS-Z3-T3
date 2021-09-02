using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController
{
    private Rigidbody2D _controlledBody;
    private Transform _controlledTransform;
    private bool _isFacingRight = true;
    private Vector3 _velocity = Vector3.zero;
    private LayerMask _groundMask;
    private float _startingDistanceToGround;

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 theScale = _controlledTransform.localScale;
        theScale.x *= -1;
        _controlledTransform.localScale = theScale;
    }

    private float CheckDistanctToGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(_controlledTransform.position, _controlledTransform.TransformDirection(Vector3.down), Mathf.Infinity, _groundMask);
        if (hit.collider != null)
        {
            return hit.distance;
        }
        else
        {
            return -1.0f;
        }
    }

    public CharacterMovementController(Rigidbody2D parentBody, bool isStartingFacingRight, LayerMask groundMask)
    {
        _controlledBody = parentBody;
        _groundMask = groundMask;
        _controlledTransform = parentBody.gameObject.transform;
        if(!isStartingFacingRight)
        {
            Flip();
            _isFacingRight = !_isFacingRight;
        }
        _startingDistanceToGround = CheckDistanctToGround();
    }

    public void Move(float moveSpeed, bool ignoreGround)
    {
        float currentDistanceToGround;
        if (ignoreGround)
        {
            _controlledBody.velocity = new Vector3(moveSpeed, _controlledBody.velocity.y);
        }
        else
        {
            currentDistanceToGround = CheckDistanctToGround();
            _controlledBody.velocity = new Vector3(moveSpeed, _controlledBody.velocity.y - (currentDistanceToGround - _startingDistanceToGround) / Time.fixedDeltaTime);
        }
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
