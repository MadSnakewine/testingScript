using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimEvent : MonoBehaviour
{
    public BossPierce pierce;
    public List<GameObject> hitBox = new List<GameObject>();
    public List<ParticleSystem> effect = new List<ParticleSystem>();


    public void cutBoxOn(int num)
    {
        hitBox[num].SetActive(true);
        effect[num].Play();
    }

    public void warningOff(int num)
    {
        pierce.warning[num].SetActive(false);
    }
}
