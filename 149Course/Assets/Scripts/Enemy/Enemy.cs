using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyBasState currentState;

    public Animator anim;
    public int animState;

    [Header("Movement")]
    public float speed;
    public Transform pointA,pointB;
    public Transform targetPoint;

    [Header("Attack Settings")]
    public float attackRate;        //攻击间隔
    private float nextAttack = 0;   //下次攻击时间
    public float attackRange, skillRange;       //普通攻击和技能攻击触发范围


    public List<Transform> attackList = new List<Transform>();      //攻击目标的列表

    public PatrolState patrolState = new PatrolState();             //创建巡逻状态对象
    public AttackState attackState = new AttackState();             //创建攻击状态对象

    public virtual void Init()
    {
        anim = GetComponent<Animator>();
    }
    public void Awake()
    {
        Init();
    }
    void Start()
    {
        TransitionToState(patrolState);
    }

    void Update()
    {
        currentState.OnUpdate(this);
        anim.SetInteger("state", animState);        //给动画参数赋值
    }
    public void TransitionToState(EnemyBasState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
    public void MoveToTarget()
    {
        //2D条件下朝向目标位置移动
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        FilpDirection();

    }
    public void AttackAction()      //攻击玩家
    {
        if(Vector2.Distance(transform.position,targetPoint.position) < attackRange)
        {
            if(Time.time > nextAttack)
            {
                //播放攻击动画
                anim.SetTrigger("attack");
                nextAttack = Time.time + attackRate;
            }
        }
    }
    public virtual void SkillAction()       //对炸弹使用技能
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < skillRange)
        {
            if (Time.time > nextAttack)
            {
                //播放攻击动画
                anim.SetTrigger("skill");
                nextAttack = Time.time + attackRate;
            }
        }
    }

    public void FilpDirection()     //进行图片的翻转
    {
        if(transform.position.x < targetPoint.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);      //欧拉角可以直接更改rotation
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    public void SwitchPoint()       //切换目标点
    {
        if(Mathf.Abs(pointA.position.x - transform.position.x)>Mathf.Abs(pointB.position.x - transform.position.x))
        {
            targetPoint = pointA;
        }
        else
        {
            targetPoint = pointB;
        }
    }
    public void OnTriggerStay2D(Collider2D collision)  //自带函数
    {
        if(!attackList.Contains(collision.transform))   //如果没包含这个transform，添加进列表
            attackList.Add(collision.transform);
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        attackList.Remove(collision.transform);
    }
}
