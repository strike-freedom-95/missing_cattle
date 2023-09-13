using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameFinishScript : MonoBehaviour
{
    [SerializeField] GameObject BarrierTiles;
    [SerializeField] GameObject tankSprite;

    GameObject tank;
    bool battleStarted = false;

    private void Start()
    {
        BarrierTiles.SetActive(false);
    }

    private void Update()
    {
        tank = GameObject.FindGameObjectWithTag("Boss");
        if (!tankSprite.IsDestroyed() && battleStarted)
        {
            BarrierTiles.SetActive(true);
        }
        else if (tankSprite.IsDestroyed())
        {
            BarrierTiles.SetActive(false);
        }
        else
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            BarrierTiles.SetActive(true);
            battleStarted = true;
        }
    }
}
