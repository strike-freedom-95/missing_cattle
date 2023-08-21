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
    float initTime = 100f;
    float timerValue = 1;


    AudioSource audioSource;

    [SerializeField] Image gameMenu;
    [SerializeField] Image resultDisplay;
    [SerializeField] TextMeshProUGUI cowCountGUI;
    [SerializeField] Slider timerSlider;
    [SerializeField] float totalTime = 100f;
    [SerializeField] TextMeshProUGUI scoreBoard;
    // Start is called before the first frame update
    void Start()
    {
        gameMenu.gameObject.SetActive(false);
        resultDisplay.gameObject.SetActive(false);
        Cursor.visible = false;
        totalCows = GameObject.FindGameObjectsWithTag("Cattle").Length;
        audioSource = GetComponent<AudioSource>();
        Time.timeScale = 1.0f;
        timerSlider.value = 1;
        initTime = totalTime;
    }

    // Update is called once per frame
    void Update()
    {
        timerSlider.value = timerValue;
        checkTime();
        cowCount = GameObject.FindGameObjectsWithTag("Cattle").Length;
        string cowCountDisplay = PlayerPrefs.GetInt("Score", 0).ToString() + "/" + totalCows.ToString();
        cowCountGUI.text = cowCountDisplay;
        scoreBoard.text = PlayerPrefs.GetInt("Score", 0).ToString();

        if (cowCount == 0)
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

    private void FixedUpdate()
    {
        initTime -= 0.1f;
        // Debug.Log((float)(initTime / totalTime) * 100);
        timerValue = (float)(initTime / totalTime);
    }

    void checkTime()
    {
        if(initTime <= 0)
        {
            SceneManager.LoadScene("Time Out");
        }
    }
}
