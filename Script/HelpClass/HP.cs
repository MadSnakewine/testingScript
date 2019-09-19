using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    SpriteRenderer sprite;
    [HideInInspector] public float maxHp;
    public float hp;

    private void Start()
    {
        maxHp = hp;
        sprite = GetComponent<SpriteRenderer>();
    }

    public void HpDown(float Damage)
    {
        hp -= Damage;
        StartCoroutine("SpriteCh");
    }

    IEnumerator SpriteCh()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

}
