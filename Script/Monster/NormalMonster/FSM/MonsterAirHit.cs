using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAirHit : MonsterFSMState
{
    void Update()
    {
        if (_manager.groundCheck)
            _manager.ChScript(Monster_State.Chasing);      
    }

    public override void BeginState()
    {
        //Debug.Log("공중맞기");
        //_manager.anim.SetInteger("FSM", (int)Monster_State.AirHit);

        if (!_manager.groundCheck )
        {
            _manager.anim.SetInteger("FSM", (int)Monster_State.AirHit);
            _manager.mRb2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            _manager.mRb2d.constraints = RigidbodyConstraints2D.None;
        }
        base.BeginState();
    }

    public override void EndState()
    {
        //_manager.anim.SetInteger("FSM", (int)Monster_State.AirHit);
        base.EndState();
    }

}
