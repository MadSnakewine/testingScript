using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Monster_State
{
    Idle = 0,
    JumpAttack = 1,
    Explosion = 2,
    Chasing = 3,
    Dead = 4,
    Hit = 5,
    Airborne = 6,
    AirHit = 7
}

[RequireComponent(typeof(Monster_State))]
public class MonsterFSM : MonoBehaviour
{

    private bool isinit = false;
    Monster_State startState = Monster_State.Idle;

    private Dictionary<Monster_State, MonsterFSMState> _states = new Dictionary<Monster_State, MonsterFSMState>();

    private Monster_State currentState;
    public Monster_State _CurrentState { get { return currentState; } }

    [HideInInspector] public MonsterState state;
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
        state = transform.GetComponent<MonsterState>();
        hp = transform.GetComponent<HP>();
        rb = transform.GetComponent<Rigidbody2D>();

        playerMove = player.GetComponent<PlayerMove>();
        playerFsm = player.transform.GetChild(0).GetComponent<PlayerFSM>();

        Monster_State[] stateValues = (Monster_State[])System.Enum.GetValues(typeof(Monster_State));
        foreach (Monster_State s in stateValues)
        {
            System.Type mFSMType = System.Type.GetType("Monster" + s.ToString());
            MonsterFSMState state = (MonsterFSMState)GetComponent(mFSMType);
            if (null == state)
            {
                state = (MonsterFSMState)gameObject.AddComponent(mFSMType);
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

    private void Update()
    {
        Death();
        GroundCheck();
    }

    public void ChScript(Monster_State newState)
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

    void GroundCheck()
    {
        if (currentState == Monster_State.Airborne)
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

    void Death()
    {
        //사망
        if (gameObject.GetComponent<HP>().hp <= 0)
        {
            ChScript(Monster_State.Dead);
        }
    }
}