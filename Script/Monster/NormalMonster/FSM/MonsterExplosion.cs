using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterExplosion : MonsterFSMState
{

    public float distance;

    bool explosion;

    void Start()
    {
        _manager.player = GameObject.Find("Player");

        _manager.target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override void BeginState()
    {
        StartCoroutine("Explosion");
        _manager.anim.SetInteger("FSM", (int)Monster_State.Explosion);
        //코루틴 시작
        //작업 중에는 잠시 정지        

        base.BeginState();
    }

    public override void EndState()
    {
        base.EndState();
    }

    void Update()
    {
        distance = Vector3.Distance(_manager.target.position, transform.position);
        if (distance >= 3.5f)
        {
            _manager.ChScript(Monster_State.Chasing);
            StopCoroutine("Explosion");
        }

        //사망
        if (gameObject.GetComponent<HP>().hp <= 0)
        {
            StopCoroutine("Explosion");
            _manager.ChScript(Monster_State.Dead);
        }
    }
    IEnumerator Explosion()
    {
        //폭발 후 파괴
        yield return new WaitForSeconds(1.9f);
        Destroy(gameObject);
    }
}
