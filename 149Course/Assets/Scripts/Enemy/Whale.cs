using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : Enemy, IDamageable
{
    public float scale;
    public List<GameObject> swalowedBombs;     //吃掉的炸弹列表
    public Transform swalowPoint;
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

    public void Swalow()        //Animation Event
    {
        targetPoint.GetComponent<Bomb>().TurnOff();
        targetPoint.gameObject.SetActive(false);
        swalowedBombs.Add(targetPoint.gameObject);
        targetPoint.SetParent(swalowPoint);     //设置层级
        targetPoint.position = swalowPoint.position;    //设置位置

        //身体变大
        transform.localScale *= scale;
        //吐出炸弹
        if (transform.localScale.y >= 1.9f)
        {
            for (int i = swalowedBombs.Count -1 ; i >= 0; i--)
            {
                swalowedBombs[i].SetActive(true);
                swalowedBombs[i].transform.SetParent(transform.parent.parent);     //升两层父级
                swalowedBombs[i].transform.localScale = Vector3.one;
                swalowedBombs[i].GetComponent<Bomb>().TurnOn(); 
                //找到玩家,将肚子里的炸弹全部扔向玩家
                if (FindObjectOfType<PlayerController>().gameObject.transform.position.x - transform.position.x < 0)
                    swalowedBombs[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * Random.Range(5, 8f), ForceMode2D.Impulse);
                else
                    swalowedBombs[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * Random.Range(5, 8f), ForceMode2D.Impulse);

                swalowedBombs.Remove(swalowedBombs[i]);
            }
            transform.localScale = new Vector3(-1,1,1);
        }
    }
}
