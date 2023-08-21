using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    CapsuleCollider2D capsuleCollider;

    [SerializeField] GameObject pivot;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag.Contains("Bomb"))
        {
            capsuleCollider.enabled = false;
            audioSource.Play();
            Destroy(pivot);
            animator.SetBool("isTowerBombed", true);            
            Destroy(gameObject, 2);
        }
    }
}
