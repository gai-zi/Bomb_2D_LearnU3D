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

    
    void Update()
    {
        anim.SetFloat("speed",Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("jump", controller.isJump);
        anim.SetBool("ground", controller.isGround);
            
        if(!controller.isJump && rb.velocity.x!=0)      //显示跑动灰尘特效FX
            transform.GetChild(1).gameObject.SetActive(true);
        else
            transform.GetChild(1).gameObject.SetActive(false);
    }
}
