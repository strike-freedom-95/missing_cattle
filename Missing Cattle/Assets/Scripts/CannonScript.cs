using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D bullet;
    [SerializeField] Transform gunPoint;
    [SerializeField] float bulletForce = 10f;
    [SerializeField] float timerLimit = 10f;
    [SerializeField] ParticleSystem cannonFire;

    float distance;
    float timer = 0;
    GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Debug.Log(angle);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        if (distance < 20)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        timer += 0.1f;
        if (timer > timerLimit)
        {
            timer = 0;
            var bullet1 = Instantiate(bullet, gunPoint.position, gunPoint.rotation);
            bullet1.GetComponent<Rigidbody2D>().velocity = gunPoint.up * bulletForce;
            cannonFire.Play();
        }
    }
}
