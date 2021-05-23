﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaldPirate : Enemy,IDamageable
{
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
    }
    public void KickBomb()    //专属技能：踢走炸弹 Animation Event
    {
        
    }

}
