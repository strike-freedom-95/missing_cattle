using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreboard;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        scoreboard.text = "SCORE : " + PlayerPrefs.GetInt("Score", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Game Scene");
        }
    }
}
