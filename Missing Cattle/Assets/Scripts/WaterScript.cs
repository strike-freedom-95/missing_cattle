using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterScript : MonoBehaviour
{
    [SerializeField] ParticleSystem waterParticles;
    // [SerializeField] TextMeshProUGUI gameOverText;

    private void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            // GameOverScreen.gameObject.SetActive(true);
            waterParticles.transform.position = collision.transform.position;
            waterParticles.Play();
        }
    }
}
