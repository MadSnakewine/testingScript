using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveCollider : MonoBehaviour
{
    Rigidbody2D rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.transform.parent.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            rigidbody.velocity = Vector3.zero;
        }
    }
}
