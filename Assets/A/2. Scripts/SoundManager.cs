using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class BgmAudioClip{
    public string name;
    public AudioClip clip;
}

[System.Serializable]
public class OptionTap
{
    public string name;
    public Image OnImage,OffImage;
    public Sprite OnBack, OffBack;
    public AudioSource audioSource;

    public void On()
    {
        OnImage.sprite = OnBack;
        OffImage.sprite = OffBack;
        if(audioSource != null)
        {
            audioSource.volume = 1;
        }
    }

    public void Off()
    {
        OnImage.sprite = OffBack;
        OffImage.sprite = OnBack;
        if (audioSource != null)
        {
            audioSource.volume = 0;
        }
    }
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public Sprite OnBackground, OffBackground;
    public AudioClip BtnClickClip;
    public AudioSource bgmAudioSource,btnAudioSource;
    public List<BgmAudioClip> BgmAudioClips;
    public List<OptionTap> OptionTaps;

    public void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this); }
    }

    public void Start()
    {
        foreach (var tap in OptionTaps)
        {
            tap.On();
        }
    }

    public void SoundMute(string name)
    {
        OptionTaps.Find(x => x.name == name).Off();  
    }

    public void SoundOn(string name)
    {
        OptionTaps.Find(x => x.name == name).On();
    }

    public void BgmPlay(string name)
    {
        bgmAudioSource.clip = BgmAudioClips.Find(x => x.name == name).clip;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }
    public void ButtonClickSoundPlay()
    {
        btnAudioSource.PlayOneShot(BtnClickClip);
    }

   
}
