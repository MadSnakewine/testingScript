using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundChange : MonoBehaviour
{
    CameraMove cameraMove;

    private void Start()
    {
        cameraMove = Camera.main.transform.GetComponent<CameraMove>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            cameraMove.bound = this.GetComponent<BoxCollider2D>();
        }
    }
}
