using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerKey
{
    Attack,
    Healing,
    UpperCut,
    Skill,
    Jump
}

public class PlayerKeyMaster : MonoBehaviour
{
    public Dictionary<PlayerKey, KeyCode> keyMaster;
    
    private static PlayerKeyMaster instance;
    public static PlayerKeyMaster GetInstance() // 싱글턴
    {

        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<PlayerKeyMaster>();

            if (instance == null)
            {
                GameObject container = new GameObject("PlayerKeyMaster");

                instance = container.AddComponent<PlayerKeyMaster>();
            }
        }
    }

    void Start()
    {
        keyMaster = new Dictionary<PlayerKey, KeyCode>
        {
            {PlayerKey.Attack, KeyCode.X},
            {PlayerKey.Healing, KeyCode.LeftShift},
            {PlayerKey.UpperCut, KeyCode.UpArrow},
            {PlayerKey.Skill, KeyCode.Z},
            {PlayerKey.Jump, KeyCode.C}
        };
    }

}
