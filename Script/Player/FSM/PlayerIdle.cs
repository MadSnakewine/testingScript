using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : FSMState
{

    private void Start()
    {
    }

    public override void BeginState()
    {
        _manager.anim.SetInteger("Fsm", (int)Player_State.Idle);

    }

    // Update is called once per frame
    void Update()
    {
        NotGavity();
        if (_manager.anim.GetInteger("Fsm") != (int)Player_State.Damaged && _manager.anim.GetInteger("Fsm") != 99)
        {

            if (Input.GetKeyDown(PlayerKeyMaster.GetInstance().keyMaster[PlayerKey.Attack]))
            {
                if (Input.GetKey(PlayerKeyMaster.GetInstance().keyMaster[PlayerKey.UpperCut]) && _manager.move.groundChack)
                    _manager.ChScript(Player_State.UpperCut);
                else
                    _manager.ChScript(Player_State.Attack);
            }

            if (Input.GetKey(PlayerKeyMaster.GetInstance().keyMaster[PlayerKey.Healing]) && _manager.move.groundChack)
            {
                _manager.ChScript(Player_State.Healing);
            }
        }
    }

    // 공중대기
    void NotGavity()
    {
        if (_manager.notGavityTime > 0) // 공중대기
        {
            _manager.notGavityTime = Mathf.Max(_manager.notGavityTime - Time.deltaTime, 0);
            _manager.rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            _manager.move.enabled = false;
        }
        else // 일반
        {
            _manager.rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            _manager.move.enabled = true;
        }
    }
}