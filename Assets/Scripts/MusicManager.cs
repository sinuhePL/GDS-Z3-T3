using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class MusicManager
{
    public enum Music
    {
        Menu,
        Game
    }

    private static AudioSource _musicAudioSource1;
    private static AudioSource _musicAudioSource2;
    private static bool _firstMusicSourceIsPlaying;
    public static bool _isOn;
    public static float _musicVolume;

    private static IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        if (!activeSource.isPlaying) activeSource.Play();
        float t = 0.0f;
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = _musicVolume * (1 - (t / transitionTime));
            yield return null;
        }
        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = _musicVolume * (t / transitionTime);
            yield return null;
        }
    }

    private static IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float transitionTime)
    {
        float t = 0.0f;
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            original.volume = _musicVolume * (1 - (t / transitionTime));
            newSource.volume = _musicVolume * (t / transitionTime);
            yield return null;
        }
        original.Stop();
    }

    private static AudioClip GetMusicClip(Music music)
    {
        foreach(GameAssets.MusicType musicType in GameAssets._instance._musicArray)
        {
            if(musicType._musicTypeDescription == music)
            {
                int r = UnityEngine.Random.Range(0, musicType._musicClips.Length);
                return musicType._musicClips[r];
            }
        }
        Debug.LogError("Music " + music + " not found!");
        return null;
    }

    private static IEnumerator UpdateMusic( Music myParameter, float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayMusic(myParameter);
    }

    public static void Initialize()
    {
        GameObject musicObject1 = new GameObject("Music 1 Source");
        _musicAudioSource1 = musicObject1.AddComponent<AudioSource>();
        GameObject musicObject2 = new GameObject("Music 2 Source");
        _musicAudioSource2 = musicObject2.AddComponent<AudioSource>();
        _musicAudioSource1.loop = false;
        _musicAudioSource2.loop = false;
        _isOn = true;
        _musicVolume = 0.5f;
        _firstMusicSourceIsPlaying = false;
    }

    public static void PlayMusic(Music musicType)
    {
        if (_isOn)
        {
            AudioSource activeSource = (_firstMusicSourceIsPlaying) ? _musicAudioSource1 : _musicAudioSource2;
            activeSource.clip = GetMusicClip(musicType);
            activeSource.volume = _musicVolume;
            activeSource.Play();
            GameAssets._instance.StartCoroutine(UpdateMusic(musicType, activeSource.clip.length));
        }
    }

    // return clip length in seconds
    public static void PlayMusicWithFade(Music musicType, float transitionTime = 1.0f)
    {
        if (_isOn)
        {
            AudioSource activeSource = (_firstMusicSourceIsPlaying) ? _musicAudioSource1 : _musicAudioSource2;
            AudioClip newClip = GetMusicClip(musicType);
            GameAssets._instance.StartCoroutine(UpdateMusicWithFade(activeSource, newClip, transitionTime));
        }
    }

    // return clip length in seconds
    public static void PlayMusicWithCrossFade(Music musicType, float transitionTime = 1.0f)
    {
        if (_isOn)
        {
            AudioSource activeSource = (_firstMusicSourceIsPlaying) ? _musicAudioSource1 : _musicAudioSource2;
            AudioSource newSource = (_firstMusicSourceIsPlaying) ? _musicAudioSource2 : _musicAudioSource1;
            activeSource.loop = false;
            _firstMusicSourceIsPlaying = !_firstMusicSourceIsPlaying;
            newSource.clip = GetMusicClip(musicType);
            newSource.Play();
            GameAssets._instance.StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTime));
        }
    }
}
