using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class BoseMonster : MonoBehaviour
{
    public static BoseMonster instance = null;
    public GameObject[] col = new GameObject[5];
    public GameObject[] vol = new GameObject[5];
    public GameObject playerParent;
    public GameObject player;
    public GameObject monster_normalBlade;
    public GameObject judgeArea;
    public Text txtHp;
    public Image imgDownHp;
    //몬스터 체력, 공격력, 방어력,
    private int monsterHp = 200;
    private int monsterDamge = 100;
    private int defense = 100;
    private int randomSkill;
    private Animator bossAnim;
    private Action[] Monster_Array_Skill = new Action[2];
    public GameObject wintext;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }
    void Start()
    {
        txtHp.text = monsterHp + "/" + 200;
        bossAnim = GetComponent<Animator>();
        Monster_Array_Skill[0] = MonsterFloorSmash;
        Monster_Array_Skill[1] = MonsterVolcanoShout;
        MonsterBasicPatternAttack();

    }
    void Update()
    {
        FollowTarget();
    }
    private void OnTriggerEnter(Collider other)
    {
        KnifeAttack(other);
    }
    public void KnifeAttack(Collider other)
    {
        if (other.tag == "Blade")
        {
            monsterHp -= PlayerInformManager.instance.KnifeDamage;
            txtHp.text = monsterHp + "/" + 200;
            imgDownHp.fillAmount -= 0.005f * PlayerInformManager.instance.KnifeDamage;
            if (monsterHp <= 0) 
            {
                PlayerInformManager.instance.Win();
            }
            Destroy(other.gameObject);
        }
    }
    private void FollowTarget()
    {
        if (player != null)
        {
            Vector3 dir = player.transform.position - this.transform.position;
            dir.y = 0;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3f);
        }
    }
    private void PlayAnim(string anim, bool result)
    {
        bossAnim.SetBool(anim, result);
    }

    //소리치기 공격, 땅던지기, 주변온도 변화, 불덩이 던지기 >> 기본공격이 발생하고 일정시간이 되면 해당 스킬이 발사
    //5초마다 기본공격을 하고 15초마다 스킬을 사용
    //기본 공격이 몬스터도 플레이어를 향해 블레이드를 날림 2
    //소리치기 주변으로 에너지 공기 원이 날라감 6
    //주변온도 변화
    //불덩이 던지기 화염운석이 생성되고 가운데 위치가 되면 플레이어에게 날라가도록 4
    //땅을 던지는 느낌 5
    private void MonsterBasicPatternAttack()
    {
        StartCoroutine(CorutineMonsterPattern());
    }
    IEnumerator CorutineMonsterPattern()
    {
        WaitForSeconds recycleTime = new WaitForSeconds(10f);
        WaitForSeconds truningTime = new WaitForSeconds(0.01f);
        WaitForSeconds patternRecycleTime = new WaitForSeconds(3.0f);
        while (true)
        {
            ShowJudgeArea(true);
            player.transform.position = playerParent.transform.position;
            Vector3 v3= player.transform.position;
            v3.y=-51;
            judgeArea.transform.LookAt(v3);
            yield return patternRecycleTime;
            MonsterSkillAttack();
            yield return recycleTime;
        }
    }
    private void MonsterSkillAttack()
    {
        randomSkill = UnityEngine.Random.Range(0, Monster_Array_Skill.Length);
        Monster_Array_Skill[randomSkill].Invoke();
    }
    private void MonsterFloorSmash()
    {
        StartCoroutine(CorutineFloorSmash());
    }
    private void MonsterVolcanoShout()
    {
        StartCoroutine(CorutineVolcanoShout());
    }
    IEnumerator CorutineFloorSmash()
    {
        Debug.Log("스메쉬 공격");
        SmashAnimation(true);
        yield return new WaitForSeconds(3.5f);
        OnColider(true);
        SmashAnimation(false);
        yield return new WaitForSeconds(2.0f);
        ShowJudgeArea(false);
        OnColider(false);
    }
    IEnumerator CorutineVolcanoShout()
    {
        BoxReset(col);
        yield return new WaitForSeconds(2.0f);
        Debug.Log("볼케이노 공격");
        ShoutVolcanoAnimation(true);
        BoxReset(vol);
        yield return new WaitForSeconds(3f);
        OnColider(true);
        ShoutVolcanoAnimation(false);
        yield return new WaitForSeconds(2.0f);
        ShowJudgeArea(false);
        BoxSetActiveFalse(col);
        OnColider(false);
    }
    private void ShowJudgeArea(bool result)
    {
        judgeArea.SetActive(result);
    }
    private void OnColider(bool result)
    {
        for (int i = 0; i < judgeArea.transform.childCount; i++)
        {
            judgeArea.transform.GetChild(i).GetComponent<BoxCollider>().enabled = result;
        }
    }
    private void BoxSetActiveFalse(GameObject[] obj)
    {
        for (int i = 0; i < obj.Length; i++)
        {
            if (obj[i].activeSelf == true)
            {
                obj[i].SetActive(false);
            }

        }
    }
    private void BoxReset(GameObject[] obj)
    {
        for (int i = 0; i < obj.Length; i++)
        {
            if (obj[i].activeSelf == true)
            {
                obj[i].SetActive(false);
            }
            obj[i].SetActive(true);
        }
    }
    private void ShoutVolcanoAnimation(bool value)
    {
        PlayAnim("ShoutVolcano", value);
    }
    private void SmashAnimation(bool value)
    {
        PlayAnim("FloorSmash", value);
    }
}
