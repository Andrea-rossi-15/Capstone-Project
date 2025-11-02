using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SaveAndLoaddata : MonoBehaviour
{
    [SerializeField] List<Scrollbar> _scrollbars = new List<Scrollbar>();
    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] List<string> _mixerParameters;
    [SerializeField] Scrollbar _brightnessScrollbar;
    [SerializeField] Image _brightness;

    void Start()
    {
        if (FindObjectsOfType<SaveAndLoaddata>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        LoadState();
        for (int i = 0; i < _scrollbars.Count; i++)
        {
            var scrollbar = _scrollbars[i];
            if (scrollbar != null)
            {
                int index = i;
                scrollbar.onValueChanged.AddListener(value =>
                {
                    SetMixerVolume(index, value);
                    SaveState();
                });
            }
        }
        if (_brightnessScrollbar != null)
        {
            _brightnessScrollbar.onValueChanged.AddListener(value =>
            {
                ChangeBrightness(value);
                PlayerPrefs.SetFloat("Brightness", value);
                PlayerPrefs.Save();
            });
        }
    }
    void SaveState()
    {
        foreach (var scrollbar in _scrollbars)
        {
            if (scrollbar != null)
            {
                int index = _scrollbars.IndexOf(scrollbar);
                float valueToSave = scrollbar.value;
                PlayerPrefs.SetFloat("Scrollbar_" + index, valueToSave);
            }
        }
        PlayerPrefs.Save();
    }
    void LoadState()
    {
        foreach (var scrollbar in _scrollbars)
        {
            if (scrollbar != null)
            {
                int index = _scrollbars.IndexOf(scrollbar);
                if (PlayerPrefs.HasKey("Scrollbar_" + index))
                {
                    float savedValue = PlayerPrefs.GetFloat("Scrollbar_" + index);
                    scrollbar.value = savedValue;
                    SetMixerVolume(index, savedValue);
                }
            }
        }
        if (_brightnessScrollbar != null)
        {
            if (PlayerPrefs.HasKey("Brightness"))
            {
                float savedBrightness = PlayerPrefs.GetFloat("Brightness");
                _brightnessScrollbar.value = savedBrightness;
                ChangeBrightness(savedBrightness);
            }
            else
            {
                ChangeBrightness(_brightnessScrollbar.value);
            }
        }
    }
    void SetMixerVolume(int _index, float value)
    {
        float _db = Mathf.Log10(Mathf.Clamp(value, 0.001f, 1f)) * 20f;

        if (_index < _mixerParameters.Count)
        {
            _audioMixer.SetFloat(_mixerParameters[_index], _db);
        }
    }
    void ChangeBrightness(float _value)
    {
        Color c = _brightness.color;
        float minAlpha = 0.2f;
        float maxAlpha = 0.8f;
        c.a = Mathf.Clamp(_value, minAlpha, maxAlpha);
        _brightness.color = c;
    }
}
