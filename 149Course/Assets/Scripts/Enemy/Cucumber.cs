using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cucumber : Enemy,IDamageable
{
    public Rigidbody2D rb;

    public void GetHit(float damage)
    {
        health -= damage;
        if (health < 1)
        {
            health = 0;
            isDead = true;
        }
        anim.SetTrigger("hit");
    }

    public override void Init()
    {
        base.Init();        //先运行父类的代码
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetOff()    //专属技能：吹灭炸弹 Animator Event
    {
        targetPoint.GetComponent<Bomb>().TurnOff();
    }
}
