using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GroundScript : MonoBehaviour
{
    [SerializeField] ParticleSystem fireParticles;
    [SerializeField] ParticleSystem smokeParticles;
    // [SerializeField] Image GameOverScreen;

    private void Start()
    {
        // GameOverScreen.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Contacted the ground... ");
            TriggerExplosion(collision);
            // GameOverScreen.gameObject.SetActive(true);
            
            smokeParticles.transform.position = collision.transform.position + new Vector3(0, -0.8f, 100);            
            smokeParticles.Play();
            
        }
    }

    public void TriggerExplosion(Collision2D collision)
    {
        fireParticles.transform.position = collision.transform.position;
        fireParticles.Play();
    }
}
