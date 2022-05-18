using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Pause")]
    public GameObject pausePanel;
    public GameObject optionsPanel;
    public GameObject ui;

    [Header("Bright")]
    public Slider slider;
    public float sliderValue;
    public Image panelBright;

    bool pauseActive;



    void Start()
    {
        pauseActive = false;
        slider.value = PlayerPrefabs.Getfloat("Brightness", 0.5f);
        panelBright.color = new Color(panelBright.color.r, panelBright.color.g, panelBright.color.b, slider.value);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseActive)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            pauseActive = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseActive)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            pauseActive = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (pauseActive) {ui.SetActive(false);}
        else {ui.SetActive(true);}
    }

    public void OptionsPanelActive()
    {
        optionsPanel.SetActive(true);
    }

    public void OptionsPanelDeactive()
    {
        optionsPanel.SetActive(false);
    }

    public void ChangeSlider(float valor)
    {
        sliderValur = valor;
        PlayerPrefabs.SetFloat("Brightness", sliderValue);
        panelBright.color = new Color(panelBright.color.r, panelBright.color.g, panelBright.color.b, slider.value);
    }
}
