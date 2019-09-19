using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource audioSource;

    public AudioClip[] attackSound;
    public AudioClip skillSound;

    public bool isEnable = false;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(PlayOne());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(PlayAttack());
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(PlaySkill());
        }
    }
    IEnumerator PlayAttack()
    {
        if (isEnable == false)
        {
            isEnable = true;

            audioSource.clip = attackSound[Random.Range(0, 3)];
            audioSource.Play();
            yield return new WaitForSeconds(0.1f);
            isEnable = false;
        }
    }
    IEnumerator PlaySkill()
    {
        if (isEnable == false)
        {
            isEnable = true;

            audioSource.clip = skillSound;
            audioSource.Play();
            yield return new WaitForSeconds(0.1f);
            isEnable = false;
        }
    }
}
