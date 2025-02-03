using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class ClassMonster : MonoBehaviour
{
    private Transform player;
    public Text txtHp;
    [SerializeField]
    private Image imgDownHp;
    [SerializeField]
    private Text bloodHp;
    private int hp = 100;
    private NavMeshAgent navMeshAgent;
    private bool isAnim = false;
    private Animation monsterAnim;
    public int index = 0;
    List<string> animArray;
    public int MonsterHp { get { return hp; } set { hp = value; } }
    // Start is called before the first frame update
    void Start()
    {
        monsterAnim = GetComponent<Animation>();
        animArray = new List<string>();
        AnimationArray();
        player = VrGameManger.instance.player.transform;
        Debug.Log(player);
        navMeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(MonsterPattern());
    }
    // Update is called once per frame
    void Update()
    {
        StopIdle();
    }
    private void OnTriggerEnter(Collider other)
    {
        KnifeAttack(other);
    }
    private void AnimationArray()
    {
        foreach (AnimationState state in monsterAnim)
        {
            animArray.Add(state.name);
            index++;
        }
    }
    IEnumerator MonsterPattern()
    {
        WaitForSeconds time = new WaitForSeconds(5.0f);
        while (true)
        {
            MonsterMoving();
            yield return time;
        }
    }
    public void StopIdle()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < 3.2 && isAnim == false)
        {
            monsterAnim.Play(animArray[0]);
        }
    }
    public void MonsterMoving()
    {
        //플레이어와 몬스터와의 거리를 계산
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <3.2)
        {
            if (isAnim == false)
            {
                StartCoroutine(CorutineAttack());
            }
        }
        else
        {
            if (isAnim == false)
            {
                monsterAnim.Play(animArray[4]);
                navMeshAgent.SetDestination(player.position);
            }
        }
    }
    IEnumerator CorutineAttack()
    {
        yield return new WaitForSeconds(0.5f);
        isAnim = true;
        MonsterAttack();
        PlayerInformManager.instance.DownPlayerHp(10);
        yield return new WaitForSeconds(2f);
        monsterAnim.Play(animArray[0]);
        isAnim = false;
    }
    private void MonsterAttack()
    {
        monsterAnim.Play(animArray[1]);
    }
    public void KnifeAttack(Collider other)
    {
        if (other.tag == "Blade")
        {
            Destroy(other);
            hp -= PlayerInformManager.instance.KnifeDamage;
            txtHp.text = hp+"/"+100;
            imgDownHp.fillAmount -= 0.01f*PlayerInformManager.instance.KnifeDamage;
            StartCoroutine(DownHp());
            if (hp <= 0)
            {
                MonsterDie();
                PlayerInformManager.instance.PlusMoney(50000);
            }
            Destroy(other);
        }
    }
    private void MonsterDie()
    {
        VrGameManger.instance.UpScore(100);
        Destroy(gameObject);
    }
    IEnumerator DownHp()
    {
        bloodHp.gameObject.SetActive(true);
        bloodHp.text = (-0.1f * PlayerInformManager.instance.KnifeDamage).ToString();
        yield return new WaitForSeconds(0.2f);
        bloodHp.gameObject.SetActive(false);
    }
}
