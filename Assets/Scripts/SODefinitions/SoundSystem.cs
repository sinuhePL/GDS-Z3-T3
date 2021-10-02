using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GDS3
{
    [CreateAssetMenu(fileName = "SoundSystem", menuName = "Scriptable Objects/Sound System")]
    public class SoundSystem : ScriptableObject, IListenable
    {
        public class MyAudioSource
        {
            public AudioSource _soundSource;
            public bool _isAvailable;

            public MyAudioSource(Transform myParent)
            {
                _isAvailable = true;
                GameObject soundGameObject = new GameObject("Sound Source");
                soundGameObject.transform.SetParent(myParent);
                _soundSource = soundGameObject.AddComponent<AudioSource>();
                _soundSource.loop = false;
            }
        }

        private static SoundSystem _instance;
        private MyAudioSource[] _audioPool;
        private bool _isGamePaused;
        public IntegerReference _poolSize;
        public BoolReference _isOn;
        public FloatReference _soundVolume;
        public AudioEvent[] _soundObjects;
        public GameEvent[] _events;
        public GameEvent _pauseEvent;

        private void Initialize()
        {
            foreach (GameEvent _event in _events)
            {
                _event.RegisterListener(this);
            }
            _pauseEvent.RegisterListener(this);
            _isGamePaused = false;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _audioPool = null;
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            foreach (GameEvent _event in _events)
            {
                _event.UnregisterListener(this);
            }
            _pauseEvent.UnregisterListener(this);
            SceneManager.sceneLoaded -= OnSceneLoaded;
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

        private IEnumerator ReleaseAudioSource(MyAudioSource mySource, float seconds)
        {
            if(_isGamePaused)
            {
                yield return null;
            }
            yield return new WaitForSeconds(seconds);
            mySource._isAvailable = true;
        }

        public static SoundSystem Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = Resources.Load<SoundSystem>("SoundSystem");
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
                    GameObject soundsParent = new GameObject("Sound Sources");
                    for (int i = 0; i < _poolSize.Value; i++)
                    {
                        _audioPool[i] = new MyAudioSource(soundsParent.transform);
                    }
                }
                if (_pauseEvent == gameEvent)
                {
                    if (_isGamePaused)
                    {
                        foreach(MyAudioSource audioSource in _audioPool)
                        {
                            audioSource._soundSource.UnPause();
                        }
                        _isGamePaused = false;
                    }
                    else
                    {
                        foreach (MyAudioSource audioSource in _audioPool)
                        {
                            audioSource._soundSource.Pause();
                        }
                        _isGamePaused = true;
                    }
                }
                else
                {
                    foreach (AudioEvent sound in _soundObjects)
                    {
                        if (sound._relatedEvent == gameEvent)
                        {
                            MyAudioSource freeAudioSource = GetMyAudioSource();
                            if (freeAudioSource != null)
                            {
                                freeAudioSource._soundSource.volume = _soundVolume.Value;
                                float seconds = sound.Play(freeAudioSource._soundSource, _soundVolume.Value);
                                GameAssets._instance.StartCoroutine(ReleaseAudioSource(freeAudioSource, seconds));
                            }
                        }
                    }
                }
            }
        }
    }
}
