using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CinematicaManager : MonoBehaviour
{
    public float changeDelay = 10;
    
    public void Start()
    {
        StartCoroutine(Change());
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Level1");
        }
    }
    
    IEnumerator Change()
    {
        yield return new WaitForSeconds(changeDelay);
        SceneManager.LoadScene("Level1");
    }
}
