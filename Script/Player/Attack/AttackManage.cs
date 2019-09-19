using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManage : MonoBehaviour
{
    public List<GameObject> listHitObj = new List<GameObject>();
    public PlayerState state;
    public PlayerFSM fsm;
    public GameObject attackHitCollider;

    bool deathon = false;

    public void AttackHit(int damage)
    {
        foreach (var hitObj in listHitObj)
        {
            state.Stamina.value += 1;

            if (hitObj)
            {
                hitObj.GetComponent<HP>().HpDown(damage);
                Camera.main.GetComponent<CameraMove>().CameraShake(0.5f, 0.05f);
                EffectCase();

                if (hitObj.GetComponent<HP>().hp <= 0)
                {
                    deathon = true;
                }
            }
        }
        if(deathon && state.skillCount < 3)
        {
            state.SkillCountUp();
            deathon = false;
        }

        listHitObj.Clear();
    }

    //public void AirBorne()
    //{
    //    foreach (var hitObj in listHitObj)
    //    {
    //        if (fsm._CurrentState == Player_State.UpperCut)
    //        {
    //            if(hitObj.transform.GetComponent<MonsterFSM>())
    //                hitObj.transform.GetComponent<MonsterFSM>().ChScript(Monster_State.Airborne);
    //        }
    //    }
    //}

    void EffectCase()
    {
        switch (fsm._CurrentState)
        {
            case Player_State.UpperCut:
                break;
            case Player_State.Attack:
                fsm.effect.attackEffect.Play();
                break;
            case Player_State.Skill:
                break;
        }
    }

    void ColliderFalse()
    {
        attackHitCollider.SetActive(false);
    }
}

