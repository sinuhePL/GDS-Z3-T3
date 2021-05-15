using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField]
    private BoolReference _isSoundOn;
    [SerializeField]
    private BoolReference _isMusicOn;
    [SerializeField]
    private FloatReference _soundVolume;
    [SerializeField]
    private FloatReference _musicVolume;
    [SerializeField]
    private Toggle _soundToggle;
    [SerializeField]
    private Toggle _musicToggle;
    [SerializeField]
    private Slider _soundVolumeSlider;
    [SerializeField]
    private Slider _musicVolumeSlider;

    private void Start()
    {
        _soundToggle.isOn = _isSoundOn.Value;
        _musicToggle.isOn = _isMusicOn.Value;
        _soundVolumeSlider.value = _soundVolume.Value;
        _musicVolumeSlider.value = _musicVolume.Value;
    }

    public void ToggleSound(bool isOn)
    {
        _isSoundOn.Value = isOn;
    }

    public void ToggleMusic(bool isOn)
    {
        _isMusicOn.Value = isOn;
    }

    public void ChangeSoundVolume(float volume)
    {
        _soundVolume.Value = volume;
    }

    public void ChangeMusicVolume(float volume)
    {
        _musicVolume.Value = volume;
    }
}
