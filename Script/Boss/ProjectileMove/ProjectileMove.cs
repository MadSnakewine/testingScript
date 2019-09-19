using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed;
    public float damage;

    public GameObject effect;

    public bool stone;
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            BossHelpEvent.GetInstance().Damage(damage, true);

            GameObject temp = (GameObject)Instantiate(effect, this.transform.position, Quaternion.identity);
            Destroy(temp, 0.2f);
            this.gameObject.SetActive(false);
        }
        if(stone)
        {
            if (collision.gameObject.layer == 15)
            {
                GameObject temp = (GameObject)Instantiate(effect, this.transform.position, Quaternion.identity);
                Destroy(temp, 0.2f);
                this.gameObject.SetActive(false);
            }
        }
        else if (collision.gameObject.layer == 12 || collision.gameObject.layer == 14)
        {
            GameObject temp = (GameObject)Instantiate(effect, this.transform.position, Quaternion.identity);
            Destroy(temp, 0.2f);
            this.gameObject.SetActive(false);
        }
    }

}
