using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadingSceneManager : MonoBehaviour
{
    bool IsDone = false;
    AsyncOperation async_operation;
    public string Next_View;
    bool update = false;

    private void Start()
    {
        Screen.SetResolution(1280, 720, true);
        Application.targetFrameRate = 60;
        StartCoroutine(StartLoad(Next_View)); // 여기로 넘어감 게이지가 다차면
    }

    void Update()
    {
        if (update)
        {
            update = false;
            Application.LoadLevel(Next_View);
        }
    }

    public IEnumerator StartLoad(string strSceneName)
    {
        async_operation = Application.LoadLevelAsync(strSceneName);
        async_operation.allowSceneActivation = false;

        if (IsDone == false)
        {
            IsDone = true;

            while (async_operation.progress < 0.9f)
            {
                yield return true;
            }
        }
    }

    public void NextScene()
    {
        update = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            update = true;
        }
    }
}