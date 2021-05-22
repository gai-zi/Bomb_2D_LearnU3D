using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyBasState        //巡逻，实现抽象类
{
    public override void EnterState(Enemy enemy)
    {
        enemy.animState = 0;
        enemy.SwitchPoint();
    }

    public override void OnUpdate(Enemy enemy)
    {
        if (!enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))  //如果没有运行idle动画
        {
            enemy.animState = 1;
            enemy.MoveToTarget();
        }
        if (Mathf.Abs(enemy.transform.position.x - enemy.targetPoint.position.x) < 0.01f)        //是否达到了目标点
        {
            enemy.TransitionToState(enemy.patrolState);     //敌人到达巡逻点后，再次调用EnterState
        }
        if (enemy.attackList.Count > 0)     //如果有攻击目标
        {
            enemy.TransitionToState(enemy.attackState);
        }
    }
}
