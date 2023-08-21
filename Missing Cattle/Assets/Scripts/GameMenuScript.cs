using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;

public class GameMenuScript : MonoBehaviour
{
    CanvasRenderer canvas;
    bool isGamePaused = false;
    bool isGameComplete = false;
    int cowCount = 0;
    int totalCows = 0;
    int cowTaken = 0;
    int cowDeath = 0;

    AudioSource audioSource;

    [SerializeField] Image gameMenu;
    [SerializeField] Image resultDisplay;
    [SerializeField] TextMeshProUGUI cowCountGUI;
    // Start is called before the first frame update
    void Start()
    {
        gameMenu.gameObject.SetActive(false);
        resultDisplay.gameObject.SetActive(false);
        Cursor.visible = false;
        totalCows = GameObject.FindGameObjectsWithTag("Cattle").Length;
        audioSource = GetComponent<AudioSource>();
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        cowCount = GameObject.FindGameObjectsWithTag("Cattle").Length;
        string cowCountDisplay = cowCount.ToString() + "/" + totalCows.ToString();
        cowCountGUI.text = cowCountDisplay;

        if(cowCount == 0)
        {
            ShowGameResult();
        }

        if(Input.GetKeyDown(KeyCode.Escape) && !isGamePaused)
        {
            GamePause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isGamePaused)
        {
            GameResume();
        }
    }

    void GamePause()
    {
        isGamePaused = true;
        Cursor.visible = true;
        gameMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
        audioSource.Pause();
    }

    void GameComplete()
    {
        Cursor.visible = true;
        resultDisplay.gameObject.SetActive(true);
        Time.timeScale = 0;
        audioSource.Pause();
    }

    public void GameResume()
    {
        isGamePaused = false;
        Cursor.visible = false;
        gameMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        audioSource.Play();
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
        audioSource.Stop();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game Scene");
        Time.timeScale = 1;
    }

    void ShowGameResult()
    {
        GameComplete();
    }
}
