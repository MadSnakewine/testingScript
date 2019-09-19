using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFromBossSummon : MonoBehaviour
{
    public bool summon;             // 소환
    public float summonCoolTime;    // 소환쿨타임    

    //[HideInInspector] public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        //  anim = transform.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine("Summon");
    }
    IEnumerator Summon()
    {
        while (true)
        {
            Debug.Log("코루틴타이머 시작");
            yield return new WaitForSeconds(summonCoolTime);
            Debug.Log("종료");
            summon = true;
            if(summon == true)
            {
                summon = false;
            }
        }
    }
}
