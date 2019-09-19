using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossFsm))]
public class BossFSMState : MonoBehaviour
{
    [HideInInspector] public BossFsm _manager;

    // Start is called before the first frame update
    void Awake()
    {
        _manager = GetComponent<BossFsm>();
    }

    public virtual void BeginState()
    {

    }

    public virtual void EndState()
    {

    }

}
