using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPoisonGas : BossFSMState
{
    public ParticleSystem effectGasStick;
    public ParticleSystem effectGasCloud;
    public GameObject colliderBox;

    public ParticleSystem warning;

    bool updateStart = false;
    public override void BeginState()
    {
        StartCoroutine("PoisonGasStart");
        _manager.anim.SetInteger("Fsm", (int)Boss_State.PoisonGas);

        base.BeginState();
    }

    public override void EndState()
    {
        colliderBox.SetActive(false);
        updateStart = false;
        base.EndState();
    }


    // Update is called once per frame
    void Update()
    {
        if (updateStart)
        {
            if (_manager.anim.GetCurrentAnimatorStateInfo(0).IsName("1PBoss_Poisongas") || _manager.anim.GetCurrentAnimatorStateInfo(0).IsName("2PBoss_Poisongas"))
            {
                if (_manager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    EndPoisonGas();
                }
            }
        }
    }

    void EndPoisonGas()
    {
        _manager.ChScript(Boss_State.Idle);
    }

    IEnumerator PoisonGasStart()
    {
        //warning.Play();

        yield return new WaitForSeconds(1.5f);
        updateStart = true;
        effectGasStick.Play();
        effectGasCloud.Play();
        colliderBox.SetActive(true);

    }
}

