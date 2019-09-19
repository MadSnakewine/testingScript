using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamaged : FSMState
{

    public override void BeginState()
    {
        Physics2D.gravity = new Vector2(0, -100);
        _manager.rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        _manager.transform.parent.GetComponent<PlayerMove>().enabled = true;
        _manager.move.moveOn = false;

        if (_manager.move.groundChack)
            _manager.anim.SetFloat("Ground", -1f);
        else if (!_manager.move.groundChack)
            _manager.anim.SetFloat("Ground", 1.0f);

        _manager.anim.SetInteger("Fsm", (int)Player_State.Damaged);
        StartCoroutine("TimeScale");
        _manager.invincibility = true;
        
        Camera.main.GetComponent<CameraMove>().CameraShake(0.5f, 0.05f);
        base.BeginState();
    }

    public override void EndState()
    {
        Physics2D.gravity = new Vector2(0, -50);
        _manager.invincibility = false;
        _manager.move.moveOn = true;
        base.EndState();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Damaged"))
        {
            _manager.anim.SetInteger("Fsm", (int)Player_State.Damaged + 100);
        }

        //지상
        if (_manager.move.groundChack)
        {
            if (_manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Damaged") || _manager.anim.GetCurrentAnimatorStateInfo(0).IsName("AirDemagedEnd"))
            {
                _manager.anim.SetInteger("Fsm", (int)Player_State.Damaged + 100);

                if (_manager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
                {
                    EndDamaged();
                }
            }
        }
        //공중 이면 뒤로밀리게
        else if (!_manager.move.groundChack)
        {
            if (this.transform.parent.transform.eulerAngles.y == 0)
                this.transform.parent.transform.position += Vector3.left * 8 * Time.deltaTime;
            else
                this.transform.parent.transform.position += Vector3.right * 8 * Time.deltaTime;
        }
    }

    void EndDamaged()
    {
        _manager.ChScript(Player_State.Idle);
    }

    IEnumerator TimeScale()
    {
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 1.0f;

    }
}

