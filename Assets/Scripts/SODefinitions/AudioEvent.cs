using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AudioEvent : ScriptableObject
{
    public GameEvent _relatedEvent;
    public abstract float Play(AudioSource source);
}
