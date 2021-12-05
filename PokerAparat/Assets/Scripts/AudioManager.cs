using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public AudioSource MainAudioSource;
    public AudioClip ButtonClick;
    public AudioClip CardFlip;
    public Sprite AudioOn, AudioOff;

    public static AudioManager Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }

    public void PlayMainAudioSourceClip(AudioClip clip)
    {
        if (PrefsManager.Sound)
            MainAudioSource.PlayOneShot(clip);
    }


}
