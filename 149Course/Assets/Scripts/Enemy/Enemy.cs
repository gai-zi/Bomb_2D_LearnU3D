using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public float speed;

    public Transform pointA,pointB;
    public Transform targetPoint;

    public List<Transform> attackList = new List<Transform>();      //攻击目标的列表

    private BoxCollider2D boxcoll;

    void Start()
    {
        boxcoll = transform.GetChild(0).GetComponent<BoxCollider2D>();
        SwitchPoint();
    }

    
    void Update()
    {
        if (Mathf.Abs(transform.position.x - targetPoint.position.x) < 0.01f)        //是否达到了目标点
            SwitchPoint();

        MoveToTarget();


    }
    public void MoveToTarget()
    {
        //2D条件下朝向目标位置移动
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        FilpDirection();

    }
    public void AttackAction()      //攻击玩家
    {

    }
    public void SkillAction()       //对炸弹使用技能
    {

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
