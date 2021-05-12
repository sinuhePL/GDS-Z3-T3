using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundSystem", menuName = "Scriptable Objects/Sound System")]
public class SoundSystem : ScriptableObject, IListenable
{
    public class MyAudioSource
    {
        public AudioSource _soundSource;
        public bool _isAvailable;

        public MyAudioSource()
        {
            _isAvailable = true;
            GameObject soundGameObject = new GameObject("Sound Source");
            _soundSource = soundGameObject.AddComponent<AudioSource>();
            _soundSource.loop = false;
        }
    }

    private static SoundSystem _instance;
    private MyAudioSource[] _audioPool;
    public IntegerReference _poolSize;
    public BoolReference _isOn;
    public FloatReference _soundVolume;
    public AudioEvent[] _soundObjects;
    public GameEvent[] _events;

    private void Initialize()
    {
        foreach (GameEvent _event in _events)
        {
            _event.RegisterListener(this);
        }
    }

    private void OnDisable()
    {
        foreach (GameEvent _event in _events)
        {
            _event.UnregisterListener(this);
        }
    }

    private MyAudioSource GetMyAudioSource()
    {
        foreach (MyAudioSource mySource in _audioPool)
        {
            if (mySource._isAvailable)
            {
                mySource._isAvailable = false;
                return mySource;
            }
        }
        Debug.LogError("Sound pool depleted!");
        return null;
    }

    private static IEnumerator ReleaseAudioSource(MyAudioSource mySource, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        mySource._isAvailable = true;
    }

    public static SoundSystem Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = Resources.FindObjectsOfTypeAll<SoundSystem>()[0] as SoundSystem;
                _instance.Initialize();
            }
            if (!_instance)
            {
                _instance = CreateInstance<SoundSystem>();
                _instance.Initialize();
            }
            return _instance;
        }
    }

    public void OnEventRaised(GameEvent gameEvent)
    {
        if (_isOn.Value)
        {
            if (_audioPool == null)
            {
                _audioPool = new MyAudioSource[_poolSize.Value];
                for (int i = 0; i < _poolSize.Value; i++)
                {
                    _audioPool[i] = new MyAudioSource();
                }
            }
            foreach (AudioEvent sound in _soundObjects)
            {
                if (sound._relatedEvent == gameEvent)
                {
                    MyAudioSource freeAudioSource = GetMyAudioSource();
                    float seconds = sound.Play(freeAudioSource._soundSource);
                    GameAssets._instance.StartCoroutine(ReleaseAudioSource(freeAudioSource, seconds));
                }
            }
        }
    }
}
