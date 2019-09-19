using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject trigger;
    public GameObject spawnObject;
    public bool dubble = false;

    private bool create = false;
    private Transform pool;

    private void Start()
    {
        pool = GameObject.Find("Summon").transform;
    }

    void FixedUpdate()
    {
        if (!create && trigger.activeSelf)
        {
            Instantiate(spawnObject, this.transform.position, this.transform.rotation).transform.SetParent(pool);
            create = true;
        }
        if (create && !trigger.activeSelf)
        {
            if (dubble)
                Instantiate(spawnObject, this.transform.position, this.transform.rotation).transform.SetParent(pool);
            create = false;
        }
    }
}
