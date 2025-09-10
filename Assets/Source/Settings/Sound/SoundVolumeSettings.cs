using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundVolumeSettings : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _mixerKey;
    
    private PlayerConfigData _config;

    private void Start()
    {
        _config = PlayerConfig.Instance.ConfigData;
        _mixer.GetFloat(_mixerKey + "Volume", out _config.masterVolume);
    }

    public void ChangeSoundVolume(float value)
    {
        if (value == 0)
        {
            _mixer.SetFloat("MasterVolume", -80);
        }
        else
        {
            _mixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
        }
    }
}
