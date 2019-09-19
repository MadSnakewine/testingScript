using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2PhaseSummon : MonoBehaviour
{

    public GameObject targetBoss;
    public GameObject summonPoint;

    void Start()
    {
        targetBoss = GameObject.Find("Boss");
        summonPoint = GameObject.Find("MonstersSpawnPoint");
    }

    void Update()
    {
        if (targetBoss.GetComponentInChildren<BossFsm>().bossPage == 2)
        {
            Summon();
        }
    }

    void Summon()
    {
        summonPoint.SetActive(true);
    }
}
