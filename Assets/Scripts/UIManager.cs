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
    public GameObject gameOverPanel;
    public GameObject ui;

    [Header("Bright")]
    public Slider brightSlider;
    public float sliderValue;
    public Image panelBright;

    [Header("Volume")]
    public Slider musicSlider;
    public float musicSliderValue;
    public Slider soundSlider;
    public float soundSliderValue;

    [Header("Resolution")]
    Resolution[] resolutions;
    bool pauseActive;
    public Toggle toggle;
    public TMP_Dropdown resDropdown;

    [Header("Quality Settings")]
    public TMP_Dropdown dropdown;
    public int quality;

    [Header("Collectable")]
    public GameObject collectable;

    void Start()
    {
        pauseActive = false;
        brightSlider.value = PlayerPrefs.GetFloat("Brightness", 0.5f);
        panelBright.color = new Color(panelBright.color.r, panelBright.color.g, panelBright.color.b, brightSlider.value);

        musicSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        AudioListener.volume = musicSlider.value;

        soundSlider.value = PlayerPrefs.GetFloat("VFXVolume", 0.5f);
        AudioListener.volume = soundSlider.value;

        if (Screen.fullScreen) {toggle.isOn = true;}
        else {toggle.isOn = false;}
        //CheckResolution();

        quality = PlayerPrefs.GetInt("qualityLevel", 1);
        dropdown.value = quality;
        //AdjustQuality();
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

        if (Global.isDead)
        {
            Cursor.lockState = CursorLockMode.Confined;
            //Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        //Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void OptionsPanelActive()
    {
        optionsPanel.SetActive(true);
    }

    public void OptionsPanelDeactive()
    {
        optionsPanel.SetActive(false);
    }

    public void ChangeBrightnessSlider(float brightValor)
    {
        sliderValue = brightValor;
        PlayerPrefs.SetFloat("Brightness", sliderValue);
        panelBright.color = new Color(panelBright.color.r, panelBright.color.g, panelBright.color.b, brightSlider.value);
    }
    
    public void ChangeMusicSlider(float musicValor)
    {
        musicSliderValue = musicValor;
        PlayerPrefs.SetFloat("BGMVolume", musicSliderValue);
        AudioListener.volume = musicSlider.value;
    }
    
    public void ChangeSoundSlider(float soundValor)
    {
        soundSliderValue = soundValor;
        PlayerPrefs.SetFloat("VFXVolume", soundSliderValue);
        AudioListener.volume = soundSlider.value;
    }

    public void SetFullscreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    /* public void AdjustQuality()
    {
        QualitySettings.SetQualityLevel(dropdown.value);
        PlayerPrefs.SetInt("qualityLevel", dropdown.value);
        quality = dropdown.value;
    }*/

    /* public void CheckResolution()
    {
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();
        List<string> options = new List<string>();
        int actualRes = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                actualRes = i;
            }
            resDropdown.AddOptions(options);
            resDropdown.value = actualRes;
            resDropdown.RefreshShownValue();

            resDropdown.value = PlayerPrefs.GetInt("numberRes", 0);
        }
    }*/

    /* public void ChangeRes(int resIndex)
    {
        PlayerPrefs.SetInt("numberRes", resDropdown.value);
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }*/

    public void HideCollectable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        collectable.GetComponent<Animator>().SetInteger("State", 2);
        Global.moving = true;
    }
}
