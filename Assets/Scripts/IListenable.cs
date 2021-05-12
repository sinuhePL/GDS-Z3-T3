using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IListenable 
{
    void OnEventRaised(GameEvent _event);
}
