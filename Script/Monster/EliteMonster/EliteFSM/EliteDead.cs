using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteDead : EliteFSMState
{
    public override void BeginState()
    {
        _manager.anim.SetInteger("FSM", (int)Elite_State.Dead);
        base.BeginState();
    }

    public override void EndState()
    {
        base.EndState();
    }

}