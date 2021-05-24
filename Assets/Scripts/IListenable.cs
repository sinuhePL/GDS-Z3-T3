using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public interface IListenable
    {
        void OnEventRaised(GameEvent _event);
    }
}
