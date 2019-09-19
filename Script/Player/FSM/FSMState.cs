using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerFSM))]
public class FSMState : MonoBehaviour {

    [HideInInspector] public PlayerFSM _manager;

    // Use this for initialization
    void Awake()
    {
        _manager = GetComponent<PlayerFSM>();
    }
    public virtual void BeginState()
    {

    }

    public virtual void EndState()
    {

    }
}
