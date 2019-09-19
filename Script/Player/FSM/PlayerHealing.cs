using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealing : FSMState
{
    [HideInInspector] public float timer;
    public float timerMax;

    float hp;
    public override void BeginState()
    {
        if (_manager.state.Stamina.value == 50)
        {
            Application.targetFrameRate = 60;
            _manager.move.moveOn = false;
            hp = _manager.state.hp.value;
            timer = timerMax;
            _manager.effect.healingEffect.Play();
            _manager.anim.SetInteger("Fsm", (int)Player_State.Healing);
        }
        else
        {
            ReCoveryEnd();
        }

        base.BeginState();
    }

    public override void EndState()
    {
        _manager.effect.healingEffect.Stop();
        _manager.move.moveOn = true;
        base.EndState();
    }

    void FixedUpdate()
    {
        ReCovery();
    }

    void ReCovery()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            _manager.state.hp.value += 300;
            ReCoveryEnd();
        }

        //게이지 줄어들기
        StaminaDown();

        // 피격시 idle로 옯기고 피격애니메이션실행
        BeHit();
    }

    void ReCoveryEnd()
    {
        _manager.ChScript(Player_State.Idle);
    }

    void StaminaDown()
    {
        _manager.state.Stamina.value -= 1.0f;
    }

    void BeHit()
    {
        if(hp > _manager.state.hp.value)
        {
            ReCoveryEnd();
        }
    }
}
