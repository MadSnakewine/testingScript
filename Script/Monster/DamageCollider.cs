using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageCollider : MonoBehaviour
{
    public float hitTime;           // 충격 시간
    public string[] collisionTag;   // 충돌 테그
    public Slider hp;
    public float damage;
    public GameObject player;

    void Awake()
    {
        hp = FindObjectOfType<PlayerState>().hp;
        player = GameObject.Find("Player");
    }

    public void Setting(int _damage, float _hitTime, string[] _collisionTag)
    {
        // 공격박스 설정
        hitTime = _hitTime;
        collisionTag = _collisionTag;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌 테그에 오브젝트가 충돌한 경우
        for (int i = 0; i < collisionTag.Length; i++)
            if (collision.tag == collisionTag[i])
            // 공격 
            {
                //Debug.Log("몹공격");
                if (player.GetComponentInChildren<PlayerFSM>().invincibility == false)
                {
                    //Debug.Log("공격닿음");
                    hp.value -= damage;
                    FindObjectOfType<PlayerFSM>().ChScript(Player_State.Damaged);
                }
            }
    }
}
