using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteRush : EliteFSMState
{
    public override void BeginState()
    {
        _manager.anim.SetInteger("FSM", (int)Elite_State.Rush);
        base.BeginState();
    }

    public override void EndState()
    {
        base.EndState();
    }

}