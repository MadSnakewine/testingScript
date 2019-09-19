using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCreate : MonoBehaviour
{
    GameObject wall;
    public GameObject boss;

    private void Start()
    {
        wall = this.transform.GetChild(0).gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            wall.SetActive(true);
            boss.SetActive(true);
        }
    }
}
