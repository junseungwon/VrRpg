using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIAnimatorCore;

public class TestMove : MonoBehaviour
{
    GameObject Camera;
    public GameObject inventory;
    public float moveSpeed = 5.0f;
    public float rotateSpeedX = 3.0f;
    public float rotateSpeedY = 5.0f;
    public UIAnimator InventoryUiAnim;
    float h, v, eulerX, eulerY;
    private bool isAnimPlay;
    private void Start()
    {
        Camera = GameObject.Find("Main Camera");
        isAnimPlay = false;
    }
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Vector3 moveVec = new Vector3(h, 0, v);

        if (Input.GetKey(KeyCode.G))
        {
            eulerY += Input.GetAxis("Mouse X") * rotateSpeedX;
            eulerX -= Input.GetAxis("Mouse Y") * rotateSpeedY;

            Camera.transform.rotation = Quaternion.Euler(eulerX, eulerY, 0);
            transform.rotation = Quaternion.Euler(0, eulerY, 0);
        }
        transform.Translate(moveVec * Time.deltaTime * moveSpeed, Space.Self);

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isAnimPlay)
            {
                isAnimPlay = true;
                if (inventory.activeSelf)
                {

                    InventoryUiAnim.PlayAnimation(AnimSetupType.Outro);
                    Invoke("OffInventoryCanvas",1.0f);
                }
                else
                {
                    inventory.SetActive(true);
                    InventoryUiAnim.PlayAnimation(AnimSetupType.Intro);
                }
                Invoke("ChangeIsAnimPlayBool", 1.0f); 
            }
        }
    }
    private void ChangeIsAnimPlayBool() => isAnimPlay = false;
    private void OffInventoryCanvas() => inventory.SetActive(false);

}
