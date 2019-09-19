using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoar : BossFSMState
{
    public ParticleSystem effect;

    bool updateStart = false;

    int number;
    public override void BeginState()
    {
        StartCoroutine("RoarStart");
        _manager.anim.SetInteger("Fsm", (int)Boss_State.Roar);
        base.BeginState();
    }

    public override void EndState()
    {
        _manager.stoneBlock[number].onoff = false;
        updateStart = false;
        base.EndState();
    }

    // Update is called once per frame
    void Update()
    {
        if (updateStart)
        {
            Camera.main.GetComponent<CameraMove>().CameraShake(0.2f, 0.1f);

            if (_manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Boss_2P_Roar"))
            {
                if (_manager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    _manager.ChScript(Boss_State.Idle);
                }
            }
        }
    }



    IEnumerator RoarStart()
    {
        yield return new WaitForSeconds(1.5f);

        updateStart = true;
        number = Random.Range(0, _manager.stoneBlock.Count);
        _manager.stoneBlock[number].onoff = true;
        effect.Play();
    }
}
