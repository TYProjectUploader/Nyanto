using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider BgMusicSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start()
    {
        //load previous settings if available
        if (PlayerPrefs.HasKey("BgMusicVolume"))
        {
            LoadSavedVolume();
        }
        SetBgMusicVolume();
        SetSFXVolume();

    }

    public void SetBgMusicVolume()
    {
        float volume = BgMusicSlider.value;
        //volume slider is linear while actual value is logarithmic due to being in dB
        //with this logic min value of slider has to be above 0 to not cause error
        audioMixer.SetFloat("BgMusic", MathF.Log10(volume)*20);
        PlayerPrefs.SetFloat("BgMusicVolume", volume);
    }
    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        audioMixer.SetFloat("SFX", MathF.Log10(volume)*20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    private void LoadSavedVolume()
    {
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1);
        BgMusicSlider.value = PlayerPrefs.GetFloat("BgMusicVolume", 1);
    }
}
