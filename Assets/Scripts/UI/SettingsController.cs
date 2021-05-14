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

    private void Start()
    {
        _soundToggle.isOn = _isSoundOn.Value;
        _musicToggle.isOn = _isMusicOn.Value;
    }

    public void ToggleSound(bool isOn)
    {
        _isSoundOn.Value = isOn;
    }

    public void ToggleMusic(bool isOn)
    {
        _isMusicOn.Value = isOn;
    }
}
