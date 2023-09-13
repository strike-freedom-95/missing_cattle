using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D bullet;
    [SerializeField] Transform gunPoint;
    [SerializeField] float bulletForce = 20f;
    [SerializeField] float timerLimit = 1f;
    [SerializeField] GameObject gunFire;
    [SerializeField] float range = 20f;
    [SerializeField] AudioClip gunFireClip;
    // [SerializeField] ParticleSystem cannonFire;

    float distance;
    float timer = 0;
    GameObject player;
    public double shootTime;
    AudioSource cannonSoundSource;
    bool beyondLimits = false;

    void Start()
    {
        gunFire.SetActive(false);
        cannonSoundSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        if (GameObject.Find("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

            if (!beyondLimits)
            {
                transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            }

            if (angle >= -90 && angle <= 90)
            {
                beyondLimits = false;
                if (distance < range)
                {
                    Shoot();
                    gunFire.SetActive(true);
                }
                else
                {
                    gunFire.SetActive(false);
                }
            }
            else
            {
                beyondLimits = true;
                gunFire.SetActive(false);
            }
        }
        else
        {
            gunFire.SetActive(false);
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
            CannonSounds(0);
        }
    }

    void CannonSounds(int index)
    {
        switch(index)
        {
            case 0:cannonSoundSource.PlayOneShot(gunFireClip);break;
        }
    }
}
