using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public BossFsm fsm;


    public Vector2 boxSize;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(this.transform.position, boxSize, 0);
        foreach (Collider2D col in cols)
        {
            if (col.transform.tag == "Player")
                fsm.playerGroundCheck = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
            fsm.playerGroundCheck = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }
}
