using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("玩家受伤");
        }
        if (other.CompareTag("Bomb"))
        {
            Debug.Log("炸弹熄灭");
        }
    }
}
