using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D Player;
    [SerializeField] Image gameOverImage;    
    
    CinemachineVirtualCamera VirtualCamera;
    bool noPlayerFound = false;
    float baseOrthoSize = 10f;
    float timer = 1f;

    private void Start()
    {
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();
        gameOverImage.gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        VirtualCamera.m_Lens.OrthographicSize = baseOrthoSize;
        if (noPlayerFound)
        {            
            if (VirtualCamera.m_Lens.OrthographicSize > 3)
            {
                baseOrthoSize -= 0.05f;
            }
            timer -= 0.05f;
            if(timer < 0)
            {
                gameOverImage.gameObject.SetActive(true);
                timer = 0;
            }
        }
    }

    private void Update()
    {
        if (Player.IsDestroyed())
        {
            noPlayerFound = true;
        }
    }
}
