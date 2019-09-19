using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawn : MonoBehaviour
{
    public StoneHelp help;
    public List<GameObject> stone = new List<GameObject>();

    int count;
    bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    private void Update()
    {
        if (help.onoff && start)
        {
            stone[count].SetActive(true);
            stone[count].transform.position = this.transform.position;
            if (stone.Count - 1 == count)
                count = 0;
            else
                count++;

            start = false;
        }
        else if(!help.onoff)
        {
            start = true;
        }
    }
}
