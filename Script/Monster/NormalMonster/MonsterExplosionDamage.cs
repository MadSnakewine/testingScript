using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterExplosionDamage : MonoBehaviour
{

    [HideInInspector] public MonsterState mstate;
    public Vector2 boxSize;

    // Start is called before the first frame update
    void Start()
    {
        mstate = GameObject.Find("Monster").transform.GetChild(0).GetComponent<MonsterState>();
    }

    private void Update()
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(this.transform.position, boxSize, 0);
        foreach (Collider2D col in cols)
        {
            if (col.transform.tag == "Player")
            {
                //GameObject.Find("Player").GetComponent<PlayerState>().hp
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }
}
