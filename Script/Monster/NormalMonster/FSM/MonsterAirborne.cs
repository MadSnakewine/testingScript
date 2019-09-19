using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAirborne : MonsterFSMState
{
    public float upperForce;

    public override void BeginState()
    {
        Debug.Log("에어본");
        _manager.mRb2d.velocity = Vector3.zero;
        // y축으로 upperForce만큼 상승
        _manager.mRb2d.AddForce(Vector3.up * upperForce, ForceMode2D.Impulse);
        _manager.anim.SetInteger("FSM", (int)Monster_State.Airborne);
        _manager.ChScript(Monster_State.AirHit);        

        base.BeginState();
    }

    public override void EndState()
    {
        base.EndState();
    }

    void Start()
    {
        //_manager.player = GameObject.Find("Player");
    }

    void Update()
    {
    }
}
