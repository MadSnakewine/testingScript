using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AttackName
{
    Ground,
    Jump
}

public class PlayerAttack : FSMState
{
    [HideInInspector] public bool attackCheck; // 다음공격을할건지 true일경우 다음공격 flase일경우 공격끝
    [HideInInspector] public int attackNext;

    [HideInInspector] public bool jumpAttackCheck; // 점프상태에서공격이가능한지 true면 공격가능 false공격불가 move쪽에서 지상에내려가면 true로 돌려준다.

    public AttackGround attackGround;
    public AttackJump attackJump;

    public Vector2 boxSize; // 공격범위

    // 판정할때 애니메이션이 어디인지확인한이유는 확인을하지않으면 코드는지상이지만 애니메이션은조금느리게움직여서 코드는지상 애니메이션은공중인경우가있기때문에 애니메이션도 한번더 처리해주었다.
    public override void BeginState()
    {
        _manager.hitBox2D.size = boxSize;
        // 지상상태
        if (_manager.move.groundChack && _manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            _manager.anim.SetInteger("Fsm", (int)Player_State.Attack);
            _manager.move.enabled = false;
        }
        // 공중상태
        else if (!_manager.move.groundChack && _manager.anim.GetCurrentAnimatorStateInfo(0).IsName("DoubleJump") || _manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") || _manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Fall"))
        {
            if (jumpAttackCheck) // 점프평타
            {
                _manager.anim.SetInteger("Fsm", (int)Player_State.Attack);
                _manager.rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                _manager.move.enabled = false;
            }
            else if (!jumpAttackCheck)
            {
                AttackEnd();
            }
        }
        else
        {
            AttackEnd();
        }
    }

    public override void EndState()
    {
        _manager.move.enabled = true;
        attackNext = 0;
        _manager.anim.SetInteger("AttackNext", attackNext);
        base.EndState();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown((PlayerKeyMaster.GetInstance().keyMaster[PlayerKey.Attack])))
        {
            if (_manager.move.groundChack) // 지상
            {
                if (Input.GetKey((PlayerKeyMaster.GetInstance().keyMaster[PlayerKey.UpperCut])))
                {
                    _manager.ChScript(Player_State.UpperCut);
                    return;
                }
                attackGround.GroundAttackCheck();
            }
            else if (!_manager.move.groundChack) //공중
            {
                attackJump.JumpAttackCheck();
            }

        }
    }

    public void AttackEnd()
    {
        if (jumpAttackCheck)
            jumpAttackCheck = false;

        _manager.ChScript(Player_State.Idle);
    }

}