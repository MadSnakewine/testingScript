using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpEvent : MonoBehaviour
{
    private static HelpEvent instance;
    public static HelpEvent GetInstance() // 싱글턴
    {
        if (instance == null)
        {
            instance = FindObjectOfType<HelpEvent>();

            if (instance == null)
            {
                GameObject container = new GameObject("HelpEvent");

                instance = container.AddComponent<HelpEvent>();
            }
        }
        return instance;
    }


    public bool MoveCorrection(Vector2 dir, Vector3 thispos , float dis) // 해당방향의 벽이있으면 false를반환
    {
        float maxDistance = dis;
        Vector3 pos = thispos;
        pos.y += 0.5f;

        if (Physics2D.Raycast(pos, dir, maxDistance, (1 << 12)))
        {
            return false;
        }
        return true;
    }


}
