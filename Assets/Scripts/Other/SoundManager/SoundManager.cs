using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SoundType
{
    FOOTSTEPS,
    RIFLEBULLET,
    SHOOTUGUNBULLET,
    RELOAD,
    HIT,
    ENEMYDIE,
    HEALING,
    TAKEBULLET,
    ONPLAYERDEATHUI,
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] _soundList;
    private static SoundManager _istance;
    private AudioSource _audioSource;


    private void Awake()
    {
        _istance = this;
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType _sound, float _volume = 1)
    {
        _istance._audioSource.PlayOneShot(_istance._soundList[(int)_sound], _volume);
    }
}
