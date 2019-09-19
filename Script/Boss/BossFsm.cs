using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public enum Boss_State
{
    Idle,
    PosChange,
    PoisonGas,
    Thorn,
    PageChange,
    Roar,
    Cut,
    Pierce,
    Explosion,
    Damaged
}
/// <summary>
///  위치변경
///  독가스
///  가시던지기
///  페이지변환
///  표호
///  배기
///  찍기
/// </summary>

[RequireComponent(typeof(Boss_State))]
public class BossFsm : MonoBehaviour
{

    private bool _isinit = false;
    Boss_State startState = Boss_State.Idle;

    public Dictionary<Boss_State, BossFSMState> _states = new Dictionary<Boss_State, BossFSMState>(); 

    private Boss_State currentState;
    public Boss_State _CurrentState { get { return currentState; } }

    [HideInInspector] public BossState state;
    [HideInInspector] public HP hp;
    [HideInInspector] public Animator anim;

    [HideInInspector] public int bossPage;

    public GameObject player;
    [HideInInspector] public PlayerMove playerMove;
    [HideInInspector] public PlayerFSM playerFsm;
    [HideInInspector] public bool playerGroundCheck;

    [HideInInspector] public int damagedCount; // 맞은횟수를 카운팅한다.

    public List<StoneHelp> stoneBlock = new List<StoneHelp>();
    [HideInInspector] public int posNumber = 0;

    float oldHp;
    // Use this for initialization
    void Awake()
    {
        anim = transform.parent.GetComponent<Animator>();
        state = transform.GetComponent<BossState>();
        hp = transform.parent.GetComponent<HP>();
        bossPage = 1;
        playerGroundCheck = false;
        playerMove = player.GetComponent<PlayerMove>();
        playerFsm = player.transform.GetChild(0).GetComponent<PlayerFSM>();
        oldHp = hp.hp;


        Boss_State[] stateValues = (Boss_State[])System.Enum.GetValues(typeof(Boss_State));
        foreach (Boss_State s in stateValues)
        {
            System.Type FSMType = System.Type.GetType("Boss" + s.ToString());
            BossFSMState state = (BossFSMState)GetComponent(FSMType);
            if (null == state)
            {
                state = (BossFSMState)gameObject.AddComponent(FSMType);
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
        Temp();

        damagedCountAdd();
    }

    void damagedCountAdd()
    {
        if(oldHp != hp.hp)
        {
            oldHp = hp.hp;
            damagedCount++;
        }
    }

    public void ChScript(Boss_State newState)
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

    void Temp()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            ChScript(Boss_State.Cut);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            ChScript(Boss_State.Damaged);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            ChScript(Boss_State.Explosion);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            ChScript(Boss_State.Pierce);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            ChScript(Boss_State.Roar);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            ChScript(Boss_State.Thorn);
        }
    }
}
