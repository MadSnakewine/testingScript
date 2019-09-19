using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public GameObject mob; // 적 프리팹
    float sponTime = 0.3f;  // 리스폰 시간 ( 3초에 1번 소환)
    void Update()
    {
        sponTime -= Time.deltaTime;  // 리스폰 시간을 깍음.
        if (sponTime < 0)                  // 리스폰 시간이 0이 되었는지 검사
        {
            Instantiate(mob, transform.position, Quaternion.identity); // 생성
            sponTime = 0.3f;                                                    // 리스폰시간 초기화
        }
    }
}
