using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public Text InputText, IdText, PasswordText;
    public GameObject AlarmExitGameCanvasGameObject, AlarmSelectNicknameCanvasGameObject, AlarmRightNicknameCanvasGameObject, AlarmCanUseNicknameCanvasGameObject, AlarmSameNicknameCanvasGameObject, NicknameCanvasGameObject, LoginCanvasGameObject;

    public UiAnimation uiAnimation;

    private bool sameNicknameCheck;
    private string samecheckNickname = "";

    public void Start()
    {
        SoundManager.instance.BgmPlay("Login");
        if (PlayerPrefs.HasKey("ID"))
        {
            IdText.text = PlayerPrefs.GetString("ID");
            PasswordText.text = PlayerPrefs.GetString("PASSWORD");
        }
        
    }
    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (useDB.instance.getId() == null)
            {
                IdText.text = "ADMIN";
                PasswordText.text = "1234";
                LoginBtn();
            }
            else
            {
                int a = Random.Range(0,100);
                int b = Random.Range(0,100);
                int c = Random.Range(0,100);
                InputText.text = string.Format("TEMPNAME{0}{1}{2}",a,b,c);
                if (!useDB.instance.CanUseNickname(InputText.text)) { return; }
                sameNicknameCheck = true;
                samecheckNickname = InputText.text;
                ChoiceNickname();
            }
        }

    }
    public void ChoiceCharacterButton()
    {
        string nickname = InputText.text;
        if (nickname == "") { return; }
        if (!sameNicknameCheck || nickname != samecheckNickname) { SameNicknameButton(); return; } //'중복체크' 안했으면 알람띄우기
        RightNicknameButton();

    }
    public void SetSameNicknameCheck(bool value) { sameNicknameCheck = value; }
    public void SetNickname() { samecheckNickname = InputText.text; }
    public void SameNicknameButton()
    {
        string nickname = InputText.text;

        //닉네임이 사용가능하면
        if (useDB.instance.CanUseNickname(nickname))
        {
            uiAnimation.PlayOnAnimation("CanUse");
        }
        else
        {
            uiAnimation.PlayOnAnimation("Same");
        }
    }

    public void RightNicknameButton()
    {
        uiAnimation.PlayOnAnimation("Right");
    }
    public void ChoiceNickname()
    {
        useDB.instance.setNickName(InputText.text);
        GameManager.instance.ChangeScene("StartScene_R");
    }
    public void ExitButton()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    public void LoginBtn()
    {
        string id = IdText.text;
        string pass = PasswordText.text;

        //빈공간 있으면 작동 안함
        if (id == "" || pass == "") { Debug.Log("빈칸 있음"); return; }

        if (useDB.instance.Login(id,pass))
        {
            PlayerPrefs.SetString("PASSWORD",pass);
            uiAnimation.PlayOnAnimation("Nickname");
            Invoke("LoginGameObjectOff", 0.5f);
            
        }
    }

    public void LoginGameObjectOff()
    {
        uiAnimation.PlayOffAnimation("Login");
    }

}
