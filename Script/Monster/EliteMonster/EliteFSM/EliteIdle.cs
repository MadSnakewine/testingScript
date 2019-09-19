using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteIdle : EliteFSMState
{
    public override void BeginState()
    {
        _manager.anim.SetInteger("FSM", (int)Elite_State.Idle);
        base.BeginState();
    }

    public override void EndState()
    {
        base.EndState();
    }

}