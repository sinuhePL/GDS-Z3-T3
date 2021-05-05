using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    [System.Serializable]
    public class MusicType
    {
        public MusicManager.Music _musicTypeDescription;
        public AudioClip[] _musicClips;
    }

    [System.Serializable]
    public class SoundClip
    {
        public SoundManager.Sound _sound;
        public AudioClip _soundClip;
    }

    public static GameAssets _instance;
    public MusicType[] _musicArray;
    public SoundClip[] _soundArray;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(this);
        }
    }
}
