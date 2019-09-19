using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIMangaer : MonoBehaviour
{
    public PlayerState state;

    public List<GameObject> skillUI = new List<GameObject>();

    private static PlayerUIMangaer instance;
    public static PlayerUIMangaer GetInstance() // 싱글턴
    {
        if (instance == null)
        {
            instance = FindObjectOfType<PlayerUIMangaer>();

            if (instance == null)
            {
                GameObject container = new GameObject("PlayerUIMangaer");

                instance = container.AddComponent<PlayerUIMangaer>();
            }
        }
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}


