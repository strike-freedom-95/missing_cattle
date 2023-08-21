using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CattleScript : MonoBehaviour
{
    bool isBeamed = false;
    Rigidbody2D cow;
    float timer = 0;
    Animator myAnimator;
    AudioSource myAudioSource;
    BoxCollider2D myBoxCollider;

    [SerializeField] AudioClip cowSound;

    // [SerializeField] ParticleSystem goreParticles;

    // Start is called before the first frame update
    void Start()
    {
        cow = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myAudioSource = GetComponent<AudioSource>();
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Beam")
        {
            isBeamed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isBeamed = false;
        if (collision.gameObject.tag == "Water")
        {
            isBeamed = true;
        }
    }

    private void FixedUpdate()
    {
        if (isBeamed)
        {
            //transform.Translate(0, 2 * Time.deltaTime, 0);
            cow.AddForce(new Vector2(0, 12));
            myAnimator.SetBool("isBeamed", true);
        }
        else
        {
            myAnimator.SetBool("isBeamed", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && isBeamed)
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player" && isBeamed == false)
        {
            CattleDeathSequence();
        }

        if (collision.gameObject.tag == "Bomb")
        {
            CattleDeathSequence();
        }
    }

    void ChangeColor()
    {
        // goreParticles.Play();
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }

    void PlayCattleDeathSound()
    {
        myAudioSource.PlayOneShot(cowSound);
    }

    void CattleDeathSequence()
    {
        myAnimator.SetBool("isCowKilled", true);
        myBoxCollider.isTrigger = true;
        cow.gravityScale = 0;
        PlayCattleDeathSound();
        // ChangeColor();
        Destroy(gameObject, 1f);
    }


}
