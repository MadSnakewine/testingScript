using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Radar : MonoBehaviour
{
    public string[] collisionTag;   // 충돌 테그
    public GameObject target;       // 대상

    public bool OutCheck = false;
    public Vector2 OutCheckPosition;
    public Vector2 OutCheckSize;

    void Start()
    {
        target = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌 테그에 충돌한 오브젝트를 대상에 저장
        for (int i = 0; i < collisionTag.Length; i++)
            if (collision.tag == collisionTag[i])
                target = collision.gameObject;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            if (target.activeSelf == false)
            {
                target = null;
                return;
            }
            if (OutCheck)
            {
                CharLibrary.Rect thisRect = new CharLibrary.Rect((Vector2)this.transform.position + OutCheckPosition, OutCheckSize);

                if (!(target.transform.position.x > thisRect.left &&
                    target.transform.position.x < thisRect.right &&
                    target.transform.position.y > thisRect.down &&
                    target.transform.position.y < thisRect.up))
                {
                    target = null;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == target)
            target = null;
    }
}