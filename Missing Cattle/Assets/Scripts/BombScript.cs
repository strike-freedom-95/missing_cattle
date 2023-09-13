using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    Animator animator;
    Rigidbody2D myBomb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        myBomb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Cattle" || collision.gameObject.tag == "Boss") 
        {
            // AudioSource.PlayClipAtPoint(kaboom, Camera.main.transform.position);
            animator.SetBool("isCollided", true);
            Destroy(gameObject);
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            Debug.Log("Splash");
            myBomb.velocity = Vector2.zero;
            myBomb.gravityScale = 0.7f;
            Destroy(gameObject, 1);
        }
    }

    private void Awake()
    {
        Destroy(gameObject, 5f);
    }
}
