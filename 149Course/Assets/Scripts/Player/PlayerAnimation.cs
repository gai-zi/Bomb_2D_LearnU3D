using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    //访问PlayerController中的变量；
    private PlayerController controller;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();
    }

    
    void FixedUpdate()
    {
        //if (velocityY == 0)       //改bug，真正错误原因在Animator中转换条件勾选了Has Exit Time
        //    velocityY = -0.1f;
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetFloat("speed",Mathf.Abs(rb.velocity.x));
        anim.SetBool("jump", controller.isJump);
        anim.SetBool("ground", controller.isGround);
            
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerRun"))      //显示跑动灰尘特效FX
            transform.GetChild(1).gameObject.SetActive(true);
        else
            transform.GetChild(1).gameObject.SetActive(false);
    }
}
