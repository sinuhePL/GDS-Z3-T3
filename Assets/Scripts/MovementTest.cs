using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    public Rigidbody2D _myRigidbody2D;
    public Animator _myAnimator;

    private float _movementX;
    private float _movementY;
    private Vector3 m_Velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        _movementX = Input.GetAxisRaw("Horizontal");
        _movementY = Input.GetAxisRaw("Vertical");
        if(Input.GetButtonDown("Fire1")) _myAnimator.SetTrigger("attack");
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector2(_movementX * 100f, _movementY * 100f) * Time.deltaTime;
        // And then smoothing it out and applying it to the character
        _myRigidbody2D.velocity = Vector3.SmoothDamp(_myRigidbody2D.velocity, targetVelocity, ref m_Velocity, 0.05f);
        _myAnimator.SetFloat("walk_speed", _movementX * 100f);
    }
}
