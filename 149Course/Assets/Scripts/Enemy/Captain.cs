using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captain : Enemy, IDamageable
{
    SpriteRenderer sprite;
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

    public override void SkillAction()
    {
        base.SkillAction();           //基于原来的判断

        if (anim.GetCurrentAnimatorStateInfo(1).IsName("skill"))
        {
            sprite.flipX = true;        //使用SpriteRenderer组件将图片翻转
            if (transform.position.x > targetPoint.position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.right, speed * 2 * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.left, speed * 2 * Time.deltaTime);
            }
        }
        else
            sprite.flipX = false;
    }
    public override void Init()
    {
        base.Init();

        sprite = GetComponent<SpriteRenderer>();

    }
    public override void Update()
    {
        base.Update();

        if(animState == 0 || !anim.GetCurrentAnimatorStateInfo(1).IsName("skill"))
        {
            sprite.flipX = false;
        }
    }
}
