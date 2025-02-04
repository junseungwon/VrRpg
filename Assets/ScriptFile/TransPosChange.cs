using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransPosChange : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(PosChange());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator PosChange()
    {
        Vector3 pos = Camera.main.transform.position;
        yield return new WaitForSeconds(2.0f);
        Camera.main.transform.position = new Vector3(transform.position.x, 5f, transform.position.z);
        transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(10f);
        transform.GetChild(1).gameObject.SetActive(true);
        Camera.main.transform.position = pos;
    }
}
