using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHelpEvent : MonoBehaviour
{
    public PlayerFSM fsm;
    public PlayerState pstate;

    private static BossHelpEvent instance;
    public static BossHelpEvent GetInstance() // 싱글턴
    {

        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<BossHelpEvent>();

            if (instance == null)
            {
                GameObject container = new GameObject("BossHelpEvent");

                instance = container.AddComponent<BossHelpEvent>();
            }
        }
    }

    public void Damage(float damage , bool animDamaged)
    {
        if (!fsm.invincibility)
        {
            pstate.hp.value -= damage;
            if (animDamaged)
                fsm.ChScript(Player_State.Damaged);
        }
    }
}
