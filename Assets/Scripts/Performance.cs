using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Performance : MonoBehaviour
{
    public TextMeshProUGUI fpsDisplay;
    public int targetFrameRate = 60;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }

    void Update()
    {
        float fps = 1 / Time.deltaTime;
        fpsDisplay.text = fps + "FPS";
    }
}
