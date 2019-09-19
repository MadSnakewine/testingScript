using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAnimEvent : MonoBehaviour
{
    public AttackManage attackMG;
    public PlayerAttack attack;
    public PlayerUpperCut upperCut;
    public PlayerFSM fsm;
    PlayerState state;

    float force;
    bool moveStart;

    public void Start()
    {
        state = transform.GetChild(0).GetComponent<PlayerState>();
        moveStart = false;
    }

    private void Update()
    {
        if (moveStart)
        {
            if(fsm._CurrentState == Player_State.Damaged)
            {
                force = 0;
            }

            if (this.transform.rotation.y == 0 && HelpEvent.GetInstance().MoveCorrection(Vector2.right, this.transform.position, 0.6f))
            {
                this.transform.position += Vector3.right * force * Time.deltaTime;
            }
            else if (this.transform.rotation.y == 1 && HelpEvent.GetInstance().MoveCorrection(Vector2.left, this.transform.position, 0.6f))
            {
                this.transform.position += Vector3.left * force * Time.deltaTime;
            }
        }
    }

    public void AttackStart() // 판정이 켜져있는시간
    {
        attackMG.attackHitCollider.SetActive(true);
        attack.attackCheck = false;
    }

    public void AttackEnd() // 판정을끄면서 대미지를넣는다.
    {
        attackMG.AttackHit(state.attackDamage);
        attackMG.attackHitCollider.SetActive(false);
        if (!attack.attackCheck)
        {
            attack.AttackEnd();
        }
        moveStart = false;
    }

    //바라보는방향으로 앞으로가기
    void AttackMove(float force)
    {
        this.force = force;
        moveStart = true;
    }

    public void UpperCutStart()
    {
        attackMG.attackHitCollider.SetActive(true);
        force = 0;
    }

    public void UpeerCutAirBorne()
    {
        //attackMG.AirBorne();
    }

    public void UpperCutEnd()
    {
        attackMG.attackHitCollider.SetActive(false);
        attackMG.AttackHit(state.skillDamage);
        upperCut.UpperCutEnd();
    }

    public void SkillStart()
    {
        attackMG.attackHitCollider.SetActive(true);
        force = 0;
    }

    public void SkillEnd()
    {
        attackMG.attackHitCollider.SetActive(false);
        attackMG.AttackHit(state.skillDamage);
    }
}
