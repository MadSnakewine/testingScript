using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChasing : MonsterFSMState
{

    public float speed;     //추적속도

    public float stoppingDistance;      //추적정지거리

    public float distance;      //현재거리차이

    public override void BeginState()
    {
        _manager.anim.SetInteger("FSM", (int)Monster_State.Chasing);
        base.BeginState();
    }

    public override void EndState()
    {
        _manager.anim.SetInteger("FSM", (int)Monster_State.Idle);
        base.EndState();
    }

    void Start()
    {
        //추적대상과의 거리
        _manager.target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _manager.player = GameObject.Find("Player");
    }

    void Update()
    {
        distance = Vector3.Distance(_manager.target.position, transform.position);
        if (_manager.target.GetComponentInChildren<PlayerFSM>()._CurrentState == Player_State.UpperCut && distance <= 3.0f)
        {
            _manager.ChScript(Monster_State.Airborne);
        }
        //해당 오브젝트의 이름이 Baneling일 때
        if (gameObject.name.Contains("Cutie"))
        {
            //추적대상이 거리 안에 있을 시~
            if (distance <= 3.5f)
            {
                //자폭
                _manager.ChScript(Monster_State.Explosion);
                return;
            }
            //없을 시 추적 이동
            //좌우반전
            if (_manager.target.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(_manager.target.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
            }
            else if (_manager.target.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(_manager.target.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
            }
        }
        //해당 오브젝트의 이름이 Cricket일 때
        else if (gameObject.name.Contains("BBojjak"))
        {
            //없을 시 추적 이동
            //좌우반전
            if (_manager.target.transform.position.x < transform.position.x)
            {
                //추적대상이 거리 안에 있을 시~
                if (distance <= 7.0f)
                {
                    //꼽등이공격!
                    _manager.ChScript(Monster_State.JumpAttack);
                    return;
                }
                transform.localScale = new Vector3(1, 1, 1);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(_manager.target.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);

            }
            else if (_manager.target.transform.position.x > transform.position.x)
            {
                //추적대상이 거리 안에 있을 시~
                if (distance <= 7.0f)
                {
                    //꼽등이공격!
                    _manager.ChScript(Monster_State.JumpAttack);
                    return;
                }
                transform.localScale = new Vector3(-1, 1, 1);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(_manager.target.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);

            }
        }
        /*
        if (!_manager.groundCheck&&_manager.GetComponent<MonsterFSM>()._CurrentState == Monster_State.Airborne)
        {
            _manager.mRb2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            _manager.mRb2d.constraints = RigidbodyConstraints2D.None;
        }
        */

        //맞은 공격이 일반공격일 때 Hit 실행

        if (_manager.target.GetComponentInChildren<PlayerFSM>()._CurrentState == Player_State.Attack)
        {
            _manager.ChScript(Monster_State.Hit);
        }


        //맞은 공격이 어퍼컷일 때 몬스터에어본을 실행
        /*
        if (player.GetComponentInChildren<PlayerFSM>()._CurrentState == Player_State.UpperCut && player.GetComponentInChildren<AttackCollider>())
        {
            _manager.ChScript(Monster_State.Airborne);
        }
        */
    }
}
