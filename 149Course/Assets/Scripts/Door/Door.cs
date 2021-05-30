using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        
        GameManager.instance.IsExitDoor(this);

        coll.enabled = false;
    }

    public void OpenDoor()
    {
        anim.Play("open");
        coll.enabled = true;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.NextScene();
        }
    }
}
