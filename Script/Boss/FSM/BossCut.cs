using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCut : BossFSMState
{
    bool updateStart = false;

    public GameObject warning;

    public override void BeginState()
    {
        StartCoroutine("CutStart");
        _manager.anim.SetInteger("Fsm", (int)Boss_State.Cut);
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
        if (updateStart)
        {

            if (_manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Boss_2P_Cut"))
            {
                if (_manager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    _manager.ChScript(Boss_State.Idle);
                }
            }
        }
    }

    IEnumerator CutStart()
    {
        warning.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        warning.SetActive(false);

        updateStart = true;        
    }
}
