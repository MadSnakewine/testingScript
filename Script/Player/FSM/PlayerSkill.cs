using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : FSMState
{
    public float speed;

    public float notGavityTimer;
    Vector3 inputMoveXY;

    public PlayerAttack attack;

    float tempY;

    public Vector2 boxSize;

    public override void BeginState()
    {
        _manager.hitBox2D.size = boxSize;

        if (InputMoveDir())
        {
            _manager.rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            _manager.transform.parent.GetComponent<PlayerMove>().enabled = false;
            _manager.move.groundChack = false;
            _manager.anim.SetInteger("Fsm", (int)Player_State.Skill);
            
            if (!attack.jumpAttackCheck)
                attack.jumpAttackCheck = true;

            lookRot(this.transform.parent.transform.position);
            //_manager.effect.skillEffect.Play();
            _manager.invincibility = true;

            _manager.state.SkillCountDown();
        }
        base.BeginState();
    }

    public override void EndState()
    {
        if (this.transform.parent.transform.localScale == new Vector3(1, -1, 1))
            this.transform.parent.transform.localScale = new Vector3(1, 1, 1);

        this.transform.parent.transform.rotation = Quaternion.Euler(0, tempY, 0);

        _manager.invincibility = false;
        base.EndState();
    }

    // Update is called once per frame
    void Update()
    {
        Skill();
    }

    void Skill()
    {
        if (_manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Skill"))
        {
            if (_manager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f)
            {
                Vector3 move = this.transform.parent.transform.position;
                move += inputMoveXY * speed;

                if (HelpEvent.GetInstance().MoveCorrection(inputMoveXY, move , 0.4f))
                    this.transform.parent.transform.position = move;
            }
            else
            {
                _manager.ChScript(Player_State.Idle);
                _manager.notGavityTime = notGavityTimer;

            }
        }
    }

    bool InputMoveDir()
    {
        inputMoveXY = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow))
            inputMoveXY.x = -1;
        if (Input.GetKey(KeyCode.RightArrow))
            inputMoveXY.x = 1;
        if (Input.GetKey(KeyCode.UpArrow))
            inputMoveXY.y = 1;
        if (Input.GetKey(KeyCode.DownArrow))
                inputMoveXY.y = -1;

        if (inputMoveXY == Vector3.zero)
        {
            _manager.ChScript(Player_State.Idle);
            tempY = this.transform.parent.transform.eulerAngles.y;
        }
        else
            return true;

        return false;
    }

    void lookRot(Vector3 target)
    {
        target += inputMoveXY;

        Vector2 targetRot;
        targetRot.x = target.x - transform.position.x;
        targetRot.y = target.y - transform.position.y;
        float angle = -1 * Mathf.Rad2Deg * Mathf.Atan2(targetRot.x, targetRot.y) + 90;

        if (Input.GetKey(KeyCode.RightArrow)) // 오른쪽
        {
            this.transform.parent.transform.localScale = new Vector3(1, 1, 1);
            _manager.effect.skillEffect.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            tempY = 0;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) //왼쪽
        {
            this.transform.parent.transform.localScale = new Vector3(1, -1, 1);
            _manager.effect.skillEffect.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            tempY = 180;
        }

        this.transform.parent.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }


}
