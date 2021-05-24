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

    public void Jump(float jumpForce)
    {
        _controlledBody.AddForce(new Vector2(0f, jumpForce));
    }
}
