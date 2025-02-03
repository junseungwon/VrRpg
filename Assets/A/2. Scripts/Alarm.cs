using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alarm : MonoBehaviour
{
    public Shop shop;
    public void Logout()
    {
        GameManager.instance.ChangeScene("LoginScene_R");
        useDB.instance.setId(null);

    }
    public void Save()
    {
        PlayerPrefs.SetString("ID",useDB.instance.getId());

    }
    public void NotSave()
    {
        PlayerPrefs.SetString("ID", "");
        PlayerPrefs.SetString("PASSWORD", "");
    }
    
    public void SaveGame()
    {

    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else 
            Application.Quit();
        #endif
    }

    public void Buy()
    {
        
    }

    public void Sell()
    {

    }
}
