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
    // bool isGameComplete = false;
    int cowCount = 0;
    int totalCows = 0;
    // int cowTaken = 0;
    // int cowDeath = 0;
    float initTime = 100f;
    float timerValue = 1;

    AudioSource gameMusic;

    [SerializeField] Image gameMenu;
    [SerializeField] Image resultDisplay;
    [SerializeField] TextMeshProUGUI cowCountGUI;
    [SerializeField] Slider timerSlider;
    [SerializeField] float totalTime = 100f;
    [SerializeField] TextMeshProUGUI scoreBoard;
    [SerializeField] TextMeshProUGUI remarkText;
    [SerializeField] TextMeshProUGUI resultText;
    [SerializeField] Button nextLevelButton;
    // [SerializeField] Button musicEnableButton;
    // Start is called before the first frame update
    void Start()
    {
        gameMenu.gameObject.SetActive(false);
        resultDisplay.gameObject.SetActive(false);
        Cursor.visible = false;
        totalCows = GameObject.FindGameObjectsWithTag("Cattle").Length;
        gameMusic = GetComponent<AudioSource>();
        Time.timeScale = 1.0f;
        timerSlider.value = 1;
        initTime = totalTime;
        nextLevelButton.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        timerSlider.value = timerValue;
        checkTime();
        cowCount = GameObject.FindGameObjectsWithTag("Cattle").Length;
        // Debug.Log(cowCount);
        string cattleCountDisplay = PlayerPrefs.GetInt("Score", 0).ToString() + "/" + totalCows.ToString();
        cowCountGUI.text = cattleCountDisplay;

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

    private void FixedUpdate()
    {
        initTime -= 0.1f;
        // Debug.Log((float)(initTime / totalTime) * 100);
        timerValue = (float)(initTime / totalTime);
    }

    void GamePause()
    {
        isGamePaused = true;
        Cursor.visible = true;
        gameMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
        gameMusic.Pause();
    }

    void GameComplete()
    {
        Cursor.visible = true;
        scoreBoard.text = DisplayFinalScore();
        resultDisplay.gameObject.SetActive(true);
        Time.timeScale = 0;
        gameMusic.Pause();
    }    

    void ShowGameResult()
    {
        GameComplete();
    }    

    void checkTime()
    {
        if(initTime <= 0)
        {
            // SceneManager.LoadScene("Time Out");
            ShowGameResult();
        }
    }

    public void MoveToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartGame()
    {
        //SceneManager.LoadScene("Game Scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
        gameMusic.Stop();
    }

    public void GameResume()
    {
        isGamePaused = false;
        Cursor.visible = false;
        gameMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        gameMusic.Play();
    }

    string DisplayFinalScore()
    {
        string result = string.Empty;
        string remarkMessage = string.Empty;
        string resultMessage = string.Empty;
        int finalCattleScore = PlayerPrefs.GetInt("Score", 0);
        int finalTurretScore = PlayerPrefs.GetInt("TurretDestroyed", 0);
        result = "FINAL SCORE\nCATTLE CAPTURED : " + finalCattleScore.ToString() + "\nENEMIES DESTROYED: " + finalTurretScore.ToString();
        if(finalCattleScore == 0)
        {
            nextLevelButton.interactable = false;
            remarkMessage = "\"YOU SHALL NOT PASS!!!\"";
            resultMessage = "[FAILED]";
            resultText.color = Color.red;
            // remarkMessage = "\"PERFECT\"";
        }
        else if(totalCows == finalCattleScore)
        {
            // remarkMessage = "\"WELL DONE\"";
            remarkMessage = "\"PERFECT\"";
            resultMessage = "[LEVEL COMPLETE]";
        }
        else if(totalCows - finalCattleScore <= 3)
        {
            // nextLevelButton.interactable = false;
            // remarkMessage = "\"YOU SHALL NOT PASS!!!\"";
            remarkMessage = "\"WELL DONE\"";
            resultMessage = "[LEVEL COMPLETE]";
        }
        else
        {
            remarkMessage = "\"MEDIOCRE\"";
        }
        resultText.text = resultMessage;
        remarkText.text = remarkMessage;
        return result;
    }

    //totalCows - finalCattleScore <= 3
    // totalCows == finalCattleScore
    //finalCattleScore == 0

}
