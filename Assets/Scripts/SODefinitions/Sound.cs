using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "mySound", menuName = "Scriptable Objects/Sound")]
    public class Sound : AudioEvent
    {
        [SerializeField] private AudioClip[] _clips;
        [Range(-0.25f, 0.25f)] [SerializeField] private float _volume;

        public override float Play(AudioSource source, float volume)
        {
            if (_clips.Length == 0) return 0.0f;
            source.clip = _clips[Random.Range(0, _clips.Length)];
            source.volume = volume + _volume;
            source.Play();
            return source.clip.length;
        }
    }
}