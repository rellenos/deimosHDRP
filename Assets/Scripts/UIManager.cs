using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject pausePanel;
    bool pauseActive;

    void Start()
    {
        pauseActive = false;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //Debug.Log("Pause");
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            pauseActive = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && pauseActive)
        {
            //Debug.Log("Play");
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            pauseActive = false;
        }
        
    }
}
