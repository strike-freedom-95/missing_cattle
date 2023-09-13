using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject player;
    float distance;
    Animator animator;
    AudioSource audioSource;
    bool isMissileDestroyed = false;
    bool disableTracking = false;
    float trackTimer = 0;

    [SerializeField] float range = 20f;
    [SerializeField] float missileSpeed = 3f;
    [SerializeField] float missileDuration = 4f;
    [SerializeField] float trackTimerlimit = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();  
    }

    private void Update()
    {
        // rb.AddForce(transform.forward * 100f) ;
        if (GameObject.Find("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            // Debug.Log(player.gameObject.transform.position);
            distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < range)
            {
                if(!isMissileDestroyed)
                {
                    if (!disableTracking)
                    {
                        Vector2 direction = player.transform.position - transform.position;
                        direction.Normalize();
                        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
                        // rb.AddForce(new Vector2(direction.x * 5, direction.y * 5));
                        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, missileSpeed * Time.deltaTime);
                        // transform.Translate(transform.up * Time.deltaTime * missileSpeed);
                    }
                    else
                    {
                        transform.Translate(new Vector2(0, Time.deltaTime * missileSpeed));
                    }
                }   

            }

        }
        else
        {
            //gunFire.SetActive(false);
        }
    }

    private void Awake()
    {
        Destroy(gameObject, missileDuration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Water")
        {
            animator.SetBool("isSplashed", true);
        }
        else
        {
            animator.SetBool("isContacted", true);
        }
        MissileDestruction();
    }

    void MissileDestruction()
    {        
        transform.Translate(0, 0, 0);
        isMissileDestroyed = true;
        rb.gravityScale = 0f;
        rb.velocity = Vector3.zero;
        rb.bodyType=RigidbodyType2D.Static;
        audioSource.Play();
        Destroy(gameObject, 0.5f);
    }

    private void FixedUpdate()
    {
        trackTimer += 0.1f;
        if(trackTimer > trackTimerlimit)
        {
            disableTracking = true;
            trackTimer = 10;
        }
    }
}
