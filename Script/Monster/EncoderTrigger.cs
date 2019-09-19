using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 오브젝트의 신호를 조합하여 출력합니다.
public class EncoderTrigger : MonoBehaviour
{
    public GameObject[] inputList;      // 입력 오브젝트 리스트
    public bool[] intpuMap;             // 맵 (input 리스트가 map 과 같아질 경우 outputList On)
    public GameObject[] outputList;     // 출력 오브젝트 리스트
    public bool[] outputMap;     // 출력 오브젝트 리스트

    public bool isDisposable = false;   // 1 회용 엔코더
    public bool isMaintenance = false;  // 지속 신호
    private bool isFinish = false;      // 엔코더 사용 종료

    private void Awake()
    {
        if (inputList.Length != intpuMap.Length || outputList.Length != outputMap.Length)
            isFinish = true;
    }
    private void Update()
    {
        if (isFinish)
            return;

        bool[] testMap = new bool[intpuMap.Length];

        // 검사
        for (int i = 0; i < inputList.Length; i++)
            if (inputList[i].activeSelf == intpuMap[i])
                testMap[i] = true;
            else
                testMap[i] = false;
        for (int i = 0; i < testMap.Length; i++)
            // 검사 실패시
            if (!testMap[i])
            {
                for (int j = 0; j < outputList.Length; j++)
                    if (isMaintenance)
                        outputList[j].SetActive(!outputMap[j]);
                return;
            }

        if (isDisposable)
            isFinish = true;

        // 출력
        for (int i = 0; i < outputList.Length; i++)
            outputList[i].SetActive(outputMap[i]);
    }
}
