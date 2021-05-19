﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator anim;
    private Collider2D coll;
    private Rigidbody2D rb;

    public float startTime;
    public float waitTime;
    public float bombForce;

    [Header("Check")]
    public float radius;
    public float downY;     //Y轴移动距离
    public LayerMask targetLayerMask;       //可以在Inspector窗口多选图层

    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;          //获取游戏时钟
    }

    
    void Update()
    {
        if(Time.time > startTime + waitTime)        //时间超过
        {
            anim.Play("Bomb_Explotion");            //直接播放
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position - new Vector3(0,-downY,0), radius);
    }
    public void Explotion()         //animation Event
    {
        coll.enabled = false;           //将自身的碰撞体取消勾选，防止炸弹的动画受力飞起来
        Collider2D[] aroundObjects = Physics2D.OverlapCircleAll(transform.position, radius, targetLayerMask);

        rb.gravityScale = 0;            //防止失去碰撞体自由下落

        foreach (var item in aroundObjects)
        {
            Vector3 pos = transform.position - item.transform.position;     //力的方向

            item.GetComponent<Rigidbody2D>().AddForce((-pos + Vector3.up) * bombForce , ForceMode2D.Impulse);  //给反方向的力
        }
    }
    public void DestroyBomb()
    {
        Destroy(gameObject);
    }
}
