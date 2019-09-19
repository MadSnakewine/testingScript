using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharLibrary
{
    public struct Rect
    {
        public float left;
        public float right;
        public float up;
        public float down;
        public Vector2 position;
        public Vector2 size;

        public Rect(Vector2 _position, Vector2 _size)
        {
            position = _position;
            size = _size;
            left = position.x - (size.x * 0.5f);
            right = position.x + (size.x * 0.5f);
            up = position.y + (size.y * 0.5f);
            down = position.y - (size.y * 0.5f);
        }
    }

    [System.Serializable]
    public class HitCollider
    {
        public Vector2 position;                    // 충돌박스 위치
        public Vector2 size = new Vector2(1, 1);    // 충돌박스 크기
        public int damage;                          // 공격력
        public float hitTime;                       // 충격 유지 시간
        public float startTime;                     // 충돌박스 출력 시작 시간
        public float endTime;                       // 충돌박스 출력 종료 시간
        public string[] collisionTag;               // 충돌 테그
    }
    [System.Serializable]
    public class HitColliderEffect : HitCollider
    {
        public GameObject Effect;                   // 이펙트 오브젝트
    }

    [System.Serializable]
    public class CannonController
    {
        public Vector2 position;                    // 발사 위치
        public int damage;                          // 공격력
        public float speed;                         // 총알 속도
        public float activeTime;                    // 총알 유효 시간
        public float hitTime;                       // 충격 유지 시간
        public float startTime;                     // 총알 생성 시간
        public string[] collisionTag;               // 충돌 테그
        public GameObject bullet;                   // 총알 오브젝트
    }
    [System.Serializable]
    public class CannonControllerEffect : CannonController
    {
        public GameObject Effect;                   // 이펙트
    }
}
