using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    Vector3 tPlayer; // 케릭터의 위치를 임시로저장
    public float z, y; // 카메라의 z y값

    public float speed;

    public bool moveChack = false;

    public BoxCollider2D bound;

    Vector3 minBound;
    Vector3 maxBound;

    float haifWidth;
    float haifHight;

    Camera theCamera;

    float amount;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        theCamera = GetComponent<Camera>();

        haifHight = theCamera.orthographicSize; // 카메라의 반높이
        haifWidth = haifHight * Screen.width / Screen.height; // 카메라의 반너비 , Screen은 해상도를나타냄
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Move();

        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            amount = 0;
        }
    }

    void Move()
    {
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;

        tPlayer = player.transform.position;

        tPlayer.y += y;
        tPlayer.z = z;
        transform.position = Vector3.Lerp(transform.position, tPlayer, speed);

        float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + haifWidth, maxBound.x - haifWidth);
        float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + haifHight, maxBound.y - haifHight);

        tPlayer = new Vector3(clampedX, clampedY, this.transform.position.z);
        tPlayer += (Vector3)Random.insideUnitCircle * amount;

        this.transform.position = Vector3.Lerp(this.transform.position, tPlayer, speed * 4);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            moveChack = true;
        }
    }

    public void CameraShake(float amount, float timer)
    {
        this.amount = amount;
        this.timer = timer;
    }

}
