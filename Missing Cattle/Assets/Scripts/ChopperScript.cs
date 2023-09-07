using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ChopperScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    Rigidbody2D rb;
    float timer = 0f;
    bool isPlayerAlive = true;
    BoxCollider2D boxCollider;
    bool isChopperDestroyed = false;
    Animator animator;

    [SerializeField] float chopperSpeed = 3f;
    [SerializeField] float timerLimit = 10f;
    [SerializeField] GameObject missile;
    [SerializeField] Transform firePoint;
    [SerializeField] float missileForce;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (GameObject.Find("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            float dis = Vector2.Distance(transform.position, player.transform.position);
            FlipSprite();
            if (dis > 5 && dis < 15)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, chopperSpeed * Time.deltaTime);
                if (isPlayerAlive && !isChopperDestroyed)
                {
                    FireMissile();
                }                
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            isPlayerAlive = false;
        }
    }

    void FlipSprite()
    {        
        if(player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    void FireMissile()
    {
        timer += 0.1f;
        if (timer > timerLimit)
        {
            timer = 0;
            var bullet1 = Instantiate(missile, firePoint.position, firePoint.rotation);
            bullet1.GetComponent<Rigidbody2D>().velocity = firePoint.up * missileForce;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Bomb") || collision.gameObject.tag.Contains("Player") || collision.gameObject.tag.Contains("Water"))
        {
            isChopperDestroyed = true;
            boxCollider.enabled = false;
            PlayerPrefs.SetInt("TurretDestroyed", PlayerPrefs.GetInt("TurretDestroyed", 0) + 1);
            animator.SetBool("isChopperHit", true);
            Destroy(gameObject, 1f);
        }

        if(collision.gameObject.tag.Contains("Ground") || collision.gameObject.name.Contains("Cannon"))
        {
            rb.AddForce(new Vector2(0f, 100f));
        }
    }
}