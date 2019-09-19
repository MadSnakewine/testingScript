using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPageChange : BossFSMState
{

    public override void BeginState()
    {
        _manager.anim.SetInteger("Fsm",(int)Boss_State.PageChange);
        _manager.damagedCount = 0;
        base.BeginState();
    }

    public override void EndState()
    {
        base.EndState();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Ani_Boss_Ecdysis1~2"))
        {
            if (_manager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                _manager.anim.SetFloat("Page", _manager.bossPage);
                _manager.ChScript(Boss_State.Roar);
            }
        }
    }
}
