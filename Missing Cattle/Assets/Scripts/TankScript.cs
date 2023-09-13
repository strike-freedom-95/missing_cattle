using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TankScript : MonoBehaviour
{
    GameObject player;
    bool isPlayerAlive = true;
    bool isTankDestroyed = false;
    float timer = 0f;
    Rigidbody2D rb;
    Animator animator;
    
    [SerializeField] int hitsLimit = 10;
    [SerializeField] float tankSpeed = 2f;
    [SerializeField] GameObject tankBody;
    [SerializeField] float timerLimit = 10f;
    [SerializeField] GameObject missile;
    [SerializeField] Transform missileLaunchPoint;
    [SerializeField] float missileForce;
    [SerializeField] GameObject gun1;
    [SerializeField] GameObject gun2;
    [SerializeField] GameObject gun3;
    [SerializeField] AudioClip kaboom;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = tankBody.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // MissileEvent();
    }

    private void FixedUpdate()
    {
        if (GameObject.Find("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            float dis = Vector2.Distance(transform.position, player.transform.position);
            FlipSprite();
            if (dis < 40)
            {
                Vector3 playerRefPoint = new Vector3(player.transform.position.x, 0, 0);
                transform.position = Vector3.MoveTowards(transform.position, playerRefPoint, tankSpeed * Time.deltaTime);
                
                animator.SetBool("isTankmoving", true);
                if (isPlayerAlive && !isTankDestroyed)
                {
                    // FireMissile();
                }
            }
            else
            {
                rb.velocity = Vector3.zero;
                animator.SetBool("isTankmoving", false);
            }
        }
        else
        {
            isPlayerAlive = false;
        }
    }

    void FlipSprite()
    {
        if (player.transform.position.x > transform.position.x)
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
            var missileVar = Instantiate(this.missile, missileLaunchPoint.position, missileLaunchPoint.rotation);
            missileVar.GetComponent<Rigidbody2D>().velocity = missileLaunchPoint.right * missileForce * -1;
        }
    }

    IEnumerator MissileEvent()
    {
        yield return new WaitForSecondsRealtime(timerLimit);
        FireMissile();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bomb")
        {
            AudioSource.PlayClipAtPoint(kaboom, Camera.main.transform.position);
            hitsLimit--;
            if(hitsLimit < 0)
            {
                Destroy(gun1);
                Destroy(gun2);
                Destroy(gun3);
                animator.SetBool("isTankDestroyed", true);
                Destroy(gameObject, 0.5f);
            }
        }
    }
}
