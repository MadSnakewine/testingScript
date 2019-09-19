using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamaged : BossFSMState
{
    public override void BeginState()
    {
        _manager.anim.SetInteger("Fsm", (int)Boss_State.Damaged);
        base.BeginState();
    }

    public override void EndState()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Boss_2P_Damage"))
        {
            if (_manager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                _manager.ChScript(Boss_State.Idle);
            }
        }
    }

}