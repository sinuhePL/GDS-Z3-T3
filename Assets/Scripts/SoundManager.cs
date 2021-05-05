using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        click,
        exit
    }

    public class MyAudioSource
    {
        private AudioSource _soundSource;
        public bool _isAvailable;

        MyAudioSource()
        {
            _isAvailable = true;
            GameObject soundGameObject = new GameObject("Sound");
            _soundSource = soundGameObject.AddComponent<AudioSource>();
            _soundSource.loop = false;
        }

        public void PlaySound(AudioClip clip, Vector3 position, float volume)
        {
            _soundSource.transform.position = position;
            _soundSource.clip = clip;
            _soundSource.volume = volume;
            _soundSource.Play();
        }

    }

    private const int _poolSize = 6;
    private static MyAudioSource[] _audioPool;
    private static AudioSource _oneShotAudioSource;
    public static bool _isOn;
    public static float _soundVolume;

    private static MyAudioSource GetMyAudioSource()
    {
        foreach(MyAudioSource mySource in _audioPool)
        {
            if(mySource._isAvailable)
            {
                mySource._isAvailable = false;
                return mySource;
            }
        }
        Debug.LogError("Sound pool depleted!");
        return null;
    }

    private static AudioClip GetSoundClip(Sound sound)
    {
        foreach (GameAssets.SoundClip soundClip in GameAssets._instance._soundArray)
        {
            if (soundClip._sound == sound)
            {
                return soundClip._soundClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }

    private static IEnumerator ReleaseAudioSource(MyAudioSource mySource, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        mySource._isAvailable = true;
    }

    public static void Initialize()
    {

        GameObject oneShotObject = new GameObject("One Shot Sound");
        _oneShotAudioSource = oneShotObject.AddComponent<AudioSource>();
        _oneShotAudioSource.loop = false;
        _isOn = true;
        _soundVolume = 0.5f;
        _audioPool = new MyAudioSource[_poolSize];
    }

    public static void PlaySound(Sound sound)
    {
        if (_isOn)
        {
            _oneShotAudioSource.volume = _soundVolume;
            _oneShotAudioSource.PlayOneShot(GetSoundClip(sound));
        }
    }

    public static void PlaySound(Sound sound, Vector3 position)
    {
        if (_isOn)
        {
            MyAudioSource audioSource;
            audioSource = GetMyAudioSource();
            if (audioSource != null)
            {
                audioSource.PlaySound(GetSoundClip(sound), position, _soundVolume);
            }
        }
    }
}
