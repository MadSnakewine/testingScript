using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public float warkSpeed;
    public float jumpForce;
    public float upperCutJumpforce;

    public Slider hp;
    public Slider Stamina; //스태미너

    public int skillCount; // 질풍참 남은횟수
    public float skillCoolingTimeMax;

    [HideInInspector] public float skillCoolingTime; // 질풍참 쿨탐

    public int attackDamage;
    public int skillDamage;

    // Start is called before the first frame update
    void Start()
    {
        skillCoolingTime = skillCoolingTimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        skillCoolingTimeDown();
    }

    void skillCoolingTimeDown()
    {
        // 카운트가 3이아니면
        //쿨타임새고 다되면 카운트1추가
        if (skillCount < 5)
        {
            skillCoolingTime -= Time.deltaTime;

            if (skillCoolingTime <= 0)
            {
                SkillCountUp();
                skillCoolingTime = skillCoolingTimeMax;
            }
        }
    }

    //skill ui증가
    public void SkillCountUp()
    {
        PlayerUIMangaer.GetInstance().skillUI[skillCount].SetActive(true);
        skillCount++;
    }

    public void SkillCountDown()
    {
        skillCount--;
        PlayerUIMangaer.GetInstance().skillUI[skillCount].SetActive(false);

    }
    //skill ui다운


}

