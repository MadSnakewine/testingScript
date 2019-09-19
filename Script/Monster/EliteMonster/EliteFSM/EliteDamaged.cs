using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteDamaged : EliteFSMState
{
    public override void BeginState()
    {
        _manager.anim.SetInteger("FSM", (int)Elite_State.Damaged);
        base.BeginState();
    }

    public override void EndState()
    {
        base.EndState();
    }

}