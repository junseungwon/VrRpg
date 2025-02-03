using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UIAnimatorCore;

public class VRKeyboard : MonoBehaviour
{
    public UIAnimator KeyboardUiAnimator;
    public GameObject Keyboard;
    public Text targetTextName;
    private Text targetText;
    public void KeyboardOnOff(string _targetTextName)
    {

        //다시한번 누르면
        if (targetTextName.text == _targetTextName && !KeyboardUiAnimator.IsPlaying)
        {
            if (Keyboard.activeSelf)
            {
                KeyboardUiAnimator.PlayAnimation(AnimSetupType.Outro);
            }
            else
            {
                Keyboard.SetActive(true);
                KeyboardUiAnimator.PlayAnimation(AnimSetupType.Intro);
            }
        }
        GameObject pressedGameObject = EventSystem.current.currentSelectedGameObject;
        targetText = pressedGameObject.GetComponentInChildren<Text>();
        targetTextName.text = _targetTextName;

    }

    public void InputKey()
    {
        //입력값은 버튼의 자식 TEXT에 있는 string 값
        string s = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text.Trim();
        s = s.Length == 1 ? s : s.ToUpper();

        //들어온 입력을 텍스트에 적용
        switch (s)
        {
            case "ENTER":
                KeyboardUiAnimator.PlayAnimation(AnimSetupType.Outro);
                break;

            //TEXT에 입력된 문자 하나 지우기
            case "BACK":
                if (targetText.text.Length == 0) { break; }
                targetText.text = targetText.text.Substring(0, targetText.text.Length - 1);
                break;

            //키보드 캔버스의 게임오브젝트 끄기
            case "ESC":
                KeyboardUiAnimator.PlayAnimation(AnimSetupType.Outro);
                break;

            //TEXT에 문자 추가하기
            default:
                targetText.text += s;
                break;
        }
    }
}
