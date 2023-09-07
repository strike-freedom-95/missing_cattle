using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    TextMeshProUGUI gameOverText;

    [SerializeField] Image tutorialsWindow;
    private void Start()
    {
        tutorialsWindow.gameObject.SetActive(false);
    }

    public void OpenTutorialsWindow()
    {
        tutorialsWindow.gameObject.SetActive(true);
    }

    public void CloseTutorialsWindow()
    {
        tutorialsWindow.gameObject.SetActive(false);
    }


    public void DisplayGameOverText()
    {
        gameOverText.enabled = true;
    }

    public void HideGameOverText()
    {
        gameOverText.enabled = false;
    }

    public void PlayAgainButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
