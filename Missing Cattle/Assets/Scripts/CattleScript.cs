using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CattleScript : MonoBehaviour
{
    bool isBeamed = false;
    Rigidbody2D cow;
    float timer = 0;

    [SerializeField] ParticleSystem goreParticles;

    // Start is called before the first frame update
    void Start()
    {
        cow = GetComponent<Rigidbody2D>();
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
        if (collision.gameObject.tag == "Bullet")
        {
            ChangeColor();
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
            ChangeColor();
        }
    }

    void ChangeColor()
    {
        goreParticles.Play();
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        Destroy(gameObject, 0.1f);
    }
}
