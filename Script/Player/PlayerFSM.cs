using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player_State
{
    Idle = 0,
    Death = 1,
    Attack = 2,
    Skill = 4,
    UpperCut = 5,
    Healing = 6,
    Damaged = 7,
}

[RequireComponent(typeof(Player_State))]
public class PlayerFSM : MonoBehaviour
{
    private bool _isinit = false;
    Player_State startState = Player_State.Idle;

    private Dictionary<Player_State, FSMState> _states = new Dictionary<Player_State, FSMState>(); // Player_State라는 이름으로 FSMState라는 데이터를찾는다.

    private Player_State currentState;
    public Player_State _CurrentState { get { return currentState; } }

    [HideInInspector] public PlayerState state;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody2D rigidbody;
    [HideInInspector] public PlayerMove move;
    [HideInInspector] public PlayerEffect effect;
    // 공중대기시간
    [HideInInspector] public float notGavityTime;

    [HideInInspector] public bool airburn = false;

    [HideInInspector] public bool invincibility;

    [HideInInspector] public BoxCollider2D hitBox2D;
    // Use this for initialization
    void Awake()
    {
        invincibility = false;
        anim = transform.parent.GetComponent<Animator>();
        state = transform.GetComponent<PlayerState>();
        rigidbody = transform.parent.GetComponent<Rigidbody2D>();
        move = transform.parent.GetComponent<PlayerMove>();
        effect = transform.GetComponent<PlayerEffect>();
        hitBox2D = transform.parent.GetChild(2).GetChild(0).transform.GetComponent<BoxCollider2D>();

        Player_State[] stateValues = (Player_State[])System.Enum.GetValues(typeof(Player_State));
        foreach (Player_State s in stateValues)
        {
            System.Type FSMType = System.Type.GetType("Player" + s.ToString());
            FSMState state = (FSMState)GetComponent(FSMType);
            if (null == state)
            {
                state = (FSMState)gameObject.AddComponent(FSMType);
            }
            _states.Add(s, state);
            state.enabled = false;
        }
    }

    void Start()
    {
        ChScript(startState);
        _isinit = true;
    }

    private void Update()
    {
        if (state.hp.value <= 0)
            ChScript(Player_State.Death);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Skill") && _CurrentState != Player_State.Damaged)
        {
            if (Input.GetKeyDown(PlayerKeyMaster.GetInstance().keyMaster[PlayerKey.Skill]) && state.skillCount > 0) // 일썸
            {
                ChScript(Player_State.Skill);
            }
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            ChScript(Player_State.Damaged);
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            ChScript(Player_State.Death);
        }
    }

    public void ChScript(Player_State newState)
    {
        if (_isinit)
        {
            _states[currentState].enabled = false;
            _states[currentState].EndState();
        }
        currentState = newState;
        _states[currentState].BeginState();
        _states[currentState].enabled = true;
    }
}