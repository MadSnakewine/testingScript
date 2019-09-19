using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public AttackManage attackMG;
    public PlayerSkill skill;
    bool overLap = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            foreach(var hitObj in attackMG.listHitObj)
            {
                if(hitObj.gameObject ==collision.gameObject)
                {
                    overLap = false;
                }
            }

            if(overLap)
                attackMG.listHitObj.Add(collision.gameObject);

            overLap = true;
        }
    }

}
