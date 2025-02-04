using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    [SerializeField]
    private Transform spawnParent;
    [SerializeField]
    private Transform[] spawnPos = new Transform[3];
    private WaitForSeconds spawnSecond = new WaitForSeconds(3f);
    [SerializeField]
    public GameObject monster;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CorutineMonsterSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CorutineMonsterSpawn()
    {
        yield return new WaitForSeconds(10f);
        while (true)
        {
            if (spawnParent.childCount < 3)
            {
                int pos = Random.Range(0, 3);
                GameObject monsterPos =Instantiate(monster);
                monsterPos.transform.position = spawnPos[pos].position;
                monsterPos.transform.SetParent(spawnParent);
            }
            yield return spawnSecond;
        }
    }
}
