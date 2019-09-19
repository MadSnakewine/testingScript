using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneUI : MonoBehaviour
{
    public GameObject sceneUI;
    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A) && Vector3.Distance(player.transform.position, this.transform.position) <= 1.8f)
        {
            sceneUI.SetActive(true);
        }
    }
}
