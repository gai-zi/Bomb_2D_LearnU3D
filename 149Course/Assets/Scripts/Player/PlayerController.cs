using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour,IDamageable
{
    private Rigidbody2D rb;
    private Animator anim;
    private FixedJoystick joystick;

    public float speed;
    public float jumpForce;
    [Header("Player State")]
    public float health;
    public bool isDead;
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
        anim = GetComponent<Animator>();
        joystick = FindObjectOfType<FixedJoystick>();

        GameManager.instance.IsPlayer(this);        //给单例的GameManger中player赋值

        health = GameManager.instance.LoadHealth();
        UIManager.instance.UpdateHealth(health);
    }

    void Update()
    {

        anim.SetBool("dead", isDead);       //实时将死亡状态同步到动画中
        if (isDead)     //如果死亡
            return;
        CheckInput();
        
    }
    void FixedUpdate()      //0.02s
    {
        if (isDead)     //如果死亡
        {
            rb.velocity = Vector2.zero;     //速度为0
            return;
        }
        PhysicsCheck();             
        Movement();
        Jump();
    }

    void Movement()
    {
        //键盘操作
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        //操作杆
        horizontalInput = joystick.Horizontal;
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        //if (horizontalInput != 0)
        //    transform.localScale = new Vector3(horizontalInput, 1, 1);
        if (horizontalInput > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (horizontalInput < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);

            
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
    public void Jump()
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
    public void ButtonJump()
    {
        if(isGround)
            canJump = true;
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

    public void GetHit(float damage)        //实现接口
    {   if (!anim.GetCurrentAnimatorStateInfo(1).IsName("player_hit"))   //如果没播放受伤动画
        {
            health -= damage;
            if (health < 1)
            {
                health = 0;
                isDead = true;
                this.transform.tag = "Untagged";        //更改tag，防止死后一直攻击人物
            }
            anim.SetTrigger("hit");

            UIManager.instance.UpdateHealth(health);
        }
    }
}
