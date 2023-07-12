using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    float horizontalMove;
    float verticalMove;
    float moveSpeed = 1.5f;
    bool isBeamActive = false;

    [SerializeField] Rigidbody2D player;
    [SerializeField] GameObject beam;
    [SerializeField] ParticleSystem BeamParticle;

    void Start()
    {
        beam.SetActive(false);
    }


    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        verticalMove = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;        

        if (Input.GetKey(KeyCode.E))
        {
            beam.SetActive(true);
            isBeamActive = true;
            horizontalMove = 0;
            verticalMove = 0;
        }

        if(Input.GetKeyUp(KeyCode.E))
        {
            beam.SetActive(false);
            isBeamActive = false;
        }
        transform.Translate(horizontalMove, verticalMove, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Water")
        {
            Destroy(gameObject);
            Debug.Log("Game Over");
        }
        if (collision.gameObject.tag == "Cattle" && isBeamActive)
        {
            BeamParticle.Play();
        }
    }
}
