using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public interface IControllable
    {
        Rigidbody2D GetRigidbody2D();
        Transform GetTransform();
        void MoveMe(float moveSpeed);
        void Jump(float jumpYVelocity);
        void ApplyForce(Vector2 force);
    }
}
