using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// tag 에 오브젝트가 충돌하면 오브젝트를 끄고 킵니다.
[RequireComponent(typeof(BoxCollider2D))]
public class SwitchTrigger : MonoBehaviour
{
    public string[] collisionTag;   // 충돌 테그
    public GameObject startObject;     // 충돌시 활성화 할 오브젝트
    public GameObject targetObject;    // 충돌시 비 활성화 할 오브젝트

    public bool isButton = true;    // 되돌아가는 기능이 없는 단방향 버튼
    public bool logicSwitch = false;

    private GameObject dataObject = null;

    private void Awake()
    {
        // 시작 설정
        startObject.SetActive(true);
        targetObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌 테그에 오브젝트가 충돌한 경우
        for (int i = 0; i < collisionTag.Length; i++)
            if (collision.tag == collisionTag[i])
            {
                dataObject = collision.gameObject;
                Activate();
            }
    }

    private void Update()
    {
        if (!logicSwitch || dataObject == null)
            return;

        if (!dataObject.activeSelf)
        {
            Activate();
            dataObject = null;
        }
    }

    private void Activate()
    {
        // 오브젝트 활성 / 비 활성화
        if (!isButton)
        {
            // 스위치 방식
            if (startObject.activeSelf)
            {
                startObject.SetActive(false);
                targetObject.SetActive(true);
            }
            else
            {
                startObject.SetActive(true);
                targetObject.SetActive(false);
            }
        }
        else
        {
            // 버튼 방식
            startObject.SetActive(false);
            targetObject.SetActive(true);
        }
    }
}
