using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCutDamageBox : MonoBehaviour
{
    BossState bstate;
    public Boss_State state;
    public Vector2 boxSize;

    // Start is called before the first frame update
    void Start()
    {
        bstate = GameObject.Find("Boss").transform.GetChild(0).GetComponent<BossState>();
    }

    private void Update()
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(this.transform.position, boxSize, this.transform.eulerAngles.z);
        foreach (Collider2D col in cols)
        {
            if (col.transform.tag == "Player")
            {
                switch(state)
                {
                    case Boss_State.Cut:
                        BossHelpEvent.GetInstance().Damage(bstate.cutDamage , true);
                        break;
                    case Boss_State.Pierce:
                        BossHelpEvent.GetInstance().Damage(bstate.pierceDamage, true);
                        break;
                }
                
                
            }
        }
        this.gameObject.SetActive(false);
    }
    
    void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }
}
