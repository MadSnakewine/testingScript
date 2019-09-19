using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpperCut : FSMState
{
    public Vector2 boxSize;

    public override void BeginState()
    {
        _manager.hitBox2D.size = boxSize;
        if (!_manager.anim.GetCurrentAnimatorStateInfo(0).IsName("UpperCut"))
        {
            _manager.rigidbody.velocity = new Vector3(0, _manager.state.upperCutJumpforce, 0);
            _manager.airburn = true;
            _manager.anim.SetInteger("Fsm", (int)Player_State.UpperCut);
            _manager.move.moveOn = false;
        }
        else
            UpperCutEnd();
    }

    public override void EndState()
    {
        _manager.airburn = false;
        _manager.move.moveOn = true;
    }


    public void UpperCutEnd()
    {
        _manager.ChScript(Player_State.Idle);
    }
}
