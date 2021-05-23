using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    private int dir;        //方向
    public bool bombAviable;    //是否可以对炸弹操作
    public float toBombForce;   //对炸弹的力
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (transform.position.x > other.transform.position.x)
        {
            dir = -1;   //炸弹在右边
        }
        else
            dir = 1;
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().GetHit(1);
            //踢飞玩家
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir, 1) * toBombForce, ForceMode2D.Impulse);
        }
        if (other.CompareTag("Bomb")  && bombAviable)
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir, 1) * toBombForce, ForceMode2D.Impulse);
        }
    }
}
