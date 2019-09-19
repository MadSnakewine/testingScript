using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackJump : MonoBehaviour
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

    public void JumpAttackCheck() // 점프중에는 한번만되야한다.
    {
        if (!attack.attackCheck && attack.jumpAttackCheck)
        {
            attack.attackCheck = true;

            if (attack._manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_0"))
                attack.attackNext = 1;
            else if (attack._manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_1"))
                attack.attackNext = 2;
            else if (attack._manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_2"))
                attack.attackCheck = false;

            attack._manager.anim.SetInteger("AttackNext", attack.attackNext);
        }
    }
}
