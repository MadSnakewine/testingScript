using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Timer
{
    public float PoisonGas; // 독가스
    public float Thorn;
    public float Pierce;
}

public class BossIdle : BossFSMState
{
    Timer mainTimer;
    Vector3 oldPlayerPos;

    public List<int> PageSwapHp = new List<int>();
    List<int> PosNumberList = new List<int>();

    List<Boss_State> patternList = new List<Boss_State>();

    bool[] damgedanim = { true, true, true, true, true, true, true, true, true, true };

    public override void BeginState()
    {
        _manager.anim.SetInteger("Fsm", (int)Boss_State.Idle);

        base.BeginState();
    }

    public override void EndState()
    {
        base.EndState();
    }

    private void Start()
    {
        TimerReset();
        oldPlayerPos = _manager.player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PageCh();
        Dameged();

        if (_manager.hp.hp <= 0)
        {
            Destroy(this.transform.parent.gameObject);
        }

        PatternStart();

        if (_manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            switch (_manager.bossPage)
            {
                case 1:
                    {
                        PageOne();
                        break;
                    }
                case 2:
                    {
                        PageTwo();
                        break;
                    }
            }
        }
    }

    void Dameged()
    {
        float tempNumber = _manager.hp.maxHp / 10;
        for (int i = 1; i < 10; i++)
        {
            if (_manager.hp.hp <= _manager.hp.maxHp  - tempNumber * i && damgedanim[i])
            {
                patternList.Add(Boss_State.Damaged);
                damgedanim[i] = false;
            }
        }
    }

    void PatternStart()
    {
        if(patternList.Count != 0) //리스트에 무언가있다면 실행
        {
            switch(patternList[0])
            {
                case Boss_State.Cut:
                    _manager.ChScript(Boss_State.Cut);
                    break;
                case Boss_State.Explosion:
                    _manager.ChScript(Boss_State.Explosion);
                    break;
                case Boss_State.PageChange:
                    _manager.ChScript(Boss_State.PageChange);
                    break;
                case Boss_State.Pierce:
                    _manager.ChScript(Boss_State.Pierce);
                    break;
                case Boss_State.PoisonGas:
                    _manager.ChScript(Boss_State.PoisonGas);
                    break;
                case Boss_State.PosChange:
                    _manager.posNumber = PosNumberList[0];
                    PosNumberList.RemoveAt(0);
                    _manager.ChScript(Boss_State.PosChange);
                    break;
                case Boss_State.Roar:
                    _manager.ChScript(Boss_State.Roar);
                    break;
                case Boss_State.Thorn:
                    _manager.ChScript(Boss_State.Thorn);
                    break;
                case Boss_State.Damaged:
                    _manager.ChScript(Boss_State.Damaged);
                    break;
            }
            patternList.RemoveAt(0);
        }
    }

    void TimerReset()
    {
        mainTimer.PoisonGas = _manager.state.poisonTimer;
        mainTimer.Thorn = _manager.state.thornTimer;
        mainTimer.Pierce = _manager.state.pierceTimer;
    }

    void PageCh()
    {
        if(_manager.hp.hp <= PageSwapHp[0] && _manager.bossPage == 1) // 2페이지
        {
            TimerReset();
            _manager.bossPage++;
            _manager.ChScript(Boss_State.PageChange);
        }
    }

    void PageOne()
    {
        //가시
        if (oldPlayerPos == _manager.player.transform.position) // 가만히있으면 타이머다운
            mainTimer.Thorn -= Time.deltaTime;
        else // 움직였으면 움직인위치 다시 검색
        {
            oldPlayerPos = _manager.player.transform.position;
            mainTimer.Thorn = _manager.state.thornTimer;
        }

        // 독가스
        if (_manager.playerGroundCheck) // 플레이어가 땅에있으면 독가스타이머가줄어든다.
            mainTimer.PoisonGas -= Time.deltaTime;
        else if (!_manager.playerGroundCheck) // 땅을 벗어나면 타이머 초기화
            mainTimer.PoisonGas = _manager.state.poisonTimer;

        //찍기
        mainTimer.Pierce -= Time.deltaTime;

        // cut
        if (_manager.damagedCount >= _manager.state.cutCount)
        {
            patternList.Add(Boss_State.Cut);
        }
        else if (mainTimer.Thorn <= 0) // 가시
        {
            patternList.Add(Boss_State.Thorn);
            mainTimer.Thorn = _manager.state.thornTimer;
        }
        else if (mainTimer.PoisonGas <= 0) //독가스
        {
            patternList.Add(Boss_State.PosChange);
            PosNumberList.Add(0);
            patternList.Add(Boss_State.PoisonGas);

            patternList.Add(Boss_State.PosChange);
            PosNumberList.Add(1);
            patternList.Add(Boss_State.PoisonGas);

            patternList.Add(Boss_State.PosChange);
            PosNumberList.Add(2);
            patternList.Add(Boss_State.PoisonGas);

            patternList.Add(Boss_State.PosChange);
            PosNumberList.Add(1);

            mainTimer.PoisonGas = _manager.state.poisonTimer;
        }
        else if (mainTimer.Pierce <= 0) // 찍기
        {
            patternList.Add(Boss_State.Pierce);
            mainTimer.Pierce = _manager.state.pierceTimer;
        }
    }

    void PageTwo()
    {
      
    }

    void PageThree()
    {

    }

    void PageFour()
    {

    }
}

// hp를 게속 판단하여 페이지를변경시켜줄아이