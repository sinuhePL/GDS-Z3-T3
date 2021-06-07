using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJumpController
{
    private Rigidbody2D _controlledBody;

    public CharacterJumpController(Rigidbody2D parentBody)
    {
        _controlledBody = parentBody;
    }

    public void Jump(float jumpYVelocity)
    {
        _controlledBody.velocity = new Vector2(_controlledBody.velocity.x, jumpYVelocity);
    }
}
