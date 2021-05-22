﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cucumber : Enemy
{
    public Rigidbody2D rb;

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
