using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    public static FireSpawner instance;
    public GameObject originFire;
    [SerializeField]
    private GameObject spawnFire;
    private Queue<GameObject> que_spawnFire = new Queue<GameObject>();
    private Vector3 finalOriginFireScale= new Vector3(2,2,2);
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject obj = Instantiate(spawnFire, Vector3.zero, Quaternion.identity);
            que_spawnFire.Enqueue(obj);
            obj.SetActive(false);
        }
        StartCoroutine(OriginFire());
        StartCoroutine(RandomSpawn());
    }
    private IEnumerator RandomSpawn()
    {
        while (true)
        {
            GameObject spawnObj= OutPutChildFire();
            int spawnPosx = Random.Range(-10, 10);
            int spawnPosy = Random.Range(-10, 10);
            int spawnPosz = Random.Range(-10, 10);
            spawnObj.transform.position = new Vector3(transform.position.x+spawnPosx, transform.position.y + spawnPosy, transform.position.z + spawnPosz);
            yield return new WaitForSeconds(0.2f);
        }
    }
    private IEnumerator OriginFire()
    {
        yield return new WaitForSeconds(1.5f);
        float timer = 0f;
        while (originFire.transform.localScale.x < finalOriginFireScale.x)
        {
            OriginFirePlay(timer);
            timer += 0.01f;
            yield return new WaitForSeconds(1f);
        }
    }
    private void OriginFirePlay(float timer)
    {
        originFire.transform.localScale = Vector3.Lerp(originFire.transform.localScale, finalOriginFireScale, timer);
    }
    public void InsertChildFire(GameObject child)
    {
        que_spawnFire.Enqueue(child);
        child.SetActive(false);
    }
    public GameObject OutPutChildFire()
    {
         GameObject child = que_spawnFire.Dequeue();
         child.SetActive(true);
        return child;
    }
}
