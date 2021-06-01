using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResizable
{
    IEnumerator Resize(float resizeFactor, float resizeTime);
    bool CheckIfSmall();
}
