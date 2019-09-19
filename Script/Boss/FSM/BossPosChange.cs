using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPosChange : BossFSMState
{
    bool check = true;
    public List<GameObject> posObject = new List<GameObject>();

    public override void BeginState()
    {
        _manager.anim.SetInteger("Fsm", (int)Boss_State.PosChange);
        base.BeginState();
    }

    public override void EndState()
    {
        check = true;
        base.EndState();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Boss_2P_Pos"))
        {
            if (_manager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
            {

                _manager.ChScript(Boss_State.Idle);

            }
            else if (_manager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f && check)
            {
                check = false;
                Camera.main.GetComponent<CameraMove>().CameraShake(0.5f, 0.5f);
                this.transform.parent.transform.position = posObject[_manager.posNumber].transform.position;
            }

        }
    }
}