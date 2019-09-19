using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPoisonDamage : MonoBehaviour
{
    [HideInInspector] public BossState bstate;

    public Vector2 boxSize;

    // Start is called before the first frame update
    void Start()
    {
        bstate = GameObject.Find("Boss").transform.GetChild(0).GetComponent<BossState>();
    }

    private void Update()
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(this.transform.position, boxSize, transform.eulerAngles.z);
        foreach(Collider2D col in cols)
        {
            if (col.transform.tag == "Player")
            {
                BossHelpEvent.GetInstance().Damage(bstate.poisonDamage, false);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }
}
