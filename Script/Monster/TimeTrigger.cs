using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrigger : MonoBehaviour
{
    public GameObject[] target;
    public bool[] map;
    public float time;

    public bool one = false; 
    public bool PowerDestroy = false;
    public bool NonStart = false;

    private float timeCounter;

    private void Awake()
    {
        if (one || NonStart)
            timeCounter = time;
    }

    void Update()
    {
        if (target.Length != map.Length)
            return;
        timeCounter -= Time.deltaTime;
        if (timeCounter < 0)
        {
            if (PowerDestroy)
                Destroy(this.gameObject);
            if (one)
                this.enabled = false;
            timeCounter += time;
            for (int i = 0; i < target.Length; i++)
                target[i].SetActive(map[i]);
        }
    }
}