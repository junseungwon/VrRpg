using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public Image BGMOn, BGMOff, BTNOn, BTNOff, VibeOn, VibeOff;

    void Start()
    {
        SoundManager.instance.OptionTaps[0].OnImage = BGMOn;
        SoundManager.instance.OptionTaps[0].OffImage = BGMOff;
        SoundManager.instance.OptionTaps[1].OnImage = BTNOn;
        SoundManager.instance.OptionTaps[1].OffImage = BTNOff;
        SoundManager.instance.OptionTaps[2].OnImage = VibeOn;
        SoundManager.instance.OptionTaps[2].OffImage = VibeOff;

        SoundManager.instance.BgmPlay("Start");
        foreach (var tap in SoundManager.instance.OptionTaps)
        {
            tap.On();
        }
    }

    public void SoundOn(string name) 
    {
        SoundManager.instance.SoundOn(name);
    }
    public void SoundMute(string name) 
    {
        SoundManager.instance.SoundMute(name);
    }

}
