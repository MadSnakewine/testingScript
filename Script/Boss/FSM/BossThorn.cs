using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThorn : BossFSMState
{
    public List<GameObject> thornList = new List<GameObject>();
    int thornCount= 0;

    public List<GameObject> thornPos = new List<GameObject>();

    public GameObject warning;

    public override void BeginState()
    {
        _manager.anim.SetInteger("Fsm", (int)Boss_State.Thorn);
        StartCoroutine("ThornStart");
        base.BeginState();
    }

    public override void EndState()
    {
        base.EndState();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (_manager.anim.GetCurrentAnimatorStateInfo(0).IsName("Boss_2P_Thorn"))
        {
            if (_manager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                _manager.ChScript(Boss_State.Idle);
            }
        }
    }

    void ThornProduction(int count, float startAngle , float angle, int posNumber) // 가시발사갯수 , 시작각도 , 가시각도 , 발사위치
    {
        for (int i = 0; i < count; i++, thornCount++)
        {
            thornList[thornCount].transform.localPosition = thornPos[posNumber].transform.localPosition;
            thornList[thornCount].transform.eulerAngles += new Vector3(0, 0, startAngle);
            startAngle += angle;
            thornList[thornCount].SetActive(true);

            if (thornCount == thornList.Count)
                thornCount = -1;
        }
    }

    IEnumerator ThornStart()
    {
        //warning.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        //warning.SetActive(false);

        Camera.main.GetComponent<CameraMove>().CameraShake(0.5f, 0.2f);

        ThornProduction(8, 0, 45, 0);

        yield return new WaitForSeconds(1.0f);
        ThornProduction(8, 0, 45, 1);

        yield return new WaitForSeconds(1.0f);
        ThornProduction(8, 0, 45, 2);

        yield return new WaitForSeconds(1.0f);
        ThornProduction(8, 0, 45, 3);

    }
}

