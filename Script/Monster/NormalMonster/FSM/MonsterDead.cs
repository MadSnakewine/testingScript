using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDead : MonsterFSMState
{
    public override void BeginState()
    {
        _manager.anim.SetInteger("FSM", (int)Monster_State.Dead);
        Destroy(gameObject, 0.5f);
        base.BeginState();
    }
    public override void EndState()
    {
        base.EndState();
    }
}
