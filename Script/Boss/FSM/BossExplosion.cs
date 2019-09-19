using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExplosion : BossFSMState
{
    int number;

    bool explosionEnd;

    public override void BeginState()
    {
        _manager.anim.SetInteger("Fsm", (int)Boss_State.Explosion);

        number = Random.Range(0, _manager.stoneBlock.Count);
        _manager.stoneBlock[number].onoff = true;

        _manager.damagedCount = 0;
        explosionEnd = true;
        base.BeginState();
    }

    public override void EndState()
    {
        _manager.stoneBlock[number].onoff = false;

        base.EndState();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_manager.damagedCount >= 6)
        {
            StartCoroutine("ExplosionEnd");
        }


        if (_manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Boss_2P_Explosion") && explosionEnd)
        {
            if (_manager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 5.0f)
            {
                _manager.playerFsm.ChScript(Player_State.Death);
                StartCoroutine("ExplosionStart");
            }
        }
    }

    IEnumerator ExplosionStart()
    {
        //이펙트출력

        yield return new WaitForSeconds(1.0f);
        _manager.playerFsm.ChScript(Player_State.Death);
    }

    IEnumerator ExplosionEnd()
    {
        explosionEnd = false;
        yield return new WaitForSeconds(10.0f);
        _manager.ChScript(Boss_State.Idle);
    }
}
