using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EliteFSM))]
public class EliteFSMState : MonoBehaviour
{
    [HideInInspector] public EliteFSM _manager;

    void Awake()
    {
        _manager = GetComponent<EliteFSM>();
    }

    public virtual void BeginState()
    {

    }

    public virtual void EndState()
    {

    }
}
