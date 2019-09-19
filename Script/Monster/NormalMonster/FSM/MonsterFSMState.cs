using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonsterFSM))]
public class MonsterFSMState : MonoBehaviour
{
    [HideInInspector] public MonsterFSM _manager;

    void Awake()
    {
        _manager = GetComponent<MonsterFSM>();
    }

    public virtual void BeginState()
    {

    }

    public virtual void EndState()
    {

    }
}
