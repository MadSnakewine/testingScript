using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGroundCheck : MonoBehaviour
{
    bool isGround;
    Rigidbody2D mRb2d;

    void Start()
    {
        mRb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GroundCheck();
    }

    void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, (1 << 12));
        //Debug.DrawRay(transform.position, Vector2.down * 1.0f, Color.red);


        //레이가 12번 레이어(맵콜라이더)에 닿았을 때
        if (Physics2D.Raycast(transform.position, Vector2.down, 1.0f, (1 << 12)))
        {
            //Debug.Log(hit.collider.gameObject.name);
            //Debug.Log("땅");
            mRb2d.constraints = RigidbodyConstraints2D.FreezePositionY;
            isGround = true;
            return;
        }
        //12번 레이어에 닿지 않았을 때
        else
        {
            //Debug.Log("안땅");
            mRb2d.constraints = RigidbodyConstraints2D.None;
            isGround = false;
        }
    }
}
