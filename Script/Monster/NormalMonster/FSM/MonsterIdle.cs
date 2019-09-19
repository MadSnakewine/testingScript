using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdle : MonsterFSMState
{
    public override void BeginState()
    {
        _manager.anim.SetInteger("FSM", (int)Monster_State.Idle);
        base.BeginState();
    }

    public override void EndState()
    {
        base.EndState();
    }
    private void Update()
    {
            _manager.ChScript(Monster_State.Chasing);
        if (gameObject.GetComponent<HP>().hp <= 0)
        {
            StopCoroutine("Explosion");
            _manager.ChScript(Monster_State.Dead);
        }
    }
}