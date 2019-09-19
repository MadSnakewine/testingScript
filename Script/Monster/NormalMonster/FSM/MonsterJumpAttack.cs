using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterJumpAttack : MonsterFSMState
{
    public float firingAngle = 45.0f;
    public  float gravity =9.8f;

    public float attackAnimStartTime;

    public Transform Projectile;
    private Transform mytransform;

    public float distance;      //현재거리차이

    public override void BeginState()
    {
        //Debug.Log("뛰었다");
        StartCoroutine("JumpAttack");
        _manager.anim.SetInteger("FSM", (int)Monster_State.JumpAttack);

        base.BeginState();
    }

    public override void EndState()
    {
        //Debug.Log("끝났다");
        StopCoroutine("JumpAttack");
        _manager.anim.SetInteger("FSM", (int)Monster_State.Chasing);
        base.EndState();
    }

    void Start()
    {
        mytransform = transform.GetComponent<Transform>();
    }

    void Update()
    {
        distance = Vector3.Distance(_manager.target.position, transform.position);
        if (_manager.target.GetComponentInChildren<PlayerFSM>()._CurrentState == Player_State.UpperCut && distance <= 3.0f)
        {
            _manager.ChScript(Monster_State.Airborne);
        }
        if (distance >= 7.5f)
        {
            _manager.ChScript(Monster_State.Chasing);
            return;
        }
    }

    IEnumerator JumpAttack()
    {
        // 0.5초 후 시전
        //Debug.Log("코루틴");
        
        yield return new WaitForSeconds(attackAnimStartTime);


        // 투사체를 던지는 물체의 위치로 옮기고 필요한 경우 오프셋을 추가
        Projectile.position = mytransform.position + new Vector3(0, 0.0f, 0);

        // 목표까지의 거리 계산
        float target_Distance = Vector3.Distance(Projectile.position, _manager.target.position);

        // 지정된 각도에서 대상에 물체를 던지는 속도를 계산
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // 속도의 X Y 성분 추출
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad) * 5;        

        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad) * 2;

        // 비행시간 계산
        float flightDuration = target_Distance / Vx;
        
        float elapse_time = 0;

        // 몹의 현재 위치 - 타겟의 현재 위치
        float mTop = Projectile.position.x - _manager.target.position.x;

        // (몹의 현재 위치 - 타겟)의 현재 위치의 값이 음수일 경우 x축 양의 방향으로 힘이 가해진다
        while (elapse_time < flightDuration)
        {
            if (mTop < 0)
            {
                Projectile.Translate(Vx * Time.deltaTime, (Vy - (gravity * elapse_time)) * Time.deltaTime, 0);
            }

            else
            {
                Projectile.Translate(-(Vx * Time.deltaTime), (Vy - (gravity * elapse_time)) * Time.deltaTime, 0);
            }

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
}