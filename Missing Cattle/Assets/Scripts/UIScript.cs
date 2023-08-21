using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    TextMeshProUGUI gameOverText;
    private void Start()
    {
        //gameOverText = GetComponent<TextMeshProUGUI>();
        //gameOverText.enabled = false;
        //HideGameOverText();
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
        SceneManager.LoadScene("Game Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
