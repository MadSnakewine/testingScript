using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn1st : MonoBehaviour
{

    public GameObject spawner;

    public Transform mob;

    bool spawn = false;

    void Start()
    {
        spawner = GameObject.Find("MobSpawner1st");

    }

    void Update()
    {
        if (spawn == true)
        {
            StartCoroutine("SpawnDelay");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spawn = true;
            this.transform.Find("SpawnPoint1").gameObject.SetActive(true);
            this.transform.Find("SpawnPoint2").gameObject.SetActive(true);
        }
    }

    IEnumerator SpawnDelay()
    {
        spawn = false;
        yield return new WaitForSeconds(0.5f);
        // 소환

        // 충돌감지용 박스 삭제(중복발동 방지)
        gameObject.SetActive(false);
    }
}
