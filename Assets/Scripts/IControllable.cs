using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public interface IControllable
    {
        Rigidbody2D GetRigidbody2D();
        Transform GetTransform();
        LayerMask GetGroundMask();
        Transform GetGroundCheck();
        Transform GetHitCheck();
        void MoveMe(float moveSpeed);
        void Jump(float jumpYVelocity);
        void ApplyForce(Vector2 force);
    }
}
