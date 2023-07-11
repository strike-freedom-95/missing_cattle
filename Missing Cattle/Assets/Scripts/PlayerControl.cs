using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    float horizontalMove;
    float verticalMove;
    float moveSpeed = 1.5f;

    [SerializeField] Rigidbody2D player;

    void Start()
    {
        
    }

    
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        verticalMove = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        if (transform.position.y != 0)
        {
            transform.Translate(horizontalMove, verticalMove, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
            Debug.Log("Game Over");
        }
    }
}
