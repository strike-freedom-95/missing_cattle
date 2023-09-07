using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WarShipScript : MonoBehaviour
{
    Animator animator;
    BoxCollider2D boxCollider;
    float timer = 0;
    GameObject player;
    float distance;
    bool isShipDestroyed = false;

    [SerializeField] Rigidbody2D missile;
    [SerializeField] Transform firePoint;
    [SerializeField] float missileForce = 20f;
    [SerializeField] float timerLimit = 5f;
    [SerializeField] float range = 20f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (GameObject.Find("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            distance = Vector2.Distance(transform.position, player.transform.position);
            if ((distance < range) && !isShipDestroyed)
            {
                FireMissile();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Bomb") || collision.gameObject.tag.Contains("Player"))
        {
            isShipDestroyed = true;
            boxCollider.enabled = false;
            PlayerPrefs.SetInt("TurretDestroyed", PlayerPrefs.GetInt("TurretDestroyed", 0) + 1);
            animator.SetBool("isBoatDestroyed", true);
            Destroy(gameObject, 1f);
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
}
