using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VRKeyboards : MonoBehaviour
{
    public GameObject Keyboard;
    public Text obj;
    private Text targetText;
    private void Start()
    {
        KeyboardOnOff();
    }
    public void KeyboardOnOff()
    {
       // GameObject pressedGameObject = obj;
        //Keyboard.SetActive(!Keyboard.activeSelf);
        targetText = obj; 
            //pressedGameObject.GetComponentsInChildren<Text>()[0];

    }
    public void InputKey()
    {
        //입력값은 버튼의 자식 TEXT에 있는 string 값
        string s = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text.Trim();
        s = s.Length == 1 ? s : s.ToUpper();
        Debug.Log(s);
        //들어온 입력을 텍스트에 적용
        switch (s)
        {
            case "ENTER":
                KeyboardOnOff();
                break;

            //TEXT에 입력된 문자 하나 지우기
            case "BACK":
                if (targetText.text.Length == 0) { break; }
                targetText.text = targetText.text.Substring(0, targetText.text.Length - 1);
                break;

            //키보드 캔버스의 게임오브젝트 끄기
            case "ESC":
                KeyboardOnOff();
                break;

            //TEXT에 문자 추가하기
            default:
                Debug.Log("추가됨");
                targetText.text += s;
                break;
        }
    }
}
