using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGround : MonoBehaviour
{
    public PlayerAttack attack;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 공격을한번누르면 애니가끝나면 다음애니로 애니끝날때 공격을 안누르면 끝
    public void GroundAttackCheck()
    {
        if (!attack.attackCheck)
        {
            if (attack._manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_0"))
                attack.attackNext = 1;
            else if (attack._manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_1"))
                attack.attackNext = 2;
            else if (attack._manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_2"))
                attack.attackNext = 0;

            attack.attackCheck = true;
            attack._manager.anim.SetInteger("AttackNext", attack.attackNext);
        }
    }
}
