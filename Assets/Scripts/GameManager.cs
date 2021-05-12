using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameEvent _event;
    public GameEvent _event2;

    private void OnEnable()
    {
        Debug.Log(SoundSystem.Instance._isOn.Value);
        Debug.Log(MusicSystem.Instance._isOn.Value);
    }
}
