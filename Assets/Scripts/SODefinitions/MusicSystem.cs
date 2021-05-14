using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicSystem", menuName = "Scriptable Objects/Music System")]
public class MusicSystem : ScriptableObject, IListenable
{
    private static MusicSystem _instance;
    private AudioSource _musicAudioSource1;
    private AudioSource _musicAudioSource2;
    private AudioSource _currentAudioSource;

    public BoolReference _isOn;
    public FloatReference _musicVolume;
    public float _transitionTime = 1.0f;
    public AudioEvent[] _musicObjects;
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
        if (_events != null)
        {
            foreach (GameEvent _event in _events)
            {
                _event.UnregisterListener(this);
            }
        }
    }

    private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float transitionTime)
    {
        float t = 0.0f;
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            original.volume = _musicVolume.Value * (1 - (t / transitionTime));
            newSource.volume = _musicVolume.Value * (t / transitionTime);
            yield return null;
        }
        original.Stop();
    }

    private IEnumerator ResumeMusic(AudioEvent musicType, float seconds, AudioSource musicSource)
    {
        yield return new WaitForSeconds(seconds);
        while (_currentAudioSource == musicSource)
        {
            float seconds2;
            seconds2 = musicType.Play(musicSource);
            yield return new WaitForSeconds(seconds2);
        }
    }

    public static MusicSystem Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = Resources.Load<MusicSystem>("MusicSystem");
                _instance.Initialize();
            }
            if(!_instance)
            {
                _instance = CreateInstance<MusicSystem>();
                _instance.Initialize();
            }
            return _instance;
        }
    }

    public void OnEventRaised(GameEvent gameEvent)
    {
        if (_isOn.Value)
        {
            foreach (AudioEvent music in _musicObjects)
            {
                if (music._relatedEvent == gameEvent)
                {
                    float seconds;
                    if (_musicAudioSource1 == null)
                    {
                        GameObject musicObject1 = new GameObject("Music Source 1");
                        _musicAudioSource1 = musicObject1.AddComponent<AudioSource>();
                        GameObject musicObject2 = new GameObject("Music Source 2");
                        _musicAudioSource2 = musicObject2.AddComponent<AudioSource>();
                        _musicAudioSource1.loop = false;
                        _musicAudioSource2.loop = false;
                        _currentAudioSource = _musicAudioSource1;
                        seconds = music.Play(_currentAudioSource);
                    }
                    else
                    {
                        AudioSource newAudioSource = (_musicAudioSource1 == _currentAudioSource) ? _musicAudioSource2 : _musicAudioSource1;
                        seconds = music.Play(newAudioSource);
                        GameAssets._instance.StartCoroutine(UpdateMusicWithCrossFade(_currentAudioSource, newAudioSource, _transitionTime));
                        _currentAudioSource = newAudioSource;
                    }
                    GameAssets._instance.StartCoroutine(ResumeMusic(music, seconds, _currentAudioSource));
                }
            }
        }
    }
}
