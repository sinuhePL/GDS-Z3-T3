using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public GameEvent _event;
    public GameEvent _event2;
    public UnityEvent _startScene;

    private void OnEnable()
    {
        Debug.Log(SoundSystem.Instance._isOn.Value);
        Debug.Log(MusicSystem.Instance._isOn.Value);
    }

    private void Start()
    {
        _startScene.Invoke();
    }
}
