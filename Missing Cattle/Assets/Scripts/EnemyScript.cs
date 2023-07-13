using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] ParticleSystem fireParticles;
    [SerializeField] ParticleSystem smokeParticles;
    [SerializeField] Image GameOverScreen;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameOverScreen.gameObject.SetActive(true);
            fireParticles.transform.position = collision.transform.position + new Vector3(0, -0.8f, 100);
            smokeParticles.transform.position = collision.transform.position + new Vector3(0, -0.8f, 100);
            fireParticles.Play();
            smokeParticles.Play();
        }
    }
}
