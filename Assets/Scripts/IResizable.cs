using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public interface IResizable
    {
        float GetFactor();
        float GetSmallSpeed();
        float GetBigSpeed();
        void SetCurrentSpeed(float speed);
        float GetChangetime();
    }
}
