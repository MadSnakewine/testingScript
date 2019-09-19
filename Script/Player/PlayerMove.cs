using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MoveType
{
    Idle = 0,
    Walk,
    WalkDown
}

public class PlayerMove : MonoBehaviour
{
    PlayerFSM fsm;
    Rigidbody2D rigidbody;

    float player_Speed;
    public float _Speed { get { return player_Speed; } set { player_Speed = value; } }

    [HideInInspector] public Vector3 move = Vector3.zero;

    PlayerState state;

    [HideInInspector] public Animator anim;

    [HideInInspector] public bool groundChack = true;

    bool doubleJump = false;
    [HideInInspector] public bool moveOn;
    PlayerAttack attack;

    public GameObject moveEffect;

    // Start is called before the first frame update
    void Start()
    {
        fsm = transform.GetChild(0).GetComponent<PlayerFSM>();
        state = transform.GetChild(0).GetComponent<PlayerState>();
        anim = transform.GetComponent<Animator>();
        rigidbody = transform.GetComponent<Rigidbody2D>();
        attack = transform.GetChild(0).GetComponent<PlayerAttack>();
        moveOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveOn)
        {
            Move();

            if (groundChack)
                Jump();
            else
            {
                if (doubleJump)
                {
                    DoubleJump();
                }
            }
        }
        else
        {
            moveEffect.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        Ground();
    }

    void Move() // 이동
    {
        move = this.transform.position;

        if (Input.GetKey(KeyCode.RightArrow)) // 오른쪽이동
        {
            if (HelpEvent.GetInstance().MoveCorrection(Vector2.right, this.transform.position , 0.3f))
            {
                if(groundChack)
                    moveEffect.SetActive(true);
                anim.SetFloat("Forward", 1);
                move.x += state.warkSpeed * Time.deltaTime;
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) // 왼쪽이동
        {
            if (HelpEvent.GetInstance().MoveCorrection(Vector2.left, this.transform.position, 0.3f))
            {
                if(groundChack)
                    moveEffect.SetActive(true);
                anim.SetFloat("Forward", 1);
                move.x -= state.warkSpeed * Time.deltaTime;
                this.transform.rotation = Quaternion.Euler(0, -180, 0);
            }
        }
        else
        {
            moveEffect.SetActive(false);
            anim.SetFloat("Forward", 0);
        }

        this.transform.position = move;
    }

    void Jump() // 점프
    {
        if (Input.GetKeyDown(PlayerKeyMaster.GetInstance().keyMaster[PlayerKey.Jump]))
        {
            rigidbody.velocity = new Vector3(0, state.jumpForce, 0);
            GetComponent<BoxCollider2D>().isTrigger = true;
            anim.SetBool("Jump", true);

            if (!attack.jumpAttackCheck)
                attack.jumpAttackCheck = true;
        }
    }

    void DoubleJump() // 더블점프
    {
        if (Input.GetKeyDown(PlayerKeyMaster.GetInstance().keyMaster[PlayerKey.Jump]))
        {
            anim.SetBool("DoubleJump", true);
            anim.Play("DoubleJump");
            rigidbody.velocity = new Vector3(0, state.jumpForce, 0);
            doubleJump = false;

            if (!attack.jumpAttackCheck)
                attack.jumpAttackCheck = true;
        }
    }

    void Ground()
    {
        float maxDistance = 0.01f;
        Vector3 pos = this.transform.position;
        pos.y -= 1.29f;
        Debug.DrawRay(pos, Vector2.down * maxDistance);
        if (Physics2D.Raycast(pos, Vector2.down, maxDistance, (1 << 12) | (1 << 14))) // 지상이라면
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.1f)
                return;

            GetComponent<BoxCollider2D>().isTrigger = false;

            if (anim.GetBool("Jump"))
                anim.SetBool("Jump", false);
            else if (anim.GetBool("DoubleJump"))
                anim.SetBool("DoubleJump", false);
            else if (anim.GetBool("Fall"))
                anim.SetBool("Fall", false);

            groundChack = true;
            doubleJump = true;
        }
        else // 공중이라면
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            groundChack = false;
            moveEffect.SetActive(false);

            if (!anim.GetBool("Jump"))
            {
                anim.SetBool("Fall", true);
            }

        }
    }

  
}