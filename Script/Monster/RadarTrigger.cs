using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarTrigger : MonoBehaviour
{
    public Radar rader;

    public GameObject[] target;
    public bool[] map;   

    public bool pulse = true;
    public bool one = false;

    private void Update()
    {
        if (rader.target != null)
            if (rader.target.activeSelf)
            {
                for (int i = 0; i < target.Length; i++)
                    target[i].SetActive(map[i]);
                if (one)
                    this.enabled = false;
                return;
            }

        if (pulse)
            for (int i = 0; i < target.Length; i++)
                target[i].SetActive(!map[i]);
    }
}