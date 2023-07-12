using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    TextMeshProUGUI gameOverText;
    private void Start()
    {
        gameOverText = GetComponent<TextMeshProUGUI>();
        gameOverText.enabled = false;
        // HideGameOverText();
    }
    public void DisplayGameOverText()
    {
        gameOverText.enabled = true;
    }

    public void HideGameOverText()
    {
        gameOverText.enabled = false;
    }
}
