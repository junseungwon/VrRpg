using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstancePlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject model;
    // Start is called before the first frame update
    void Start()
    {
        GameObject modelObject = Instantiate(model);
        modelObject.transform.SetParent(GameManagers.instance.player.transform);
        modelObject.transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
