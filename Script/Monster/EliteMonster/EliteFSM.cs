using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Elite_State
{
    Idle,
    Chasing,
    Rush,
    ToxinGas,
    ToxinWater,
    Damaged,
    Dead
}

[RequireComponent(typeof(Elite_State))]
public class EliteFSM : MonoBehaviour
{
    private bool isinit = false;
    Elite_State startState = Elite_State.Idle;

    private Dictionary<Elite_State, EliteFSMState> _states = new Dictionary<Elite_State, EliteFSMState>();

    private Elite_State currentState;
    public Elite_State _CurrentState { get { return currentState; } }

    [HideInInspector] public EliteState state;
    [HideInInspector] public HP hp;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody2D rb;

    [HideInInspector] public GameObject player;
    [HideInInspector] public PlayerMove playerMove;
    [HideInInspector] public PlayerFSM playerFsm;

    [HideInInspector] public Rigidbody2D mRb2d;   //몹강체

    [HideInInspector] public Transform target;    //추적대상의 위치

    [HideInInspector] public bool groundCheck;

    void Awake()
    {

        player = GameObject.Find("Player");
        mRb2d = gameObject.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = transform.GetComponent<Animator>();
        state = transform.GetComponent<EliteState>();
        hp = transform.GetComponent<HP>();
        rb = transform.GetComponent<Rigidbody2D>();

        playerMove = player.GetComponent<PlayerMove>();
        playerFsm = player.transform.GetChild(0).GetComponent<PlayerFSM>();

        Elite_State[] stateValues = (Elite_State[])System.Enum.GetValues(typeof(Elite_State));
        foreach (Elite_State s in stateValues)
        {
            System.Type eFSMType = System.Type.GetType("Monster" + s.ToString());
            EliteFSMState state = (EliteFSMState)GetComponent(eFSMType);
            if (null == state)
            {
                state = (EliteFSMState)gameObject.AddComponent(eFSMType);
            }
            _states.Add(s, state);
            state.enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ChScript(startState);
        isinit = true;
    }

    // Update is called once per frame
    void Update()
    {
        Death();
        //GroundCheck();
    }

    public void ChScript(Elite_State newState)
    {
        if (isinit)
        {
            _states[currentState].enabled = false;
            _states[currentState].EndState();
        }
        currentState = newState;
        _states[currentState].BeginState();
        _states[currentState].enabled = true;
    }
    /*
    void GroundCheck()
    {
        if (currentState == Elite_State.Airborne)
        {
            return;
        }

        if (Physics2D.Raycast(transform.position, Vector2.down, 2.0f, (1 << 12))) //  지상
        {
            groundCheck = true;
        }
        else // 공중
        {
            groundCheck = false;
        }
    }
    */

    void Death()
    {
        //사망
        if (gameObject.GetComponent<HP>().hp <= 0)
        {
            ChScript(Elite_State.Dead);
        }
    }
}
