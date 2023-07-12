using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GroundScript : MonoBehaviour
{
    [SerializeField] ParticleSystem fireParticles;
    [SerializeField] ParticleSystem smokeParticles;
    // [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] Image GameOverScreen;

    private void Start()
    {
        GameOverScreen.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameOverScreen.gameObject.SetActive(true);
            fireParticles.transform.position = collision.transform.position + new Vector3(0, -0.8f, 100);
            smokeParticles.transform.position = collision.transform.position + new Vector3(0, -0.8f, 100);
            fireParticles.Play();
            smokeParticles.Play();            
        }
    }
}