using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterScript : MonoBehaviour
{
    [SerializeField] ParticleSystem waterParticles;
    // [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] Image GameOverScreen;

    private void Start()
    {
        GameOverScreen.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameOverScreen.gameObject.SetActive(true);
            waterParticles.transform.position = collision.transform.position + new Vector3(0, -0.8f, 0);
            waterParticles.Play();
        }
    }
}
