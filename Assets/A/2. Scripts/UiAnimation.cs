using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIAnimatorCore;

[System.Serializable]
public class UiAnimators
{
    public string name;
    public UIAnimator animator;
    public GameObject targetGameObject;
}

public class UiAnimation : MonoBehaviour
{
    [SerializeField] List<UiAnimators> UiCanvasAnimators;

    public void PlayOffAnimation(string name)
    {
        if (!SoundManager.instance.btnAudioSource.isPlaying) { SoundManager.instance.ButtonClickSoundPlay(); }
        UIAnimator uiAnim = UiCanvasAnimators.Find(x=> x.name == name).animator;
        uiAnim.PlayAnimation(AnimSetupType.Outro);
        //UIAnimator의 OutroSetting에서 OnFinished에 GameObject.SetActive(false)를 넣어둠

    }

    public void PlayOnAnimation(string name)
    {
        if (!SoundManager.instance.btnAudioSource.isPlaying) { SoundManager.instance.ButtonClickSoundPlay(); }
        UiAnimators uiAnims = UiCanvasAnimators.Find(x => x.name == name);
        uiAnims.targetGameObject.SetActive(true);
        uiAnims.animator.PlayAnimation(AnimSetupType.Intro);

    }
}
