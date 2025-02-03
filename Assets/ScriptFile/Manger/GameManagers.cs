using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManagers : MonoBehaviour
{
    public static GameManagers instance = null;
    public Transform followPosition = null; //보류 플레이어가 >> 이동하는 박스
    public GameObject player = null;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
      
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
