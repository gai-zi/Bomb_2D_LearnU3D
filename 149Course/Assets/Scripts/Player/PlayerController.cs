using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;



    public float speed;
    public float jumpForce;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;

    [Header("States Check")]
    public bool isGround;
    public bool isJump;
    public bool canJump = false;

    [Header("Jump FX")]
    public GameObject jumpFX;
    public GameObject landFX;

    [Header("Attack Settings")]
    public GameObject bombPrefab;
    public float nextAttack = 0;
    public float attackRate;        //攻击频率

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckInput();
    }
    void FixedUpdate()      //0.02s
    {
        PhysicsCheck();             
        Movement();
        Jump();
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        if (horizontalInput != 0)
        {
            transform.localScale = new Vector3(horizontalInput, 1, 1);
        }
    }

    void CheckInput()
    {
        if (Input.GetButtonDown("Jump") && isGround)
        {
            canJump = true;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Attack();
        }
    }
    void Jump()
    {
        if (canJump)            //能跳的条件
        {
            isJump = true;
            jumpFX.SetActive(true);     //显示特效S
            jumpFX.transform.position = transform.position + new Vector3(0, -0.5f, 0);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;
        }
    }

    public void Attack()
    {
        if(Time.time > nextAttack)
        {
            Instantiate(bombPrefab, transform.position, bombPrefab.transform.rotation);
            nextAttack = Time.time + attackRate;
        }
    }

    void PhysicsCheck()             //物理检测
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position,checkRadius,groundLayer);
        if (isGround)
        {
            rb.gravityScale = 1;
            isJump = false;
        }
        else
            rb.gravityScale = 4;        //为了能尽快下落
    }

    public void LandFX()            //Animation Event 落地的第一帧调取该方法，生成特效
    {
        landFX.SetActive(true);
        landFX.transform.position = transform.position + new Vector3(0, -0.7f, 0);
    }
    public void OnDrawGizmos()  //画出检测范围
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
