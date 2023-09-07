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
    [SerializeField] AudioClip gunSound;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(PlayerPrefs.GetInt("TurretDestroyed", 0));
        if (collision.gameObject.tag.Contains("Bomb") || collision.gameObject.tag.Contains("Player"))
        {
            PlayerPrefs.SetInt("TurretDestroyed", PlayerPrefs.GetInt("TurretDestroyed", 0) + 1);
            capsuleCollider.enabled = false;
            AudioSource.PlayClipAtPoint(gunSound, Camera.main.transform.position);
            // audioSource.Play();
            Destroy(pivot);
            animator.SetBool("isTowerBombed", true);
            Destroy(gameObject, 0.5f);
        }
    }
}
