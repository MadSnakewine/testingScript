using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHit : MonsterFSMState
{

    public float upperForce;

    public override void BeginState()
    {
        Debug.Log("몹피격");
        _manager.mRb2d.velocity = Vector3.zero;
        // y축으로 upperForce만큼 상승
        //_manager.mRb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        //_manager.mRb2d.AddForce(Vector2.up * upperForce, ForceMode2D.Impulse);
        _manager.anim.SetInteger("FSM", (int)Monster_State.Hit);
        base.BeginState();
    }

    public override void EndState()
    {
        base.EndState();
    }

    void Start()
    {
       // _manager.player = GameObject.Find("Player");
    }

    void Update()
    {
        if (_manager.groundCheck)
        {
            _manager.ChScript(Monster_State.Chasing);
        }
    }
}
