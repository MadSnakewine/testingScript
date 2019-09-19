using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDeath : FSMState
{

    public override void BeginState()
    {
        Time.timeScale = 1.0f;
        _manager.anim.Play("Cha_Ani_Death");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
