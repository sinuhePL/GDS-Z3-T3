using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GDS3
{
    [CreateAssetMenu(fileName = "MusicSystem", menuName = "Scriptable Objects/Music System")]
    public class MusicSystem : ScriptableObject, IListenable
    {
        private static MusicSystem _instance;
        private AudioSource _musicAudioSource1;
        private AudioSource _musicAudioSource2;
        private AudioSource _currentAudioSource;
        private AudioEvent _lastPlayedMusic;
        private int _turnOnCounter;
        private bool _isGamePaused;

        public BoolReference _isOn;
        public FloatReference _musicVolume;
        public float _transitionTime = 1.0f;
        public AudioEvent[] _musicObjects;
        public GameEvent[] _events;
        public GameEvent _musicStateChangedEvent;
        public GameEvent _musicVolumeChangedEvent;
        public GameEvent _pauseEvent;

        private void Initialize()
        {
            foreach (GameEvent _event in _events)
            {
                _event.RegisterListener(this);
            }
            _musicStateChangedEvent.RegisterListener(this);
            _musicVolumeChangedEvent.RegisterListener(this);
            _pauseEvent.RegisterListener(this);
            _isGamePaused = false;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _currentAudioSource = null;
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
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
            _musicStateChangedEvent.UnregisterListener(this);
            _musicVolumeChangedEvent.UnregisterListener(this);
            _pauseEvent.UnregisterListener(this);
            SceneManager.sceneLoaded -= OnSceneLoaded;
            _currentAudioSource = null;
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

        private IEnumerator ResumeMusic(AudioEvent musicType, float seconds, AudioSource musicSource, int invokeTurnOnCounter)
        {
            yield return new WaitForSeconds(seconds);
            while (_currentAudioSource == musicSource && _isOn.Value == true && invokeTurnOnCounter == _turnOnCounter)
            {
                float seconds2;
                seconds2 = musicType.Play(musicSource, _musicVolume.Value);
                yield return new WaitForSeconds(seconds2);
            }
        }

        private IEnumerator FadeOutMusic(float fadeOutTime)
        {
            for (float t = 0; t < fadeOutTime; t += Time.deltaTime)
            {
                _currentAudioSource.volume = _musicVolume.Value * (1 - (t / fadeOutTime));
                yield return null;
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
                if (!_instance)
                {
                    _instance = CreateInstance<MusicSystem>();
                    _instance.Initialize();
                }
                return _instance;
            }
        }

        public void StopMusic()
        {
            GameAssets._instance.StartCoroutine(FadeOutMusic(_transitionTime));
        }

        public void OnEventRaised(GameEvent gameEvent)
        {
            float seconds;
            if (_currentAudioSource == null)
            {
                _turnOnCounter = 1;
                GameObject musicObject1 = new GameObject("Music Source 1");
                _musicAudioSource1 = musicObject1.AddComponent<AudioSource>();
                GameObject musicObject2 = new GameObject("Music Source 2");
                _musicAudioSource2 = musicObject2.AddComponent<AudioSource>();
                _musicAudioSource1.loop = false;
                _musicAudioSource2.loop = false;
                _musicAudioSource1.volume = _musicVolume.Value;
                _musicAudioSource2.volume = _musicVolume.Value;
                _currentAudioSource = _musicAudioSource1;
            }
            if (_musicStateChangedEvent == gameEvent)
            {
                if (_isOn.Value == true)
                {
                    _turnOnCounter++;
                    _currentAudioSource.volume = _musicVolume.Value;
                    seconds = _lastPlayedMusic.Play(_currentAudioSource, _musicVolume.Value);
                    GameAssets._instance.StartCoroutine(ResumeMusic(_lastPlayedMusic, seconds, _currentAudioSource, _turnOnCounter));
                }
                else if (_isOn.Value == false)
                {
                    _currentAudioSource.Stop();
                }
            }
            else if (_musicVolumeChangedEvent == gameEvent)
            {
                if (_currentAudioSource != null)
                {
                    _currentAudioSource.volume = _musicVolume.Value;
                }
            }
            else if (_isOn.Value)
            {
                if (_pauseEvent == gameEvent)
                {
                    if (_isGamePaused)
                    {
                        _currentAudioSource.UnPause();
                        _isGamePaused = false;
                    }
                    else
                    {
                        _currentAudioSource.Pause();
                        _isGamePaused = true;
                    }
                }
                else
                {
                    foreach (AudioEvent music in _musicObjects)
                    {
                        if (music._relatedEvent == gameEvent)
                        {
                            if (_currentAudioSource != null)
                            {
                                _turnOnCounter++;
                                AudioSource newAudioSource = (_musicAudioSource1 == _currentAudioSource) ? _musicAudioSource2 : _musicAudioSource1;
                                newAudioSource.volume = _musicVolume.Value;
                                seconds = music.Play(newAudioSource, _musicVolume.Value);
                                GameAssets._instance.StartCoroutine(UpdateMusicWithCrossFade(_currentAudioSource, newAudioSource, _transitionTime));
                                _currentAudioSource = newAudioSource;
                                GameAssets._instance.StartCoroutine(ResumeMusic(music, seconds, _currentAudioSource, _turnOnCounter));
                                _lastPlayedMusic = music;
                            }
                        }
                    }
                }
            }
        }
    }
}
