using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPierce : BossFSMState
{
    bool updateStart = false;

    public List<GameObject> warning =new List<GameObject>();
    BoxCollider2D boxCollider;

    public override void BeginState()
    {
        StartCoroutine("PierceStart");
        _manager.anim.SetInteger("Fsm", (int)Boss_State.Pierce);

        base.BeginState();
    }

    public override void EndState()
    {
        boxCollider.enabled = false;
        updateStart = false;
        base.EndState();
    }

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = this.transform.parent.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (updateStart)
        {
            if (_manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Boss_2P_Pierce"))
            {
                if (_manager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    _manager.ChScript(Boss_State.Idle);
                }
            }
        }
    }

    IEnumerator PierceStart()
    {
        for (int i = 0; i < warning.Count; i++)
        {
            warning[i].SetActive(true);
            warning[i].GetComponent<Animator>().Play("impact_warning");
        }

        yield return new WaitForSeconds(1.5f);
        boxCollider.enabled = true;

        updateStart = true;
    }
}
